using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

public interface IConnectionRepository : IRepository<Connection, Guid>
{
    Task<Connection?> GetConnectionByIdAsync(Guid id, CancellationToken cancellationToken);
}
