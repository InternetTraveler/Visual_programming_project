
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Project;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

/// <summary>
/// Сервис приложения для управления проектами и связанными графами.
/// </summary>
/// <remarks>
/// Наследует базовые CRUD-операции над <see cref="ProjectModel"/> 
/// с моделью создания <see cref="CreateProjectModel"/> и ключом <see cref="Guid"/>.
/// </remarks>
public interface IProjectAplicationService : IApplicationService<ProjectModel, CreateProjectModel, Guid>
{
    /// <summary>
    /// Добавляет граф в проект.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта.</param>
    /// <param name="model">Модель создания графа.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <returns><see langword="true"/>, если граф добавлен; иначе <see langword="false"/>.</returns>
    Task<bool> AddGrafAsync(Guid projectId, CreateGrafModel model, CancellationToken ct = default);

    /// <summary>
    /// Удаляет граф из проекта.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта.</param>
    /// <param name="grafId">Идентификатор графа.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <returns><see langword="true"/>, если граф удалён; иначе <see langword="false"/>.</returns>
    Task<bool> RemoveGrafAsync(Guid projectId, Guid grafId, CancellationToken ct = default);
}
