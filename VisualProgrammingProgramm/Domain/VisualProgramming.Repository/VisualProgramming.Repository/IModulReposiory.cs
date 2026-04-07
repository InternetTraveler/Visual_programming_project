using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;

internal interface IModulReposiory : IRepository<Modul, Guid>
{
    Task<Modul?> GetModulByIdAsync(Guid id, CancellationToken cancellationToken);
}
