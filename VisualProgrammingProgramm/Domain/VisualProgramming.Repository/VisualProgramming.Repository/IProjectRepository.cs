using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Project"/> (проектов).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{Project, Guid}"/>.
/// Предоставляет методы для получения графов, связанных с проектом.
/// </remarks>
public interface IProjectRepository : IRepository<Project, Guid>
{
    /// <summary>
    /// Асинхронно получает граф, связанный с указанным проектом, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор проекта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит граф <see cref="Graf"/>, 
    /// связанный с проектом, или null, если проект не найден или не имеет графов.
    /// </returns>
    Task<Graf?> GetGrafByIdProjectAsync(Guid id, CancellationToken cancellationToken);
}