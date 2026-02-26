using System.Text;
using System.Text.Json;
using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Infrastructure.Messaging;
using RabbitMQ.Client.Events;

namespace Api.Workers;

public class RabbitMqConsumer : BackgroundService
{

    private readonly RabbitMqConnection _connection;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqConsumer(RabbitMqConnection connection, IServiceProvider serviceProvider)
    {
        _connection = connection;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        const string queueName = "notification_queue";

        while (!ct.IsCancellationRequested)
        {
            try
            {
                var connection = await _connection.GetConnectionAsync(ct);
                var channel = await connection.CreateChannelAsync(null, ct);
                
                await channel.QueueDeclareAsync(queueName, false, false, false, cancellationToken: ct);
                
                var consumer = new AsyncEventingBasicConsumer(channel);
                
                consumer.ReceivedAsync += async (sender, args) =>
                {
                    try
                    {
                        var body = args.Body.ToArray();
                        var messageJson = Encoding.UTF8.GetString(body);
                        
                        NotificationDto? eventMessage = null;

                        // var eventMessage = JsonSerializer.Deserialize<NotificationDto>(messageJson);

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
                                requeue: false
                            );

                            return;
                        }
                        
                        Console.WriteLine($" [RabbitMQ] Received task for: {eventMessage.To}");

                        using var scope = _serviceProvider.CreateScope();
                        
                        var orchestration =
                            scope.ServiceProvider.GetRequiredService<NotificationOrchestrator>();

                        var result = await orchestration.HandleBookOverdueAsync(eventMessage);
                        
                        
                        switch (result)
                        {
                            case NotificationResult.Success:
                                Console.WriteLine("Email sent");
                                await channel.BasicAckAsync(args.DeliveryTag, false);
                                break;

                            case NotificationResult.Retry:
                                Console.WriteLine("Retry scheduled");
                                await channel.BasicNackAsync(args.DeliveryTag, false, true);
                                break;

                            case NotificationResult.Failed:
                                Console.WriteLine("Max attempts reached");
                                await channel.BasicAckAsync(args.DeliveryTag, false);
                                break;
                        }
                        
                    } catch (Exception ex)
                    {
                        Console.WriteLine($" [!] Error processing message: {ex.Message}");
                        
                        await channel.BasicNackAsync(
                            deliveryTag: args.DeliveryTag,
                            multiple: false,
                            requeue: true
                        );
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
                    cancellationToken: ct
                );
                
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