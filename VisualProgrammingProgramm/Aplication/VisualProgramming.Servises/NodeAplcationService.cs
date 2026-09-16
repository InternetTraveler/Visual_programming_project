using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Node;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Servises;

public class NodeApplicationService(
    IRepository<Node, Guid> repository,
    IMapper mapper)
    : INodeAplicationService
{
    public async Task<IEnumerable<NodeModul>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<NodeModul>);

    public async Task<NodeModul?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(id, cancellationToken);
        return node is null ? null : mapper.Map<NodeModul>(node);
    }

    public async Task<NodeModul?> CreateModelAsync(CreateNodeModul model, CancellationToken cancellationToken = default)
    {
        var node = new Node(
            new Name(model.name),
            (TypeOperation)model.typeOperation);

        var created = await repository.AddAsync(node, cancellationToken);
        return created is null ? null : mapper.Map<NodeModul>(created);
    }

    public async Task<bool> UpdateModelAsync(NodeModul model, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (node is null)
            return false;

        return await repository.UpdateAsync(node, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(id, cancellationToken);
        return node is null ? false : await repository.DeleteAsync(node, cancellationToken);
    }
}