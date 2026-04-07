using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

internal interface IGrafRepository : IRepository<Graf, Guid>
{
    Task<Graf?> GetGrafByIdAsync(Guid id, CancellationToken cancellationToken);
}
