

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfModulRepository(ApplicationDbContext context) :
    EfRepository<Modul, Guid>(context), IModulRepository
{
    private readonly DbSet<Modul> _moduls = context.Set<Modul>();

    public Task<Graf?> GetGrafByIdModulAsync(Guid id, CancellationToken cancellationToken)
    => _moduls
        .Where(m => m.Id == id)
        .Select(m => m.GrafOperation)
        .FirstOrDefaultAsync(cancellationToken);

    public Task<Port?> GetPortByIdModulAsync(Guid id, CancellationToken cancellationToken)
        => _moduls
            .Where(m => m.Id == id)
            .SelectMany(m => m.NodePortConnections)
            .Select(npc => npc.Port)
            .FirstOrDefaultAsync(cancellationToken);
}
