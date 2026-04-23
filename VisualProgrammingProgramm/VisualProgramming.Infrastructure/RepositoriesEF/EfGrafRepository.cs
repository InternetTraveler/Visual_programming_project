using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfGrafRepository(ApplicationDbContext context)
    : EfRepository<Graf, Guid>(context), IGrafRepository
{
    private readonly DbSet<Graf> _grafs = context.Set<Graf>();

    public Task<Connection?> AddAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Project?> AddAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Connection?> GetConnectionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Graf?> GetGrafByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
