using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы со связями "узел-порт" (<see cref="NodePortConnection"/>).
/// </summary>
/// <remarks>
/// В отличие от <see cref="IConnectionRepository"/>, данный интерфейс специализируется
/// на операциях с соединениями между узлами (<see cref="BaseNode"/>) и портами (<see cref="Port"/>).
/// Предоставляет методы для получения узлов и портов, связанных через NodePortConnection.
/// </remarks>
public interface INodePortConnectionRepository : IRepository<NodePortConnection, Guid>
{
    /// <summary>
    /// Асинхронно получает базовый узел или модуль, связанный с указанной связью, по её идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор связи узла с портом.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит базовый узел <see cref="BaseNode"/> (который может быть как обычным узлом, 
    /// так и модулем), связанный со связью, или null, если связь не найдена.
    /// </returns>
    Task<BaseNode?> GetNodeAndModulByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно получает порт, связанный с указанной связью, по её идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор связи узла с портом.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результат задачи содержит порт <see cref="Port"/>, 
    /// связанный со связью, или null, если связь не найдена.
    /// </returns>
    Task<Port?> GetPortByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken);
}