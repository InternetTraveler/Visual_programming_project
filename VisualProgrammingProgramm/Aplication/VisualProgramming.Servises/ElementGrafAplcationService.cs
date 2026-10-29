using AutoMapper;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

/// <summary>
/// Реализация <see cref="IElementGrafAplicationService"/> для управления элементами графа.
/// </summary>
/// <param name="repository">Репозиторий элементов графа.</param>
/// <param name="mapper">Маппер доменных моделей и моделей приложения.</param>
public class ElementGrafApplicationService(
    IRepository<ElementGraf, Guid> repository,
    IRepository<Connection, Guid> connectionRepository,
    IRepository<Project, Guid> projectRepository,
    IRepository<Graf, Guid> grafRepository,
    IRepository<Node, Guid> nodeRepository,
    IRepository<Modul, Guid> modulRepository,
    IGraphReorganizer reorganizer,
    IMapper mapper) : IElementGrafAplicationService
{
    public async Task<IEnumerable<ElementGrafModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ElementGrafModel>);

    public async Task<ElementGrafModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(id, cancellationToken);
        return element is null ? null : mapper.Map<ElementGrafModel>(element);
    }

    public async Task<ElementGrafModel?> CreateModelAsync(CreateElementGrafModel model, CancellationToken ct = default)
    {
        var project = await projectRepository.GetByIdAsync(model.projectID, ct);
        if (project is null)
            return null;

        var graf = new Graf(project);
        BaseNode node = model.isModul ? 
                await modulRepository.GetByIdAsync(model.baseNodeId, ct) : 
                await nodeRepository.GetByIdAsync(model.baseNodeId, ct);

        if (node is null)
            return null;

        var element = new ElementGraf(
            node,
            1,
            model.isModul,
            graf,
            model.positionX,
            model.positionY);

        await grafRepository.AddAsync(graf, ct);
        var created = await repository.AddAsync(element, ct);
        return created is null ? null : mapper.Map<ElementGrafModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ElementGrafModel model, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (element is null)
            return false;

        element.UpdateNode(mapper.Map<BaseNode>(model.BaseNodeId));
        element.UpdatePosition(model.PositionX, model.PositionY);
        element.UpdateGraf(mapper.Map<Graf>(model.GrafId));
        element.UpdateLevelOfDepth(model.LevelLevelOfDepthOperation);

        return await repository.UpdateAsync(element, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken ct = default)
    {
        var element = await repository.GetByIdAsync(id, ct);
        if (element is null) return false;

        await reorganizer.RemoveElementAndRebuildAsync(id, ct);
        return true;
    }

    public async Task<bool> IsContainsGrafAsync(Guid elementGrafId, Guid grafId, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(elementGrafId, cancellationToken);
        if (element?.ParentGraf is null)
            return false;

        return element.ParentGraf.Id == grafId;
    }

    public async Task<bool> AddConnectionAsync(Guid elementGrafId, Guid connectionId, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(elementGrafId, cancellationToken);
        if (element is null)
            return false;

        var connection = await connectionRepository.GetByIdAsync(connectionId, cancellationToken);
        if (connection is null)
            return false;

        if (!connection.IsContainsElementGraf(element))
            return false;

        if (!element.AddConnection(connection))
            return false;

        return await repository.UpdateAsync(element, cancellationToken);
    }

    public async Task<bool> RemoveConnectionAsync(Guid elementGrafId, Guid connectionId, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(elementGrafId, cancellationToken);
        if (element is null)
            return false;

        var connection = element.ElementGrafConnections
            .FirstOrDefault(c => c.Id == connectionId);

        if (connection is null)
            return false;

        element.RemuveConnection(connection);
        return await repository.UpdateAsync(element, cancellationToken);
    }

    public async Task<IEnumerable<ConnectionModel>> GetConnectionsAsync(Guid elementGrafId, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(elementGrafId, cancellationToken);
        if (element is null)
            return [];

        return element.ElementGrafConnections
            .Select(mapper.Map<ConnectionModel>);
    }
}