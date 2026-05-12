using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Node"/> (узлов).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{Node, Guid}"/>.
/// Предоставляет методы для получения портов, связанных с узлами.
/// </remarks>
public interface INodeRepository : IRepository<Node, Guid>
{
    /// <summary>
    /// Асинхронно получает порт, связанный с указанным узлом, по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор узла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит порт <see cref="Port"/>, 
    /// связанный с узлом, или null, если узел не найден или не имеет порта.
    /// </returns>
    Task<Port?> GetPortByIdNodeAsync(Guid id, CancellationToken cancellationToken);
}