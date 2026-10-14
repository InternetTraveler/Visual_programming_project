using AutoMapper;
using System.Reflection;
using System.Xml.Linq;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Repository;
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
        var graf = mapper.Map<Graf>(model.grafOPeration);
        var modul = new Modul(new Name(model.name!), graf);

        var elements = graf.ElementsGraf.ToList();
        var firstDepth = elements.Min(e => e.LevelLevelOfDepthOperation);
        var withIncoming = elements.SelectMany(e => e.ElementGrafConnections)
            .Where(c => c.OutElementGraf is not null).Select(c => c.OutElementGraf!.Id).ToHashSet();
        var withOutgoing = elements.SelectMany(e => e.ElementGrafConnections)
            .Where(c => c.InElementGraf is not null).Select(c => c.InElementGraf!.Id).ToHashSet();

        var boundaryPorts = elements
            .Where(e => !withIncoming.Contains(e.Id) && e.LevelLevelOfDepthOperation == firstDepth)
            .SelectMany(e => e.Node!.NodePortConnections)
            .Where(c => c.Port?.TypePort == TypePort.OUTPUT)
            .Select(c => c.Port!)
            .Concat(elements
                .Where(e => !withOutgoing.Contains(e.Id))
                .SelectMany(e => e.Node!.NodePortConnections)
                .Where(c => c.Port?.TypePort == TypePort.INPUT)
                .Select(c => c.Port!));

        foreach (var port in boundaryPorts)
            await nodePortConnectionRepository.AddAsync(new NodePortConnection(modul, port), cancellationToken);

        var created = await repository.AddAsync(modul, cancellationToken);
        return created is null ? null : mapper.Map<ModulModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ModulModel model, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (modul is null)
            return false;

        foreach (var connection in modul.NodePortConnections.ToList())
        {
            modul.RemoveNodePortConnection(connection);
            await nodePortConnectionRepository.DeleteAsync(connection, cancellationToken);
        }

        if (modul.GrafOperation is not null)
        {
            var elements = modul.GrafOperation.ElementsGraf.ToList();
            var firstDepth = elements.Min(e => e.LevelLevelOfDepthOperation);
            var withIncoming = elements.SelectMany(e => e.ElementGrafConnections)
                .Where(c => c.OutElementGraf is not null).Select(c => c.OutElementGraf!.Id).ToHashSet();
            var withOutgoing = elements.SelectMany(e => e.ElementGrafConnections)
                .Where(c => c.InElementGraf is not null).Select(c => c.InElementGraf!.Id).ToHashSet();

            var boundaryPorts = elements
                .Where(e => !withIncoming.Contains(e.Id) && e.LevelLevelOfDepthOperation == firstDepth)
                .SelectMany(e => e.Node!.NodePortConnections)
                .Where(c => c.Port?.TypePort == TypePort.OUTPUT)
                .Select(c => c.Port!)
                .Concat(elements
                    .Where(e => !withOutgoing.Contains(e.Id))
                    .SelectMany(e => e.Node!.NodePortConnections)
                    .Where(c => c.Port?.TypePort == TypePort.INPUT)
                    .Select(c => c.Port!));

            foreach (var port in boundaryPorts)
                await nodePortConnectionRepository.AddAsync(new NodePortConnection(modul, port), cancellationToken);
        }

        return await repository.UpdateAsync(modul, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var modul = await repository.GetByIdAsync(id, cancellationToken);
        return modul is not null && await repository.DeleteAsync(modul, cancellationToken);
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