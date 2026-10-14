
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface IModulAplicationService : IApplicationService<ModulModel, CreateModulModel, Guid>
{
    // Получение графа операций модуля
    Task<GrafModel?> GetGrafOperationAsync(Guid modulId, CancellationToken cancellationToken = default);

    // Получить все связи узла с портами
    Task<IEnumerable<NodePortConnectionModel>> GetNodePortConnectionsAsync(
        Guid modulId, CancellationToken cancellationToken = default);

    // Добавить связь узла с портом
    Task<bool> AddNodePortConnectionAsync(
        Guid modulId, Guid nodePortConnectionId, CancellationToken cancellationToken = default);

    // Удалить связь узла с портом
    Task<bool> RemoveNodePortConnectionAsync(
        Guid modulId, Guid nodePortConnectionId, CancellationToken cancellationToken = default);
}
