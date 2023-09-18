namespace Shared.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using Shared.Entities;
    using Shared.Interfaces;

    public class CacheRepository : ICacheRepository
    {
        private readonly IMongoCollection<CacheItem> _cache;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public CacheRepository(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetSection("ConnectionStrings")["Mongo"]);
            _database = _client.GetDatabase(configuration.GetSection("Mongo")["Database"]);
            _cache = _database.GetCollection<CacheItem>("Cache");
        }

        public async Task<CacheItem> Get(string key)
        {
            return await _cache.AsQueryable().FirstOrDefaultAsync(c => c.Key == key);
        }

        public async Task Add(CacheItem cacheValue)
        {
            await _cache.InsertOneAsync(cacheValue);
        }

        public IQueryable<CacheItem> Queryable(Expression<Func<CacheItem, bool>> predicate)
        {
            return _cache.AsQueryable().Where(predicate);
        }
    }
}
