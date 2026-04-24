using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Port"/> (портов).
/// Наследует базовый CRUD от <see cref="IRepository{Port, Guid}"/>.
/// </summary>
public interface IPortRepository : IRepository<Port, Guid>
{
    /// <summary>
    /// Асинхронно получает порт по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор порта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный порт, или <c>null</c>, если порт с указанным идентификатором не существует.
    /// </returns>
    Task<Port?> GetPortByIdAsync(Guid id, CancellationToken cancellationToken);
}