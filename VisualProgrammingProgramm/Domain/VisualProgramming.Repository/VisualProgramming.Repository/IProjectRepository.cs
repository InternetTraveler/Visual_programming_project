using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

/// <summary>
/// Репозиторий для работы с сущностями <see cref="Project"/> (проектов).
/// Наследует базовый CRUD от <see cref="IRepository{Project, Guid}"/>.
/// </summary>
public interface IProjectRepository : IRepository<Project, Guid>
{
    Task<Graf?> GetGrafByIdProjectAsync(Guid id, CancellationToken cancellationToken);
}