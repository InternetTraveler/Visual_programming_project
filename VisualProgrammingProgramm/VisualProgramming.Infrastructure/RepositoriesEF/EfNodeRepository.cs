using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfNodeRepository(ApplicationDbContext context)
    : EfRepository<Node, Guid>(context), INodeRepository
{
    private readonly DbSet<Node> _projects = context.Set<Node>();

    public Task<Node?> GetNodeByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
