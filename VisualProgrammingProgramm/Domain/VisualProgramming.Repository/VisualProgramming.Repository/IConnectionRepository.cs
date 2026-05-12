using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Connection"/> (связей).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{Connection, Guid}"/>.
/// Предоставляет методы для выполнения операций с соединениями между портами элементов графа.
/// </remarks>
public interface IConnectionRepository : IRepository<Connection, Guid>
{
    /// <summary>
    /// Асинхронно получает элемент графа, связанный с указанным соединением, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор соединения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит элемент графа <see cref="ElementGraf"/>, 
    /// связанный с соединением, или null, если соединение не найдено.
    /// </returns>
    Task<ElementGraf?> GetElementGrafByIdConnectionAsync(Guid id, CancellationToken cancellationToken);
}