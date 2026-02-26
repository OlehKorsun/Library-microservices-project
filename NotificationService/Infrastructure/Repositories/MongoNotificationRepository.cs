using Application.Interfaces.Repositories;
using Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public class MongoNotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<NotificationLog> _collection;

    public MongoNotificationRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<NotificationLog>("NotificationLogs");
    }
    
    public async Task<IEnumerable<NotificationLog>> GetAllAsync(CancellationToken ct = default)
    {
        var notification = await _collection.Find(Builders<NotificationLog>.Filter.Empty).ToListAsync(ct);
        return notification;
    }

    public async Task<NotificationLog> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var filter = Builders<NotificationLog>.Filter.Eq(x => x.Id, id);
        var notification = await _collection.Find(filter).FirstOrDefaultAsync(ct);
        return notification;
    }
    

    public async Task AddAsync(NotificationLog log, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(log, cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(NotificationLog log, CancellationToken ct = default)
    {
        var filter = Builders<NotificationLog>.Filter.Eq(x => x.Id, log.Id);
        
        var result =  await _collection.ReplaceOneAsync(filter, log, cancellationToken: ct);
        
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var filter = Builders<NotificationLog>.Filter.Eq(x => x.Id, id);
        
        var result = await _collection.DeleteOneAsync(filter, ct);

        return result.DeletedCount > 0;
    }
    
    public async Task<NotificationLog?> GetByBusinessKeyAsync(
        string to,
        string subject,
        DateOnly dueDate,
        string bookTitle,
        CancellationToken ct = default)
    {
        var filter = Builders<NotificationLog>.Filter.And(
            Builders<NotificationLog>.Filter.Eq(x => x.To, to),
            Builders<NotificationLog>.Filter.Eq(x => x.Subject, subject),
            Builders<NotificationLog>.Filter.Eq(x => x.DueDate, dueDate),
            Builders<NotificationLog>.Filter.Eq(x => x.BookTitle, bookTitle)
        );

        return await _collection.Find(filter).FirstOrDefaultAsync(ct);
    }
    
}