
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface IGrafAplicationService : IApplicationService<GrafModel, CreateGrafModel, Guid>
{
    // Проверка принадлежности проекту
    Task<bool> IsContainsProjectAsync(Guid grafId, Guid projectId, CancellationToken cancellationToken = default);

    // Добавление элемента в граф
    Task<bool> AddElementAsync(Guid grafId, Guid elementGrafId, CancellationToken cancellationToken = default);

    // Удаление элемента из графа
    Task<bool> RemoveElementAsync(Guid grafId, Guid elementGrafId, CancellationToken cancellationToken = default);

    // Получение всех элементов конкретного графа
    Task<IEnumerable<ElementGrafModel>> GetElementsAsync(Guid grafId, CancellationToken cancellationToken = default);
}
