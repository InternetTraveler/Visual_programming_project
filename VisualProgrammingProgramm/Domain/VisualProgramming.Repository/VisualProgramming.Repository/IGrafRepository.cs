using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Graf"/> (графов).
/// Наследует базовый CRUD от <see cref="IRepository{Graf, Guid}"/>.
/// </summary>
public interface IGrafRepository : IRepository<Graf, Guid>
{
    /// <summary>
    /// Асинхронно получает граф по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор графа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный граф, или <c>null</c>, если граф с указанным 
    /// идентификатором не существует.
    /// </returns>
    Task<Graf?> GetGrafByIdAsync(Guid id, CancellationToken cancellationToken);
}