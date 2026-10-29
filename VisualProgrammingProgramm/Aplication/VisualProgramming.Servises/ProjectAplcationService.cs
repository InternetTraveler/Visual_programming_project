using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Project;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="IProjectAplicationService"/> для управления проектами и связанными графами.
/// </summary>
/// <param name="repository">Репозиторий проектов.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class ProjectApplicationService(
    IRepository<Project, Guid> repository,
    ICascadeDeleteService cascade,
    IMapper mapper) : IProjectAplicationService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<ProjectModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ProjectModel>);

    /// <inheritdoc/>
    public async Task<ProjectModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await repository.GetByIdAsync(id, cancellationToken);
        return project is null ? null : mapper.Map<ProjectModel>(project);
    }

    /// <inheritdoc/>
    public async Task<ProjectModel?> CreateModelAsync(CreateProjectModel model, CancellationToken cancellationToken = default)
    {
        var project = new Project(new Name(model.name!));
        var created = await repository.AddAsync(project, cancellationToken);
        return created is null ? null : mapper.Map<ProjectModel>(created);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateModelAsync(ProjectModel model, CancellationToken cancellationToken = default)
    {
        var project = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (project is null)
            return false;

        project = mapper.Map<Project>(model);
        return await repository.UpdateAsync(project, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken ct = default)
    {
        var project = await repository.GetByIdAsync(id, ct);
        if (project is null) return false;

        await cascade.DeleteProjectAsync(id, ct);
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> AddGrafAsync(Guid projectId, CreateGrafModel model, CancellationToken ct = default)
    {
        var project = await repository.GetByIdAsync(projectId, ct);
        if (project is null) return false;

        var graf = new Graf(project);
        var added = project.AddGraf(graf);
        if (!added) return false;

        return await repository.UpdateAsync(project, ct);
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveGrafAsync(Guid projectId, Guid grafId, CancellationToken ct = default)
    {
        var project = await repository.GetByIdAsync(projectId, ct);
        if (project is null) return false;

        var graf = project.Grafs.FirstOrDefault(g => g.Id == grafId);
        if (graf is null) return false;

        project.RemoveGraf(graf);
        return await repository.UpdateAsync(project, ct);
    }
}