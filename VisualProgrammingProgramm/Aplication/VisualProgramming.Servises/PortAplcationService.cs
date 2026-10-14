using AutoMapper;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Port;
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
    IMapper mapper)
    : IPortAplicationService
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
            mapper.Map<BaseNode>(model.node),
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
    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var port = await repository.GetByIdAsync(id, cancellationToken);
        return port is not null && await repository.DeleteAsync(port, cancellationToken);
    }

    public async Task<IEnumerable<PortModel>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken = default)
    {
        var ports = await repository.GetAllAsync(cancellationToken, true);
        return ports
            .Where(p => p.Node?.Id == nodeId)
            .Select(mapper.Map<PortModel>);
    }
}