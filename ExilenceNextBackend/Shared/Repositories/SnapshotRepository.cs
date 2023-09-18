﻿namespace Shared.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using Shared.Entities;
    using Shared.Interfaces;

    public class SnapshotRepository : ISnapshotRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<PricedItem> _pricedItems;
        private readonly IMongoCollection<Snapshot> _snapshots;

        private readonly IMongoCollection<StashTab> _stashtabs;

        public SnapshotRepository(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetSection("ConnectionStrings")["Mongo"]);
            _database = _client.GetDatabase(configuration.GetSection("Mongo")["Database"]);
            _snapshots = _database.GetCollection<Snapshot>("Snapshots");
            _stashtabs = _database.GetCollection<StashTab>("Stashtabs");
            _pricedItems = _database.GetCollection<PricedItem>("Priceditems");
        }

        public async Task<bool> SnapshotExists(string clientId)
        {
            var count = await _snapshots.CountDocumentsAsync(s => s.ClientId == clientId);
            return count > 0;
        }

        public IMongoQueryable<Snapshot> GetSnapshots(Expression<Func<Snapshot, bool>> predicate)
        {
            return _snapshots.AsQueryable().Where(predicate);
        }

        public IMongoQueryable<StashTab> GetStashtabs(Expression<Func<StashTab, bool>> predicate)
        {
            return _stashtabs.AsQueryable().Where(predicate);
        }

        public IMongoQueryable<PricedItem> GetPricedItems(Expression<Func<PricedItem, bool>> predicate)
        {
            return _pricedItems.AsQueryable().Where(predicate);
        }

        public async Task RemoveSnapshot(Snapshot snapshot)
        {
            await _snapshots.DeleteOneAsync(s => s.ClientId == snapshot.ClientId);
        }

        public async Task RemoveStashtab(StashTab stashtab)
        {
            await _stashtabs.DeleteOneAsync(s => s.ClientId == stashtab.ClientId);
        }

        public async Task RemoveStashtabsForSnapshot(string snapshotClientId)
        {
            await _stashtabs.DeleteManyAsync(s => s.SnapshotClientId == snapshotClientId);
        }

        public async Task AddSnapshots(List<Snapshot> snapshots)
        {
            await _snapshots.InsertManyAsync(snapshots);
        }

        public async Task AddStashtabs(List<StashTab> snapshots)
        {
            await _stashtabs.InsertManyAsync(snapshots);
        }

        public async Task AddPricedItems(List<PricedItem> pricedItems)
        {
            await _pricedItems.InsertManyAsync(pricedItems);
        }

        public async Task RemovePricedItems(string profileClientId)
        {
            await _pricedItems.DeleteManyAsync(s => s.SnapshotProfileClientId == profileClientId);
        }

        public async Task RemoveAllSnapshots(string profileClientId)
        {
            // todo: readd when we want to persist stash tabs and items for historical reasons
            //await _pricedItems.DeleteManyAsync(s => s.SnapshotProfileClientId == profileClientId);
            //await _stashtabs.DeleteManyAsync(s => s.SnapshotProfileClientId == profileClientId);
            await _snapshots.DeleteManyAsync(s => s.ProfileClientId == profileClientId);
        }
    }
}
