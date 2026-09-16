using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfElementGrafRepository(ApplicationDbContext context) :
    EfRepository<ElementGraf, Guid>(context), IElementGrafRepository
{
    private readonly DbSet<ElementGraf> _elementsGraf = context.Set<ElementGraf>();

    public Task<Port?> GetPortByIdElementGrafAsync(Guid id, CancellationToken cancellationToken)
    => _elementsGraf
        .Where(e => e.Id == id)
        .SelectMany(e => e.ElementGrafConnections)
        .Select(c => c.SourcePort)
        .FirstOrDefaultAsync(cancellationToken);
}
