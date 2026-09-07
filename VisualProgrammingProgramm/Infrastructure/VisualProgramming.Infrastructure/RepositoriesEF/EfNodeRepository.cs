using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfNodeRepository(ApplicationDbContext context) :
    EfRepository<Node, Guid>(context), INodeRepository
{
    private readonly DbSet<Node> _nodes = context.Set<Node>();

    public Task<Port?> GetPortByIdNodeAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
