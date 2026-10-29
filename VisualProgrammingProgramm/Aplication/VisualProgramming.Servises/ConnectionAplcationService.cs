using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="IConnectionAplicationService"/> для управления соединениями.
/// </summary>
/// <param name="repository">Репозиторий соединений.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class ConnectionApplicationService(
    IRepository<Connection, Guid> repository,
    IGraphReorganizer reorganizer,
    IMapper mapper) : IConnectionAplicationService
{
    public async Task<IEnumerable<ConnectionModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ConnectionModel>);

    public async Task<ConnectionModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is null ? null : mapper.Map<ConnectionModel>(connection);
    }

    public async Task<ConnectionModel?> CreateModelAsync(CreateConnectionModel model, CancellationToken ct = default)
    {
        var connection = new Connection(
            mapper.Map<Port>(model.sourcePort),
            mapper.Map<Port>(model.targetPort),
            mapper.Map<ElementGraf>(model.inElementGraf),
            mapper.Map<ElementGraf>(model.outElemGraf));

        var created = await repository.AddAsync(connection, ct);
        if (created is null) 
            return null;

        // п.5: объединить графы двух элементов
        await reorganizer.MergeGraphsAsync(created.InElementGraf!.Id, created.OutElementGraf!.Id, ct);
        return mapper.Map<ConnectionModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ConnectionModel model, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (connection is null)
            return false;

        connection.UpdateInConnection(
            mapper.Map<ElementGraf>(model.inElementGrafId),
            mapper.Map<Port>(model.sourcePortId));

        connection.UpdateOutConnection(
            mapper.Map<ElementGraf>(model.outElementGrafId),
            mapper.Map<Port>(model.targetPortId));

        return await repository.UpdateAsync(connection, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken ct = default)
    {
        var connection = await repository.GetByIdAsync(id, ct);
        if (connection is null) 
            return false;

        await reorganizer.SplitGraphByConnectionAsync(id, ct);
        return await repository.DeleteAsync(connection, ct);
    }

    public async Task<bool> IsContainsElementGrafAsync(Guid connectionId, Guid elementGrafId, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(connectionId, cancellationToken);
        if (connection is null)
            return false;

        return connection.InElementGraf?.Id == elementGrafId
            || connection.OutElementGraf?.Id == elementGrafId;
    }
}