using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

public interface IProjectRepository : IRepository<Project, Guid>
{
    Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
}
