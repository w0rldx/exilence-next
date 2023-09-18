namespace Shared.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Shared.Entities;

    public interface IAccountRepository
    {
        Account AddAccount(Account account);
        Account RemoveAccount(Account account);
        IQueryable<Account> GetAccounts(Expression<Func<Account, bool>> predicate);
        IQueryable<Connection> GetConnections(Expression<Func<Connection, bool>> predicate);
        IQueryable<SnapshotProfile> GetProfiles(Expression<Func<SnapshotProfile, bool>> predicate);
        SnapshotProfile RemoveProfile(SnapshotProfile account);
        Task SaveChangesAsync();
    }
}
