namespace Shared.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Shared.Entities;

    public interface IGroupRepository
    {
        IQueryable<Group> GetGroups(Expression<Func<Group, bool>> predicate);
        Group AddGroup(Group group);
        Task<Group> RemoveGroup(string name);

        Task<Connection> GetConnection(string connectionId);
        Connection AddConnection(Connection connection);
        Task<Connection> RemoveConnection(string connectionId);

        Task SaveChangesAsync();
    }
}
