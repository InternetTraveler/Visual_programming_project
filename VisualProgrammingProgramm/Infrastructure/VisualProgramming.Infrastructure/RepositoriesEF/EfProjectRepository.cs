

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfProjectRepository(ApplicationDbContext context) :
    EfRepository<Project, Guid>(context), IProjectRepository
{
    private readonly DbSet<Project> _projects = context.Set<Project>();

    public Task<Graf?> GetGrafByIdProjectAsync(Guid id, CancellationToken cancellationToken)
    => _projects
        .Where(p => p.Id == id)
        .SelectMany(p => p.Grafs)
        .FirstOrDefaultAsync(cancellationToken);
}
