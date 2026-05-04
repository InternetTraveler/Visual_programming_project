using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Port"/> (портов).
/// Наследует базовый CRUD от <see cref="IRepository{Port, Guid}"/>.
/// </summary>
public interface IPortRepository : IRepository<Port, Guid>
{
}