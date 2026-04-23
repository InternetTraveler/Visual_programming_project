using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Migrations;
using VisualProgramming.Repository;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfModulRepository(ApplicationDbContext context)
    : EfRepository<Modul, Guid>(context), IModulReposiory
{
    private readonly DbSet<Modul> _moduls = context.Set<Modul>();

    public Task<Modul?> GetModulByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
