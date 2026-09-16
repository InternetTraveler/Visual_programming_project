using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

public class GrafApplicationService(
    IRepository<Graf, Guid> repository,
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
}