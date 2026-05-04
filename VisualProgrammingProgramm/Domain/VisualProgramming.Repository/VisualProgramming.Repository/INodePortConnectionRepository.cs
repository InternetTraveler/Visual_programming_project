using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы со связями "узел-порт".
/// Наследует базовый CRUD от <see cref="IRepository{Connection, Guid}"/>.
/// </summary>
/// <remarks>
/// В отличие от <see cref="IConnectionRepository"/>, данный интерфейс не добавляет
/// собственных методов, но может быть расширен в будущем для специфических операций
/// над связями между узлами и портами.
/// </remarks>
public interface INodePortConnectionRepository : IRepository<Connection, Guid>
{
    Task<BaseNode?> GetNodeAndModulByIdNodePortConnectionAsync
        (Guid id, CancellationToken cancellationToken);
    Task<Port?> GetPortByIdNodePortConnectionAsync(Guid id, CancellationToken cancellationToken);
}