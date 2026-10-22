
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface IConnectionAplicationService : IApplicationService<ConnectionModel, CreateConnectionModel, Guid>
{
    Task<bool> IsContainsElementGrafAsync(Guid connectionId, Guid elementGrafId, CancellationToken cancellationToken = default);
}
