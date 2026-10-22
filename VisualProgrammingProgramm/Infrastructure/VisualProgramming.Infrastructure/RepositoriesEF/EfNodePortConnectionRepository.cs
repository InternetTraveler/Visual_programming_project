

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfNodePortConnectionRepository(ApplicationDbContext context) :
    EfRepository<NodePortConnection, Guid>(context), INodePortConnectionRepository
{
    private readonly DbSet<NodePortConnection> _nodesPortConnections = context.Set<NodePortConnection>();

    public Task<BaseNode?> GetNodeAndModulByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken)
    => _nodesPortConnections
        .Where(npc => npc.Id == id)
        .Select(npc => npc.Node)
        .FirstOrDefaultAsync(cancellationToken);

    public Task<Port?> GetPortByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken)
        => _nodesPortConnections
            .Where(npc => npc.Id == id)
            .Select(npc => npc.Port)
            .FirstOrDefaultAsync(cancellationToken);
}
