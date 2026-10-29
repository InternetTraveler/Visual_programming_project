

using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;
using static NpgsqlTypes.NpgsqlTsQuery;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfNodePortConnectionRepository(ApplicationDbContext context) :
    EfRepository<NodePortConnection, Guid>(context), INodePortConnectionRepository
{
    private readonly DbSet<NodePortConnection> _nodesPortConnections = context.Set<NodePortConnection>();

    public Task<IEnumerable<NodePortConnection>?> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken = default)
    => (Task<IEnumerable<NodePortConnection>?>)_nodesPortConnections
        .Where(npc => npc.Node.Id == nodeId);

    public Task<IEnumerable<NodePortConnection>?> GetByPortIdAsync(Guid portId, CancellationToken cancellationToken = default)
    => (Task<IEnumerable<NodePortConnection>?>)_nodesPortConnections
        .Where(npc => npc.Port.Id == portId);

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
