using Application.Interfaces.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public class MongoNotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<NotificationLog> _collection;

    public MongoNotificationRepository(IOptions<MongoDbSettings> configuration)
    {
        var dbSettings = configuration.Value;
        
        var client = new MongoClient(dbSettings.ConnectionString);
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<NotificationLog>("NotificationLogs");
    }

    public async Task AddAsync(NotificationLog log)
    {
        await _collection.InsertOneAsync(log);
    }
}