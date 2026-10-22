
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface IElementGrafAplicationService : IApplicationService<ElementGrafModel, CreateElementGrafModel, Guid>
{
    // Проверка принадлежности графу
    Task<bool> IsContainsGrafAsync(Guid elementGrafId, Guid grafId, CancellationToken cancellationToken = default);

    // Добавление соединения к элементу графа
    Task<bool> AddConnectionAsync(Guid elementGrafId, Guid connectionId, CancellationToken cancellationToken = default);

    // Удаление соединения из элемента графа
    Task<bool> RemoveConnectionAsync(Guid elementGrafId, Guid connectionId, CancellationToken cancellationToken = default);

    // Получение соединений конкретного элемента графа (с eager loading)
    Task<IEnumerable<ConnectionModel>> GetConnectionsAsync(Guid elementGrafId, CancellationToken cancellationToken = default);
}
