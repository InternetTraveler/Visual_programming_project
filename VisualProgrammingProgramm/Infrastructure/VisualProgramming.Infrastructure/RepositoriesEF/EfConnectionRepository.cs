

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfConnectionRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), IConnectionRepository
{
    private readonly DbSet<Connection> _connection = context.Set<Connection>();

    public Task<Connection?> AddAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ElementGraf?> GetElementGrafByIdConnectionAsync(Guid id, CancellationToken cancellationToken)
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
