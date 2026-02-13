using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Infrastructure.Messaging;

public class EventPublisher
{
    private readonly RabbitMqConnection _connection;
    public EventPublisher(RabbitMqConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishAsync<T>(string queueName, T message, CancellationToken ct = default)
    {
        var connection = await _connection.GetConnectionAsync(ct);
        
        await using var channel = await connection.CreateChannelAsync(null, ct);

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: ct
        );
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: new BasicProperties(), 
            body: body,
            cancellationToken: ct
        );
        
        Console.WriteLine($" [->] Mock Event Sent to '{queueName}': {json}");
    }
}