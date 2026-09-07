
using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfGrafRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), IGrafRepository
{
    private readonly DbSet<Graf> _grafs = context.Set<Graf>();

    public Task<Graf?> AddAsync(Graf entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Graf entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ElementGraf?> GetElementGafByIdGrafAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Graf entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Graf>> IRepository<Graf, Guid>.GetAllAsync(CancellationToken cancellationToken, bool asNoTracking)
    {
        throw new NotImplementedException();
    }

    Task<Graf?> IRepository<Graf, Guid>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
