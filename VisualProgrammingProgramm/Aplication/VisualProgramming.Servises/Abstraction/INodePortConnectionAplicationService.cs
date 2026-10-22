
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface INodePortConnectionAplicationService : IApplicationService<NodePortConnectionModel, CreateNodePortConnectionModel, Guid>
{
    // Проверка, содержит ли связь указанный узел
    Task<bool> IsContainNodeAsync(Guid connectionId, Guid nodeId, CancellationToken cancellationToken = default);

    // Проверка, содержит ли связь указанный порт
    Task<bool> IsContainPortAsync(Guid connectionId, Guid portId, CancellationToken cancellationToken = default);

    // Получение всех связей для узла
    Task<IEnumerable<NodePortConnectionModel>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken = default);

    // Получение всех связей для порта
    Task<IEnumerable<NodePortConnectionModel>> GetByPortIdAsync(Guid portId, CancellationToken cancellationToken = default);
}
