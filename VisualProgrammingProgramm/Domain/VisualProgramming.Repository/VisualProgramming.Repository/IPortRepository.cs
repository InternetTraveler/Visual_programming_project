using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Port"/> (портов).
/// </summary>
/// <remarks>
/// Наследует базовый CRUD от <see cref="IRepository{Port, Guid}"/>.
/// На данный момент не добавляет специфических методов, но может быть расширен
/// для выполнения операций поиска портов по типу, описанию или связанным узлам.
/// </remarks>
public interface IPortRepository : IRepository<Port, Guid>
{
}