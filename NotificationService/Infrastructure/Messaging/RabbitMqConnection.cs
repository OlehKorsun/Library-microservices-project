using RabbitMQ.Client;

namespace Infrastructure.Messaging;

public class RabbitMqConnection : IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection? _connection;
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    
    public  RabbitMqConnection(String host, string username, string password)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = host,
            UserName = username,
            Password = password
        };
    }

    public async Task<IConnection> GetConnectionAsync(CancellationToken ct = default)
    {
        if (_connection is { IsOpen: true })
        {
            return _connection;
        }
        
        await _semaphoreSlim.WaitAsync(ct);
        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            _connection = await _connectionFactory.CreateConnectionAsync(ct);
            return _connection;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _semaphoreSlim.Dispose();
    }
}