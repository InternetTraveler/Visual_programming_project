using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfNodePortConnectionRepository(ApplicationDbContext context)
    : EfRepository<NodePortConnection, Guid>(context), INodePortConnectionRepository
{
    private readonly DbSet<NodePortConnection> _nodePortConnection 
        = context.Set<NodePortConnection>();

    public Task<Connection?> AddAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Connection?> GetConnectionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Connection>> IRepository<Connection, Guid>.GetAllAsync(CancellationToken cancellationToken, bool asNoTracking)
    {
        throw new NotImplementedException();
    }

    Task<Connection?> IRepository<Connection, Guid>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
