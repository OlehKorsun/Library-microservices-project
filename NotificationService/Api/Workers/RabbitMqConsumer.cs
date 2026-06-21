using System.Text;
using System.Text.Json;
using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Infrastructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Api.Workers;

public class RabbitMqConsumer(
    RabbitMqConnection rabbitMqConnection, 
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        const string queueName = "notification_queue";
        const string retryQueue = "notification_retry_queue";
        const string exchange = "notification_exchange";

        while (!ct.IsCancellationRequested)
        {
            try
            {
                var connection = await rabbitMqConnection.GetConnectionAsync(ct);
                var channel = await connection.CreateChannelAsync(null, ct);

                // limit number of unacked messages
                await channel.BasicQosAsync(0, 5, false, ct);

                // exchange
                await channel.ExchangeDeclareAsync(
                    exchange,
                    "direct",
                    durable: true,
                    autoDelete: false,
                    cancellationToken: ct);

                // main queue
                await channel.QueueDeclareAsync(
                    queueName,
                    true,
                    false,
                    false,
                    arguments: null,
                    cancellationToken: ct);

                await channel.QueueBindAsync(
                    queue: queueName,
                    exchange: exchange,
                    routingKey: queueName,
                    cancellationToken: ct);

                // retry queue
                var retryArgs = new Dictionary<string, object>
                {
                    { "x-message-ttl", 30000 },
                    { "x-dead-letter-exchange", exchange },
                    { "x-dead-letter-routing-key", queueName },
                };

                await channel.QueueDeclareAsync(
                    retryQueue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: retryArgs,
                    cancellationToken: ct);

                // consumer
                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (_, args) =>
                {
                    try
                    {
                        var body = args.Body.ToArray();
                        var messageJson = Encoding.UTF8.GetString(body);

                        NotificationDto? eventMessage = null;

                        try
                        {
                            eventMessage = JsonSerializer.Deserialize<NotificationDto>(messageJson);
                        }
                        catch (JsonException jsonEx)
                        {
                            Console.WriteLine($" [!] JSON deserialization error: {jsonEx.Message}");
                        }

                        if (eventMessage == null)
                        {
                            Console.WriteLine(" [!] Received invalid or empty message. Rejecting.");

                            await channel.BasicNackAsync(
                                deliveryTag: args.DeliveryTag,
                                multiple: false,
                                requeue: false,
                                cancellationToken: ct);

                            return;
                        }

                        Console.WriteLine($" [RabbitMQ] Received task for: {eventMessage.To}");

                        using var scope = serviceProvider.CreateScope();

                        var orchestration =
                            scope.ServiceProvider.GetRequiredService<NotificationOrchestrator>();

                        var result = await orchestration.HandleBookOverdueAsync(eventMessage);

                        switch (result)
                        {
                            case NotificationResult.Success:

                                Console.WriteLine("Email sent");

                                await channel.BasicAckAsync(
                                    args.DeliveryTag,
                                    false,
                                    ct);

                                break;

                            case NotificationResult.Retry:

                                Console.WriteLine("Retry scheduled");

                                var properties = new BasicProperties
                                {
                                    Persistent = true
                                };

                                await channel.BasicPublishAsync<BasicProperties>(
                                    exchange: exchange,
                                    routingKey: retryQueue,
                                    mandatory: false,
                                    basicProperties: properties,
                                    body: args.Body,
                                    cancellationToken: ct
                                );

                                await channel.BasicAckAsync(
                                    args.DeliveryTag,
                                    false,
                                    ct);

                                break;

                            case NotificationResult.Failed:

                                Console.WriteLine("Max attempts reached");

                                await channel.BasicAckAsync(
                                    args.DeliveryTag,
                                    false,
                                    ct);

                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" [!] Error processing message: {ex.Message}");

                        await channel.BasicNackAsync(
                            deliveryTag: args.DeliveryTag,
                            multiple: false,
                            requeue: true,
                            ct);
                    }
                    finally
                    {
                        await channel.CloseAsync(ct);
                    }
                };

                await channel.BasicConsumeAsync(
                    queue: queueName,
                    autoAck: false,
                    consumerTag: string.Empty,
                    noLocal: false,
                    exclusive: false,
                    arguments: null,
                    consumer: consumer,
                    cancellationToken: ct);

                await Task.Delay(Timeout.Infinite, ct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" [!] RabbitMQ connection error: {ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
            }
        }
    }
}