using AutoMapper;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Moduls.Port;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

public class PortApplicationService(
    IRepository<Port, Guid> repository,
    IMapper mapper)
    : IPortAplicationService
{
    public async Task<IEnumerable<PortModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<PortModel>);

    public async Task<PortModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var port = await repository.GetByIdAsync(id, cancellationToken);
        return port is null ? null : mapper.Map<PortModel>(port);
    }

    public async Task<PortModel?> CreateModelAsync(CreatePortModel model, CancellationToken cancellationToken = default)
    {
        var port = new Port(
            mapper.Map<BaseNode>(model.node),
            (TypePort)model.typePort,
            model.description);

        var created = await repository.AddAsync(port, cancellationToken);
        return created is null ? null : mapper.Map<PortModel>(created);
    }

    public async Task<bool> UpdateModelAsync(PortModel model, CancellationToken cancellationToken = default)
    {
        var port = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (port is null)
            return false;

        return await repository.UpdateAsync(port, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var port = await repository.GetByIdAsync(id, cancellationToken);
        return port is null ? false : await repository.DeleteAsync(port, cancellationToken);
    }
}