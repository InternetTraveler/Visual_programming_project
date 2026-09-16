using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

public class ConnectionApplicationService(
    IRepository<Connection, Guid> repository,
    IMapper mapper)
    : IConnectionAplicationService
{
    public async Task<IEnumerable<ConnectionModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
        .Select(mapper.Map<ConnectionModel>);

    public async Task<ConnectionModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is null ? null : mapper.Map<ConnectionModel>(connection);
    }

    public async Task<ConnectionModel?> CreateModelAsync(CreateConnectionModel model, CancellationToken cancellationToken = default)
    {
        var connection = new Connection(
            mapper.Map<Port>(model.sourcePort),
            mapper.Map<Port>(model.targetPort),
            mapper.Map<ElementGraf>(model.inElementGraf),
            mapper.Map<ElementGraf>(model.outElemGraf));

        var created = await repository.AddAsync(connection, cancellationToken);
        return created is null ? null : mapper.Map<ConnectionModel>(created);
    }

    public async Task<bool> UpdateModelAsync(ConnectionModel model, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(model.Id, cancellationToken);
        if (connection is null)
            return false;

        connection.UpdateInConnection(
            mapper.Map<ElementGraf>(model.inElementGraf),
            mapper.Map<Port>(model.sourcePort));

        connection.UpdateOutConnection(
            mapper.Map<ElementGraf>(model.outElemGraf),
            mapper.Map<Port>(model.targetPort));

        return await repository.UpdateAsync(connection, cancellationToken);
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var connection = await repository.GetByIdAsync(id, cancellationToken);
        return connection is null ? false : await repository.DeleteAsync(connection, cancellationToken);
    }
}