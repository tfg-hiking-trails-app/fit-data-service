using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Models;
using MongoDB.Driver;

namespace FitDataService.Infrastructure.Data.Repositories;

public abstract class AbstractRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _collection;

    public AbstractRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity); 
    }

    public async Task UpdateAsync(string id, T entity)
    {
        await _collection.ReplaceOneAsync(x => x.Id == id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}