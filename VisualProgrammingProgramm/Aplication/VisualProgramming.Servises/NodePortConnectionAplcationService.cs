using AutoMapper;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

public class NodePortConnectionApplicationService(
    IRepository<NodePortConnection, Guid> repository,
    IMapper mapper)
    : INodePortConnectionAplicationService
{
    public async Task<IEnumerable<NodePortConnectionModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<NodePortConnectionModel>);

    public async Task<NodePortConnectionModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is null ? null : mapper.Map<NodePortConnectionModel>(connection);
    }

    public async Task<NodePortConnectionModel?> CreateModelAsync(CreateNodePortConnectionModel model, CancellationToken cancellationToken = default)
    {
        var connection = new NodePortConnection(
            mapper.Map<BaseNode>(model.node),
            mapper.Map<Port>(model.port));

        var created = await repository.AddAsync(connection, cancellationToken);
        return created is null ? null : mapper.Map<NodePortConnectionModel>(created);
    }

    public async Task<bool> UpdateModelAsync(NodePortConnectionModel model, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (connection is null)
            return false;

        connection.UpdateNode(mapper.Map<BaseNode>(model.node));
        connection.UpdatePort(mapper.Map<Port>(model.port));

        return await repository.UpdateAsync(connection, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is null ? false : await repository.DeleteAsync(connection, cancellationToken);
    }
}