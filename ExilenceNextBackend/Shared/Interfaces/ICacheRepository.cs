namespace Shared.Interfaces
{
    using System.Threading.Tasks;
    using Shared.Entities;

    public interface ICacheRepository
    {
        Task<CacheItem> Get(string key);
        Task Add(CacheItem cacheValue);
    }
}
