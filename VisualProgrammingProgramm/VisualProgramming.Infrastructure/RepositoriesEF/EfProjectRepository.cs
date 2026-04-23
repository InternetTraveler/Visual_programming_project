using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfProjectRepository(ApplicationDbContext context)
    : EfRepository<Project, Guid>(context), IProjectRepository
{
    private readonly DbSet<Project> _projects = context.Set<Project>();

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

    public Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Connection entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
