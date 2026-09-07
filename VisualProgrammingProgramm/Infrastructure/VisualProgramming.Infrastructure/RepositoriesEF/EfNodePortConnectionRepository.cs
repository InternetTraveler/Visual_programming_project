

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfNodePortConnectionRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), INodePortConnectionRepository
{
    private readonly DbSet<NodePortConnection> _nodesPortConnections = context.Set<NodePortConnection>();

    public Task<NodePortConnection?> AddAsync(NodePortConnection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(NodePortConnection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<BaseNode?> GetNodeAndModulByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Port?> GetPortByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(NodePortConnection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<NodePortConnection>> IRepository<NodePortConnection, Guid>.GetAllAsync(CancellationToken cancellationToken, bool asNoTracking)
    {
        throw new NotImplementedException();
    }

    Task<NodePortConnection?> IRepository<NodePortConnection, Guid>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
