using AutoMapper;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="INodePortConnectionAplicationService"/> для управления связями узел–порт.
/// </summary>
/// <param name="repository">Репозиторий связей узел–порт.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class NodePortConnectionApplicationService(
    IRepository<NodePortConnection, Guid> repository,
    INodeRepository nodeRepository,
    IPortRepository portRepository,
    IMapper mapper)
    : INodePortConnectionAplicationService
{
    /// <summary>
    /// Единый способ построения модели связи — такой же, как при создании.
    /// </summary>
    private static NodePortConnectionModel? ToModel(NodePortConnection connection)
    {
        if (connection?.Node is null || connection.Port is null)
            return null;

        return new(connection.Id, connection.Node.Id, connection.Port.Id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<NodePortConnectionModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<NodePortConnectionModel>);

    /// <inheritdoc/>
    public async Task<NodePortConnectionModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is null ? null : mapper.Map<NodePortConnectionModel>(connection);
    }

    /// <inheritdoc/>
    public async Task<NodePortConnectionModel?> CreateModelAsync(CreateNodePortConnectionModel model, CancellationToken cancellationToken = default)
    {
        var node = await nodeRepository.GetByIdAsync(model.nodeId, cancellationToken);
        var port = await portRepository.GetByIdAsync(model.portId, cancellationToken);

        if (node is null || port is null)
            return null;

        var connection = new NodePortConnection(node, port);

        var created = await repository.AddAsync(connection, cancellationToken);

        return created is null ? null : ToModel(created);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateModelAsync(NodePortConnectionModel model, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (connection is null)
            return false;

        connection.UpdateNode(mapper.Map<BaseNode>(model.node));
        connection.UpdatePort(mapper.Map<Port>(model.port));

        return await repository.UpdateAsync(connection, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is not null && await repository.DeleteAsync(connection, cancellationToken);
    }

    public async Task<bool> IsContainNodeAsync(Guid connectionId, Guid nodeId, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(connectionId, cancellationToken);
        if (connection?.Node is null)
            return false;

        return connection.Node.Id == nodeId;
    }

    public async Task<bool> IsContainPortAsync(Guid connectionId, Guid portId, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(connectionId, cancellationToken);
        if (connection?.Port is null)
            return false;

        return connection.Port.Id == portId;
    }

    public async Task<IEnumerable<NodePortConnectionModel>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken = default)
    {
        var connections = await repository.GetAllAsync(cancellationToken, true);
        return connections
            .Where(c => c.Node?.Id == nodeId)
            .Select(mapper.Map<NodePortConnectionModel>);
    }

    public async Task<IEnumerable<NodePortConnectionModel>> GetByPortIdAsync(Guid portId, CancellationToken cancellationToken = default)
    {
        var connections = await repository.GetAllAsync(cancellationToken, true);
        return connections
            .Where(c => c.Port?.Id == portId)
            .Select(mapper.Map<NodePortConnectionModel>);
    }
}