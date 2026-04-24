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
    /// <summary>
    /// Асинхронно получает модуль по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор модуля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный модуль, или <c>null</c>, если модуль с указанным 
    /// идентификатором не существует.
    /// </returns>
    Task<Modul?> GetModulByIdAsync(Guid id, CancellationToken cancellationToken);
}