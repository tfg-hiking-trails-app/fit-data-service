using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Models;
using MongoDB.Driver;

namespace FitDataService.Infrastructure.Data.Repositories;

public abstract class AbstractRepository<T> : IRepository<T> where T : IEntity
{
    protected readonly IMongoCollection<T> Collection;

    public AbstractRepository(IMongoCollection<T> collection)
    {
        Collection = collection;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Collection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await Collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await Collection.InsertOneAsync(entity); 
    }

    public async Task UpdateAsync(string id, T entity)
    {
        await Collection.ReplaceOneAsync(x => x.Id == id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Collection.DeleteOneAsync(x => x.Id == id);
    }
}