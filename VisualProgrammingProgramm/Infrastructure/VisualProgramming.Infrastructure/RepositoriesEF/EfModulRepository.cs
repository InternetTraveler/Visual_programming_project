

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfModulRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), IModulRepository
{
    private readonly DbSet<Modul> _moduls = context.Set<Modul>();

    public Task<Modul?> AddAsync(Modul entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Modul entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Graf?> GetGrafByIdModulAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Port?> GetPortByIdModulAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Modul entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Modul>> IRepository<Modul, Guid>.GetAllAsync(CancellationToken cancellationToken, bool asNoTracking)
    {
        throw new NotImplementedException();
    }

    Task<Modul?> IRepository<Modul, Guid>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
