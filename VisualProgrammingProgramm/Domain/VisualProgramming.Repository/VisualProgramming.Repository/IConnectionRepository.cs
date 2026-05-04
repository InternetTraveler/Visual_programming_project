using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Connection"/> (связей).
/// Наследует базовый CRUD от <see cref="IRepository{Connection, Guid}"/>.
/// </summary>
public interface IConnectionRepository : IRepository<Connection, Guid>
{
    Task<Connection?> GetElementGrafByIdConnectionAsync(Guid id, CancellationToken cancellationToken);
}
