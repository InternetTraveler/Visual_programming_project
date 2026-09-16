using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Servises;

public class ModulApplicationService(
    IRepository<Modul, Guid> repository,
    IMapper mapper)
    : IModulAplicationService
{
    public async Task<IEnumerable<ModulModul>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ModulModul>);

    public async Task<ModulModul?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(id, cancellationToken);
        return modul is null ? null : mapper.Map<ModulModul>(modul);
    }

    public async Task<ModulModul?> CreateModelAsync(CreateModulModul model, CancellationToken cancellationToken = default)
    {
        var modul = new Modul(
            new Name(model.name),
            mapper.Map<Graf>(model.grafOPeration));

        var created = await repository.AddAsync(modul, cancellationToken);
        return created is null ? null : mapper.Map<ModulModul>(created);
    }

    public async Task<bool> UpdateModelAsync(ModulModul model, CancellationToken cancellationToken = default)
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