using VisualProgramming.Domain.Entites;

namespace VisualProgramming.Servises.Abstraction;

public interface IGraphReorganizer
{
    // п.3, 11: удалить элемент и перестроить граф (разбить/удалить)
    Task RemoveElementAndRebuildAsync(Guid elementGrafId, CancellationToken ct = default);
    // п.5: объединить графы двух элементов в один
    Task MergeGraphsAsync(Guid elementAId, Guid elementBId, CancellationToken ct = default);
    // п.6: разъединить граф по удалённой связи
    Task SplitGraphByConnectionAsync(Guid connectionId, CancellationToken ct = default);
    // п.10: вычислить граничные порты графа
    IEnumerable<Port> GetBoundaryPorts(Graf graf);
}