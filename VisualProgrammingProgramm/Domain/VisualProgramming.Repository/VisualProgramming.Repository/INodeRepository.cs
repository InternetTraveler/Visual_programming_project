using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Node"/> (узлов).
/// Наследует базовый CRUD от <see cref="IRepository{Node, Guid}"/>.
/// </summary>
public interface INodeRepository : IRepository<Node, Guid>
{
    /// <summary>
    /// Асинхронно получает узел по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор узла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный узел, или <c>null</c>, если узел с указанным идентификатором не существует.
    /// </returns>
    Task<Node?> GetNodeByIdAsync(Guid id, CancellationToken cancellationToken);
}