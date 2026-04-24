using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Project"/> (проектов).
/// Наследует базовый CRUD от <see cref="IRepository{Project, Guid}"/>.
/// </summary>
public interface IProjectRepository : IRepository<Project, Guid>
{
    /// <summary>
    /// Асинхронно получает проект по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор проекта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный проект, или <c>null</c>, если проект с указанным идентификатором не существует.
    /// </returns>
    Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
}