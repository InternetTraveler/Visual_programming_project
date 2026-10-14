
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface IModulAplicationService : IApplicationService<ModulModel, CreateModulModel, Guid>
{
    // Получение графа операций модуля
    Task<GrafModel?> GetGrafOperationAsync(Guid modulId, CancellationToken cancellationToken = default);

    // Обновление графа операций модуля
    Task<bool> UpdateGrafOperationAsync(Guid modulId, Guid grafId, CancellationToken cancellationToken = default);

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
