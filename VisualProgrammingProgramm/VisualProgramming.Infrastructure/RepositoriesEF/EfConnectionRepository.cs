using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfConnectionRepository(ApplicationDbContext context)
    : EfRepository<Connection, Guid>(context), IConnectionRepository
{
    private readonly DbSet<Connection> _connections = context.Set<Connection>();

    public Task<Project?> AddAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task<Connection?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _connections.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<Connection?> GetConnectionByIdAsync(Guid id, CancellationToken cancellationToken)
    => _connections.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}