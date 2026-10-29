using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Port;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="IPortAplicationService"/> для управления портами.
/// </summary>
/// <param name="repository">Репозиторий портов.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class PortApplicationService(
    IRepository<Port, Guid> repository,
    INodePortConnectionRepository nodePortConnectionRepository,
    ICascadeDeleteService cascade,
    IMapper mapper) : IPortAplicationService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<PortModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<PortModel>);

    /// <inheritdoc/>
    public async Task<PortModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var port = await repository.GetByIdAsync(id, cancellationToken);
        return port is null ? null : mapper.Map<PortModel>(port);
    }

    /// <inheritdoc/>
    public async Task<PortModel?> CreateModelAsync(CreatePortModel model, CancellationToken cancellationToken = default)
    {
        var port = new Port(
            (TypePort)model.typePort,
            model.description);

        var created = await repository.AddAsync(port, cancellationToken);
        return created is null ? null : mapper.Map<PortModel>(created);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateModelAsync(PortModel model, CancellationToken cancellationToken = default)
    {
        var port = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (port is null)
            return false;

        port = mapper.Map<Port>(model);
        return await repository.UpdateAsync(port, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken ct = default)
    {
        var port = await repository.GetByIdAsync(id, ct);
        if (port is null) return false;

        await cascade.DeletePortAsync(id, ct);
        return true;
    }

    public async Task<IEnumerable<PortModel>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken = default)
    {
        var connections = await nodePortConnectionRepository.GetByNodeIdAsync(nodeId, cancellationToken);

        var portIds = connections
            .Where(c => c.Port is not null)
            .Select(c => c.Port!.Id)
            .Distinct()
            .ToHashSet();

        if (portIds.Count == 0)
            return [];

        var ports = await repository.GetAllAsync(cancellationToken, true);

        return ports
            .Where(p => portIds.Contains(p.Id))
            .Select(mapper.Map<PortModel>);
    }
}