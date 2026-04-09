using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

public interface INodeRepository : IRepository<Node, Guid>
{
    Task<Node?> GetNodeByIdAsync(Guid id, CancellationToken cancellationToken);
}