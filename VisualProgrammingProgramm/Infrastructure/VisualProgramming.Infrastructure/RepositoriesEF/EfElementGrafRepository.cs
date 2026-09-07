using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfElementGrafRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), IElementGrafRepository
{
    private readonly DbSet<ElementGraf> _elementsGraf = context.Set<ElementGraf>();

    public Task<ElementGraf?> AddAsync(ElementGraf entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(ElementGraf entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Port?> GetPortByIdElementGrafAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(ElementGraf entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<ElementGraf>> IRepository<ElementGraf, Guid>.GetAllAsync(CancellationToken cancellationToken, bool asNoTracking)
    {
        throw new NotImplementedException();
    }

    Task<ElementGraf?> IRepository<ElementGraf, Guid>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
