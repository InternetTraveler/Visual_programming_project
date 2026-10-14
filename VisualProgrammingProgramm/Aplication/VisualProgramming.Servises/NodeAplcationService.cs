using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Node;
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
    IMapper mapper)
    : INodeAplicationService
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
            new Name(model.name),
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

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var node = await repository.GetByIdAsync(id, cancellationToken);
        return node is null ? false : await repository.DeleteAsync(node, cancellationToken);
    }
}