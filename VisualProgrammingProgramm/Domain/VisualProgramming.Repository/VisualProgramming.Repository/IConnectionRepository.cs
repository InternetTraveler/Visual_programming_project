using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

internal interface IConnectionRepository : IRepository<Connection, Guid>
{
    Task<Connection?> GetConnectionByIdAsync(Guid id, CancellationToken cancellationToken);
}
