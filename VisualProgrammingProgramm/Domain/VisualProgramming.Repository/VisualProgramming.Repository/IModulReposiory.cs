using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Modul"/> (модулей).
/// Наследует базовый CRUD от <see cref="IRepository{Modul, Guid}"/>.
/// </summary>
/// <remarks>
/// Обратите внимание на опечатку в имени интерфейса: <c>IModulReposiory</c>.
/// Рекомендуется переименовать в <c>IModulRepository</c>.
/// </remarks>
public interface IModulReposiory : IRepository<Modul, Guid>
{
    Task<Graf?> GetGrafByIdModulAsync(Guid id, CancellationToken cancellationToken);
    Task<Port?> GetPortByIdModulAsync(Guid id, CancellationToken cancellationToken);
}