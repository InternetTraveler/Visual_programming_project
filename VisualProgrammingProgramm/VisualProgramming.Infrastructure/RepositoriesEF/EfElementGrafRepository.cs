using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfElementGrafRepository(ApplicationDbContext context)
    : EfRepository<ElementGraf, Guid>(context), IElementGrafRepository
{
    private readonly DbSet<ElementGraf> _elementGrafs = context.Set<ElementGraf>();

    public Task<ElementGraf?> GetElementGrafByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
