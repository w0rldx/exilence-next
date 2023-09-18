namespace API.Services
{
    using System;
    using System.Threading.Tasks;
    using API.Interfaces;
    using AutoMapper;
    using Shared.Entities;
    using Shared.Interfaces;

    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IMapper _mapper;


        public CacheService(ICacheRepository cacheRepository, IMapper mapper)
        {
            _cacheRepository = cacheRepository;
            _mapper = mapper;
        }

        public async Task<string> Get(string key)
        {
            var cache = await _cacheRepository.Get(key);

            if (cache != null)
            {
                return cache.Value;
            }

            return null;
        }


        public async Task Add(string key, string value, DateTime expireAt)
        {
            var cacheValue = new CacheItem
            {
                Key = key,
                Value = value,
                ExpireAt = expireAt
            };

            await _cacheRepository.Add(cacheValue);
        }
    }
}
