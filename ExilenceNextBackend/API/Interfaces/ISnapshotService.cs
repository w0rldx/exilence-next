namespace API.Interfaces
{
    using System.Threading.Tasks;
    using Shared.Models;

    public interface ISnapshotService
    {
        Task<SnapshotModel> GetSnapshot(string snapshotId);
        Task<SnapshotModel> AddSnapshot(string profileId, SnapshotModel snapshotModel);
        Task RemoveSnapshot(string snapshotId);
        Task RemoveAllSnapshots(string profileClientId);
    }
}
