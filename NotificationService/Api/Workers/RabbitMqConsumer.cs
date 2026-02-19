using System.Text;
using System.Text.Json;
using Application.DTOs;
using Application.Services;
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
        var queueName = "notification_queue";

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

                        var eventMessage = JsonSerializer.Deserialize<NotificationDto>(messageJson);

                        if (eventMessage != null)
                        {
                            Console.WriteLine($" [RabbitMQ] Received task for: {eventMessage.Email}");

                            using (var scope = _serviceProvider.CreateScope())
                            {
                                var orchestration =
                                    scope.ServiceProvider.GetRequiredService<NotificationOrchestrator>();

                                await orchestration.HandleBookOverdueAsync(eventMessage);
                            }
                        }
                    } catch (Exception ex)
                    {
                        Console.WriteLine($" [!] Error processing message: {ex.Message}");
                    }
                };
                
                await channel.BasicConsumeAsync(
                    queue: queueName,
                    autoAck: true,
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
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
            }
        }
    }
}