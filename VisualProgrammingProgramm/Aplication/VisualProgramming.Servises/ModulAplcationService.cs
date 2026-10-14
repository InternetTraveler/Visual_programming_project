using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="IModulAplicationService"/> для управления модулями.
/// </summary>
/// <param name="repository">Репозиторий модулей.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class ModulApplicationService(
    IRepository<Modul, Guid> repository,
    IMapper mapper)
    : IModulAplicationService
{
    public async Task<IEnumerable<ModulModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ModulModel>);

    public async Task<ModulModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(id, cancellationToken);
        return modul is null ? null : mapper.Map<ModulModel>(modul);
    }

    public async Task<ModulModel?> CreateModelAsync(CreateModulModel model, CancellationToken cancellationToken = default)
    {
        var modul = new Modul(
            new Name(model.name),
            mapper.Map<Graf>(model.grafOPeration));

        var created = await repository.AddAsync(modul, cancellationToken);
        return created is null ? null : mapper.Map<ModulModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ModulModel model, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (modul is null)
            return false;

        return await repository.UpdateAsync(modul, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(id, cancellationToken);
        return modul is null ? false : await repository.DeleteAsync(modul, cancellationToken);
    }
}