using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Project;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Servises;

public class ProjectApplicationService(
    IRepository<Project, Guid> repository,
    IMapper mapper)
    : IProjectAplicationService
{
    public async Task<IEnumerable<ProjectModul>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ProjectModul>);

    public async Task<ProjectModul?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await repository.GetByIdAsync(id, cancellationToken);
        return project is null ? null : mapper.Map<ProjectModul>(project);
    }

    public async Task<ProjectModul?> CreateModelAsync(CreateProjectModul model, CancellationToken cancellationToken = default)
    {
        var project = new Project(new Name(model.name));
        var created = await repository.AddAsync(project, cancellationToken);
        return created is null ? null : mapper.Map<ProjectModul>(created);
    }

    public async Task<bool> UpdateModelAsync(ProjectModul model, CancellationToken cancellationToken = default)
    {
        var project = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (project is null)
            return false;

        return await repository.UpdateAsync(project, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await repository.GetByIdAsync(id, cancellationToken);
        return project is null ? false : await repository.DeleteAsync(project, cancellationToken);
    }
}