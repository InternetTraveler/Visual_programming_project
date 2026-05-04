using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Node"/> (узлов).
/// Наследует базовый CRUD от <see cref="IRepository{Node, Guid}"/>.
/// </summary>
public interface INodeRepository : IRepository<Node, Guid>
{
    Task<Port?> GetPortByIdNodeAsync(Guid id, CancellationToken cancellationToken);
}