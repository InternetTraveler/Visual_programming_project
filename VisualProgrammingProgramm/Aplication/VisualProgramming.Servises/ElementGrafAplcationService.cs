using AutoMapper;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

public class ElementGrafApplicationService(
    IRepository<ElementGraf, Guid> repository,
    IMapper mapper)
    : IElementGrafAplicationService
{
    public async Task<IEnumerable<ElementGrafModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ElementGrafModel>);

    public async Task<ElementGrafModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(id, cancellationToken);
        return element is null ? null : mapper.Map<ElementGrafModel>(element);
    }

    public async Task<ElementGrafModel?> CreateModelAsync(CreateElementGrafModel model, CancellationToken cancellationToken = default)
    {
        var element = new ElementGraf(
            mapper.Map<BaseNode>(model.baseNode),
            model.levelLevelOfDepthOperation,
            model.isModul,
            mapper.Map<Graf>(model.graf),
            model.positionX,
            model.positionY);

        var created = await repository.AddAsync(element, cancellationToken);
        return created is null ? null : mapper.Map<ElementGrafModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ElementGrafModel model, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (element is null)
            return false;

        element.UpdateNode(mapper.Map<BaseNode>(model.baseNode));
        element.UpdatePosition(model.positionX, model.positionY);
        element.UpdateGraf(mapper.Map<Graf>(model.graf));
        element.UpdateLevelOfDepth(model.levelLevelOfDepthOperation);

        return await repository.UpdateAsync(element, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var element = await repository.GetByIdAsync(id, cancellationToken);
        return element is null ? false : await repository.DeleteAsync(element, cancellationToken);
    }
}