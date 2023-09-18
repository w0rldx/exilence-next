namespace Shared.MongoMigrations
{
    using System;
    using MongoDB.Driver;
    using MongoDBMigrations;
    using Shared.Entities;
    using Version = MongoDBMigrations.Version;

    public class CacheMigration : IMigration
    {
        public Version Version => new Version(1, 1, 0);
        public string Name => "Cache";

        public void Up(IMongoDatabase database)
        {
            database.CreateCollection("Cache");
            var collection = database.GetCollection<CacheItem>("Cache");
            var indexModel = new CreateIndexModel<CacheItem>(Builders<CacheItem>.IndexKeys.Ascending("ExpireAt"),
                new CreateIndexOptions
                {
                    ExpireAfter = TimeSpan.FromSeconds(0),
                    Name = "ExpireAtIndex"
                });

            collection.Indexes.CreateOne(indexModel);
        }

        public void Down(IMongoDatabase database)
        {
            database.DropCollection("Cache");
        }
    }
}
