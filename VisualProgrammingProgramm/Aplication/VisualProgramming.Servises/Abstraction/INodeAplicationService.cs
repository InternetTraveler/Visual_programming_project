
using VisualProgramming.Moduls.Node;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface INodeAplicationService : IApplicationService<NodeModel, CreateNodeModel, Guid>
{
    // Получить все связи узла с портами
    Task<IEnumerable<NodePortConnectionModel>> GetNodePortConnectionsAsync(
        Guid nodeId, CancellationToken cancellationToken = default);

    // Добавить связь узла с портом
    Task<bool> AddNodePortConnectionAsync(
        Guid nodeId, Guid nodePortConnectionId, CancellationToken cancellationToken = default);

    // Удалить связь узла с портом
    Task<bool> RemoveNodePortConnectionAsync(
        Guid nodeId, Guid nodePortConnectionId, CancellationToken cancellationToken = default);
}
