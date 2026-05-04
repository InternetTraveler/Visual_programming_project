using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="ElementGraf"/> (элементов графа).
/// Наследует базовый CRUD от <see cref="IRepository{ElementGraf, Guid}"/>.
/// </summary>
public interface IElementGrafRepository : IRepository<ElementGraf, Guid>
{
    Task<Port?> GetPortByIdElementGrafAsync(Guid id, CancellationToken cancellationToken);
}