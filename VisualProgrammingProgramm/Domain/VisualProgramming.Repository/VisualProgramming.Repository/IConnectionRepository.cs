using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Connection"/> (связей).
/// Наследует базовый CRUD от <see cref="IRepository{Connection, Guid}"/>.
/// </summary>
public interface IConnectionRepository : IRepository<Connection, Guid>
{
    /// <summary>
    /// Асинхронно получает связь по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор связи.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденная связь, или <c>null</c>, если связь с указанным идентификатором не существует.
    /// </returns>
    Task<Connection?> GetConnectionByIdAsync(Guid id, CancellationToken cancellationToken);
}
