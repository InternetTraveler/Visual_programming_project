using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Node;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="INodeAplicationService"/> для управления узлами.
/// </summary>
/// <param name="repository">Репозиторий узлов.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class NodeApplicationService(
    IRepository<Node, Guid> repository,
    IRepository<NodePortConnection, Guid> nodePortConnectionRepository,
    ICascadeDeleteService cascade,
    IMapper mapper) : INodeAplicationService
{
    public async Task<IEnumerable<NodeModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<NodeModel>);

    public async Task<NodeModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(id, cancellationToken);
        return node is null ? null : mapper.Map<NodeModel>(node);
    }

    public async Task<NodeModel?> CreateModelAsync(CreateNodeModel model, CancellationToken cancellationToken = default)
    {
        var node = new Node(
            new Name(model.name!),
            (TypeOperation)model.typeOperation);

        var created = await repository.AddAsync(node, cancellationToken);
        return created is null ? null : mapper.Map<NodeModel>(created);
    }

    public async Task<bool> UpdateModelAsync(NodeModel model, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (node is null)
            return false;

        return await repository.UpdateAsync(node, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken ct = default)
    {
        var node = await repository.GetByIdAsync(id, ct);
        if (node is null) return false;

        await cascade.DeleteNodeAsync(id, ct);
        return true;
    }

    public async Task<IEnumerable<NodePortConnectionModel>> GetNodePortConnectionsAsync(Guid nodeId, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(nodeId, cancellationToken);
        if (node is null)
            return [];

        return node.NodePortConnections.Select(mapper.Map<NodePortConnectionModel>);
    }

    public async Task<bool> AddNodePortConnectionAsync(Guid nodeId, Guid nodePortConnectionId, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(nodeId, cancellationToken);
        if (node is null)
            return false;

        var connection = await nodePortConnectionRepository.GetByIdAsync(nodePortConnectionId, cancellationToken);
        if (connection is null)
            return false;

        if (!connection.IsContainNode(node))
            return false;

        if (!node.AddNodePortConnection(connection))
            return false;

        return await repository.UpdateAsync(node, cancellationToken);
    }

    public async Task<bool> RemoveNodePortConnectionAsync(Guid nodeId, Guid nodePortConnectionId, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(nodeId, cancellationToken);
        if (node is null)
            return false;

        var connection = node.NodePortConnections
            .FirstOrDefault(c => c.Id == nodePortConnectionId);

        if (connection is null)
            return false;

        node.RemoveNodePortConnection(connection);
        return await repository.UpdateAsync(node, cancellationToken);
    }
}