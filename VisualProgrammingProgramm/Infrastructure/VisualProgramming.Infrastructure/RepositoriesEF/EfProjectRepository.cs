

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfProjectRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), IProjectRepository
{
    private readonly DbSet<Project> _projects = context.Set<Project>();

    public Task<Project?> AddAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Graf?> GetGrafByIdProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Project entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Project>> IRepository<Project, Guid>.GetAllAsync(CancellationToken cancellationToken, bool asNoTracking)
    {
        throw new NotImplementedException();
    }

    Task<Project?> IRepository<Project, Guid>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
