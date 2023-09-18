namespace Shared.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver.Linq;
    using Shared.Entities;

    public interface ISnapshotRepository
    {
        Task<bool> SnapshotExists(string clientId);
        IMongoQueryable<Snapshot> GetSnapshots(Expression<Func<Snapshot, bool>> predicate);
        IMongoQueryable<StashTab> GetStashtabs(Expression<Func<StashTab, bool>> predicate);
        IMongoQueryable<PricedItem> GetPricedItems(Expression<Func<PricedItem, bool>> predicate);
        Task AddSnapshots(List<Snapshot> snapshots);
        Task AddStashtabs(List<StashTab> stashTabs);
        Task AddPricedItems(List<PricedItem> pricedItems);
        Task RemoveSnapshot(Snapshot snapshot);
        Task RemoveStashtab(StashTab stashtab);
        Task RemoveStashtabsForSnapshot(string snapshotClientId);
        Task RemovePricedItems(string profileClientId);
        Task RemoveAllSnapshots(string profileClientId);
    }
}
