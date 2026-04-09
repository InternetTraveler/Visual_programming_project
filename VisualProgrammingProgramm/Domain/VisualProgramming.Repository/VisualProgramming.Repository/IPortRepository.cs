using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

internal interface IPortRepository : IRepository<Port, Guid>
{
    Task<Port?> GetPortByIdAsync(Guid id, CancellationToken cancellationToken);
}
