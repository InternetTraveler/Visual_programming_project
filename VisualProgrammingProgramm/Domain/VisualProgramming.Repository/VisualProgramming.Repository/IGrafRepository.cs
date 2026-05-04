using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Graf"/> (графов).
/// Наследует базовый CRUD от <see cref="IRepository{Graf, Guid}"/>.
/// </summary>
public interface IGrafRepository : IRepository<Graf, Guid>
{
    Task<ElementGraf?> GetElementGafByIdGrafAsync(Guid id, CancellationToken cancellationToken);
}