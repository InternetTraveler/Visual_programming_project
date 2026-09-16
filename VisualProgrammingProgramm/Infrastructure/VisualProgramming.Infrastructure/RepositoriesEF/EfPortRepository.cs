using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository;

namespace VisualProgramming.Infrastructure.RepositoriesEF;

public class EfPortRepository(ApplicationDbContext context) :
    EfRepository<Port, Guid>(context), IPortRepository
{
    private readonly DbSet<Port> _ports = context.Set<Port>();
}
