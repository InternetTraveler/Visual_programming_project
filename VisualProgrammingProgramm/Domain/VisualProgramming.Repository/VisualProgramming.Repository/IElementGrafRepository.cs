using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;

namespace VisualProgramming.Repository;
internal interface IElementGrafRepository : IRepository<ElementGraf, Guid>
{
    Task<ElementGraf?> GetElementGrafByIdAsync(Guid id, CancellationToken cancellationToken);
}
