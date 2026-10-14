
using VisualProgramming.Moduls.Port;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface IPortAplicationService : IApplicationService<PortModel, CreatePortModel, Guid>
{
    // Получение всех портов узла
    Task<IEnumerable<PortModel>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken = default);
}
