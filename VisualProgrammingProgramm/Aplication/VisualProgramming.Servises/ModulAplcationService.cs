using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Moduls.NodePortConnection;
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
    IRepository<NodePortConnection, Guid> nodePortConnectionRepository,
    IGraphReorganizer reorganizer,
    ICascadeDeleteService cascade,
    IMapper mapper) : IModulAplicationService
{
    public async Task<IEnumerable<ModulModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ModulModel>);

    public async Task<ModulModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(id, cancellationToken);
        return modul is null ? null : mapper.Map<ModulModel>(modul);
    }

    public async Task<ModulModel?> CreateModelAsync(CreateModulModel model, CancellationToken ct = default)
    {
        var graf = mapper.Map<Graf>(model.grafOPeration);
        var modul = new Modul(new Name(model.name!), graf);

        foreach (var port in reorganizer.GetBoundaryPorts(graf))
            await nodePortConnectionRepository.AddAsync(new NodePortConnection(modul, port), ct);

        var created = await repository.AddAsync(modul, ct);
        return created is null ? null : mapper.Map<ModulModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ModulModel model, CancellationToken ct = default)
    {
        var modul = await repository.GetByIdAsync(model.Id, ct);
        if (modul is null) return false;

        foreach (var c in modul.NodePortConnections.ToList())
        {
            modul.RemoveNodePortConnection(c);
            await nodePortConnectionRepository.DeleteAsync(c, ct);
        }

        if (modul.GrafOperation is not null)
            foreach (var port in reorganizer.GetBoundaryPorts(modul.GrafOperation))
                await nodePortConnectionRepository.AddAsync(new NodePortConnection(modul, port), ct);

        return await repository.UpdateAsync(modul, ct);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken ct = default)
    {
        var modul = await repository.GetByIdAsync(id, ct);
        if (modul is null) return false;

        await cascade.DeleteModulAsync(id, ct);
        return true;
    }

    public async Task<GrafModel?> GetGrafOperationAsync(Guid modulId, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(modulId, cancellationToken);
        if (modul?.GrafOperation is null)
            return null;

        return mapper.Map<GrafModel>(modul.GrafOperation);
    }

    public async Task<IEnumerable<NodePortConnectionModel>> GetNodePortConnectionsAsync(Guid modulId, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(modulId, cancellationToken);
        if (modul is null)
            return [];

        return modul.NodePortConnections
            .Select(mapper.Map<NodePortConnectionModel>);
    }

    public async Task<bool> AddNodePortConnectionAsync(Guid modulId, Guid nodePortConnectionId, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(modulId, cancellationToken);
        if (modul is null)
            return false;

        var connection = await nodePortConnectionRepository.GetByIdAsync(nodePortConnectionId, cancellationToken);
        if (connection is null)
            return false;

        if (!connection.IsContainNode(modul))
            return false;

        if (!modul.AddNodePortConnection(connection))
            return false;

        return await repository.UpdateAsync(modul, cancellationToken);
    }

    public async Task<bool> RemoveNodePortConnectionAsync(Guid modulId, Guid nodePortConnectionId, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(modulId, cancellationToken);
        if (modul is null)
            return false;

        var connection = modul.NodePortConnections
            .FirstOrDefault(c => c.Id == nodePortConnectionId);

        if (connection is null)
            return false;

        modul.RemoveNodePortConnection(connection);
        return await repository.UpdateAsync(modul, cancellationToken);
    }
}