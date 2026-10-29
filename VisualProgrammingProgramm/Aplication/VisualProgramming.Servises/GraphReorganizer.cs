using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

namespace VisualProgramming.Servises;

public class GraphReorganizer(
    IRepository<Graf, Guid> grafRepo,
    IRepository<ElementGraf, Guid> elementRepo,
    IRepository<Connection, Guid> connectionRepo)
    : IGraphReorganizer
{
    // п.3, 11
    public async Task RemoveElementAndRebuildAsync(Guid elementGrafId, CancellationToken ct = default)
    {
        var element = await elementRepo.GetByIdAsync(elementGrafId, ct);
        if (element is null) return;

        var graf = element.ParentGraf;

        // 1. удалить все Connection, где участвует элемент
        var connections = (await connectionRepo.GetAllAsync(ct, true))
            .Where(c => c.InElementGraf?.Id == elementGrafId || c.OutElementGraf?.Id == elementGrafId)
            .ToList();

        foreach (var c in connections)
        {
            var tracked = await connectionRepo.GetByIdAsync(c.Id, ct);
            if (tracked is not null) await connectionRepo.DeleteAsync(tracked, ct);
        }

        // 2. удалить сам элемент
        graf?.RemoveElement(element);
        await elementRepo.DeleteAsync(element, ct);

        if (graf is null) return;

        // 3. п.11: если элементов не осталось — удалить граф
        var remaining = graf.ElementsGraf.ToList();
        if (remaining.Count == 0)
        {
            await grafRepo.DeleteAsync(graf, ct);
            return;
        }

        // 4. п.3: разбить на компоненты связности
        await SplitIntoComponentsAsync(graf, remaining, ct);
    }

    // п.5
    public async Task MergeGraphsAsync(Guid elementAId, Guid elementBId, CancellationToken ct = default)
    {
        var a = await elementRepo.GetByIdAsync(elementAId, ct);
        var b = await elementRepo.GetByIdAsync(elementBId, ct);
        if (a?.ParentGraf is null || b?.ParentGraf is null) return;

        var target = a.ParentGraf;
        var source = b.ParentGraf;

        // уже в одном графе — ничего не делаем
        if (target.Id == source.Id) return;

        // переносим все элементы source в target
        foreach (var e in source.ElementsGraf.ToList())
        {
            source.RemoveElement(e);
            e.UpdateGraf(target);
            target.AddElement(e);
        }

        await grafRepo.UpdateAsync(target, ct);
        await grafRepo.DeleteAsync(source, ct);
    }

    // п.6
    public async Task SplitGraphByConnectionAsync(Guid connectionId, CancellationToken ct = default)
    {
        var connection = await connectionRepo.GetByIdAsync(connectionId, ct);
        var graf = connection?.InElementGraf?.ParentGraf ?? connection?.OutElementGraf?.ParentGraf;
        if (graf is null) return;

        var remaining = graf.ElementsGraf.ToList();
        await SplitIntoComponentsAsync(graf, remaining, ct);
    }

    // п.10
    public IEnumerable<Port> GetBoundaryPorts(Graf graf)
    {
        var elements = graf.ElementsGraf.ToList();
        if (elements.Count == 0) return [];

        var firstDepth = elements.Min(e => e.LevelLevelOfDepthOperation);

        var withIncoming = elements
            .SelectMany(e => e.ElementGrafConnections)
            .Where(c => c.OutElementGraf is not null)
            .Select(c => c.OutElementGraf!.Id)
            .ToHashSet();

        var withOutgoing = elements
            .SelectMany(e => e.ElementGrafConnections)
            .Where(c => c.InElementGraf is not null)
            .Select(c => c.InElementGraf!.Id)
            .ToHashSet();

        var inputs = elements
            .Where(e => !withIncoming.Contains(e.Id) && e.LevelLevelOfDepthOperation == firstDepth)
            .SelectMany(e => e.Node!.NodePortConnections)
            .Where(c => c.Port?.TypePort == TypePort.INPUT)
            .Select(c => c.Port!);

        var outputs = elements
            .Where(e => !withOutgoing.Contains(e.Id))
            .SelectMany(e => e.Node!.NodePortConnections)
            .Where(c => c.Port?.TypePort == TypePort.OUTPUT)
            .Select(c => c.Port!);

        return inputs.Concat(outputs).DistinctBy(p => p.Id);
    }

    // общий обход компонент связности (п.3, 6)
    private async Task SplitIntoComponentsAsync(Graf graf, List<ElementGraf> elements, CancellationToken ct)
    {
        var graphEdges = elements
            .SelectMany(e => e.ElementGrafConnections)
            .Where(c => c.InElementGraf is not null && c.OutElementGraf is not null)
            .Select(c => (From: c.InElementGraf!.Id, To: c.OutElementGraf!.Id))
            .ToList();

        var adjacency = elements.ToDictionary(
            e => e.Id,
            e => graphEdges
                .Where(edge => edge.From == e.Id || edge.To == e.Id)
                .Select(edge => edge.From == e.Id ? edge.To : edge.From)
                .ToHashSet());

        var visited = new HashSet<Guid>();
        var components = new List<List<ElementGraf>>();

        foreach (var element in elements)
        {
            if (visited.Contains(element.Id)) continue;

            var component = new List<ElementGraf>();
            var stack = new Stack<ElementGraf>();
            stack.Push(element);
            visited.Add(element.Id);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                component.Add(current);

                foreach (var neighborId in adjacency[current.Id])
                {
                    if (visited.Add(neighborId))
                        stack.Push(elements.First(e => e.Id == neighborId));
                }
            }

            components.Add(component);
        }

        // одна компонента — граф не разбился
        if (components.Count <= 1) return;

        // первая компонента остаётся в исходном графе, остальные — в новых
        for (var i = 1; i < components.Count; i++)
        {
            var newGraf = new Graf(graf.Project);
            await grafRepo.AddAsync(newGraf, ct);

            foreach (var e in components[i])
            {
                graf.RemoveElement(e);
                e.UpdateGraf(newGraf);
                newGraf.AddElement(e);
            }

            await grafRepo.UpdateAsync(newGraf, ct);
        }

        await grafRepo.UpdateAsync(graf, ct);
    }
}