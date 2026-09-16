
using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfGrafRepository(ApplicationDbContext context) :
    EfRepository<Graf, Guid>(context), IGrafRepository
{
    private readonly DbSet<Graf> _grafs = context.Set<Graf>();

    public Task<ElementGraf?> GetElementGafByIdGrafAsync(Guid id, CancellationToken cancellationToken)
    => _grafs
        .Where(g => g.Id == id)
        .SelectMany(g => g.ElementsGraf)
        .FirstOrDefaultAsync(cancellationToken);
}
