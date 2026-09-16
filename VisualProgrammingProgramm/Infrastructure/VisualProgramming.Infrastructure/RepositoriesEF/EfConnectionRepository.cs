

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfConnectionRepository(ApplicationDbContext context) :
    EfRepository<Connection, Guid>(context), IConnectionRepository
{
    private readonly DbSet<Connection> _connection = context.Set<Connection>();

    public Task<ElementGraf?> GetElementGrafByIdConnectionAsync(Guid id, CancellationToken cancellationToken)
    => _connection
        .Where(c => c.Id == id)
        .Select(c => c.InElementGraf)
        .FirstOrDefaultAsync(cancellationToken);
}
