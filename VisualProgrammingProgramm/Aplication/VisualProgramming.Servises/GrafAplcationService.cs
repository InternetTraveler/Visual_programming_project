using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="IGrafAplicationService"/> для управления графами.
/// </summary>
/// <param name="repository">Репозиторий графов.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class GrafApplicationService(
    IRepository<Graf, Guid> repository,
    IRepository<ElementGraf, Guid> elementGrafRepository,
    IMapper mapper)
    : IGrafAplicationService
{
    public async Task<IEnumerable<GrafModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<GrafModel>);

    public async Task<GrafModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(id, cancellationToken);
        return graf is null ? null : mapper.Map<GrafModel>(graf);
    }

    public async Task<GrafModel?> CreateModelAsync(CreateGrafModel model, CancellationToken cancellationToken = default)
    {
        var graf = new Graf(mapper.Map<Project>(model.project));
        var created = await repository.AddAsync(graf, cancellationToken);
        return created is null ? null : mapper.Map<GrafModel>(created);
    }

    public async Task<bool> UpdateModelAsync(GrafModel model, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (graf is null)
            return false;

        graf.UpdateProject(mapper.Map<Project>(model.project));
        return await repository.UpdateAsync(graf, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(id, cancellationToken);
        return graf is null ? false : await repository.DeleteAsync(graf, cancellationToken);
    }

    public async Task<bool> IsContainsProjectAsync(Guid grafId, Guid projectId, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(grafId, cancellationToken);
        if (graf?.Project is null)
            return false;

        return graf.Project.Id == projectId;
    }

    public async Task<bool> AddElementAsync(Guid grafId, Guid elementGrafId, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(grafId, cancellationToken);
        if (graf is null)
            return false;

        var element = await elementGrafRepository.GetByIdAsync(elementGrafId, cancellationToken);
        if (element is null)
            return false;

        if (!element.IsContainsGraf(graf))
            return false;

        if (!graf.AddElement(element))
            return false;

        return await repository.UpdateAsync(graf, cancellationToken);
    }

    public async Task<bool> RemoveElementAsync(Guid grafId, Guid elementGrafId, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(grafId, cancellationToken);
        if (graf is null)
            return false;

        var element = graf.ElementsGraf
            .FirstOrDefault(e => e.Id == elementGrafId);

        if (element is null)
            return false;

        graf.RemoveElement(element);
        return await repository.UpdateAsync(graf, cancellationToken);
    }

    public async Task<IEnumerable<ElementGrafModel>> GetElementsAsync(Guid grafId, CancellationToken cancellationToken = default)
    {
        var graf = await repository.GetByIdAsync(grafId, cancellationToken);
        if (graf is null)
            return [];

        return graf.ElementsGraf
            .Select(mapper.Map<ElementGrafModel>);
    }
}