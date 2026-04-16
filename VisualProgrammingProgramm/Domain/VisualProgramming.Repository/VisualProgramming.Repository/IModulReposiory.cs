using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

public interface IModulReposiory : IRepository<Modul, Guid>
{
    Task<Modul?> GetModulByIdAsync(Guid id, CancellationToken cancellationToken);
}
