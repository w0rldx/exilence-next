namespace API.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface ICacheService
    {
        Task<string> Get(string key);
        Task Add(string key, string value, DateTime expireAt);
    }
}
