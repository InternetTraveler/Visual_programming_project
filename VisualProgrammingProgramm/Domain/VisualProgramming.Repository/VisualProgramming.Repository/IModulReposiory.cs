using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Modul"/> (модулей).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{Modul, Guid}"/>.
/// Предоставляет методы для получения графа операций и портов, связанных с модулем.
/// </remarks>
public interface IModulRepository : IRepository<Modul, Guid>
{
    /// <summary>
    /// Асинхронно получает граф операций, связанный с указанным модулем, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор модуля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит граф операций <see cref="Graf"/>, 
    /// связанный с модулем, или null, если модуль не найден или не имеет графа.
    /// </returns>
    Task<Graf?> GetGrafByIdModulAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно получает порт, связанный с указанным модулем, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор модуля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит порт <see cref="Port"/>, 
    /// связанный с модулем, или null, если модуль не найден или не имеет порта.
    /// </returns>
    Task<Port?> GetPortByIdModulAsync(Guid id, CancellationToken cancellationToken);
}