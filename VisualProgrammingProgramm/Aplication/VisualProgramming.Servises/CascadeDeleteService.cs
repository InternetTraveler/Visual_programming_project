using VisualProgramming.Domain.Entites;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises.Abstraction;

public class CascadeDeleteService(
    IRepository<Project, Guid> projectRepo,
    IRepository<Graf, Guid> grafRepo,
    IRepository<ElementGraf, Guid> elementRepo,
    IRepository<Connection, Guid> connectionRepo,
    IRepository<Modul, Guid> modulRepo,
    IRepository<NodePortConnection, Guid> npcRepo,
    IRepository<Node, Guid> nodeRepo,
    IRepository<Port, Guid> portRepo,
    IGraphReorganizer reorganizer)
    : ICascadeDeleteService
{
    // п.1
    public async Task DeleteProjectAsync(Guid projectId, CancellationToken ct = default)
    {
        var project = await projectRepo.GetByIdAsync(projectId, ct);
        if (project is null) return;

        // 1. Модули, чей GrafOperation принадлежит графам этого проекта
        var grafIds = project.Grafs.Select(g => g.Id).ToHashSet();
        var moduls = (await modulRepo.GetAllAsync(ct, true))
            .Where(m => m.GrafOperation is not null && grafIds.Contains(m.GrafOperation.Id))
            .ToList();

        foreach (var modul in moduls)
            await DeleteModulAsync(modul.Id, ct);

        // 2. Графы проекта (перечитываем — после удаления модулей набор мог измениться)
        var grafs = (await grafRepo.GetAllAsync(ct, true))
            .Where(g => g.Project?.Id == projectId)
            .ToList();

        foreach (var graf in grafs)
            await DeleteGrafAsync(graf.Id, ct);

        // 3. Сам проект
        await projectRepo.DeleteAsync(project, ct);
    }

    // п.1, 7 (общий)
    public async Task DeleteGrafAsync(Guid grafId, CancellationToken ct = default)
    {
        var graf = await grafRepo.GetByIdAsync(grafId, ct);
        if (graf is null) return;

        // 1. Элементы графа — через reorganizer (удаляет элемент, его Connection, перестраивает/удаляет граф)
        var elements = (await elementRepo.GetAllAsync(ct, true))
            .Where(e => e.ParentGraf?.Id == grafId)
            .ToList();

        foreach (var element in elements)
            await reorganizer.RemoveElementAndRebuildAsync(element.Id, ct);

        // 2. Висячие Connection, всё ещё ссылающиеся на граф через элементы
        //    (после reorganizer их быть не должно, но подстраховка — если граф удалён не полностью)
        var connectionIds = (await connectionRepo.GetAllAsync(ct, true))
            .Where(c => c.InElementGraf?.ParentGraf?.Id == grafId
                     || c.OutElementGraf?.ParentGraf?.Id == grafId)
            .Select(c => c.Id)
            .ToList();

        foreach (var connectionId in connectionIds)
        {
            var connection = await connectionRepo.GetByIdAsync(connectionId, ct);
            if (connection is not null)
                await connectionRepo.DeleteAsync(connection, ct);
        }

        // 3. Перечитываем граф — reorganizer мог его уже удалить (п.11)
        var grafAfter = await grafRepo.GetByIdAsync(grafId, ct);
        if (grafAfter is not null)
            await grafRepo.DeleteAsync(grafAfter, ct);
    }

    // п.7
    public async Task DeleteModulAsync(Guid modulId, CancellationToken ct = default)
    {
        var modul = await modulRepo.GetByIdAsync(modulId, ct);
        if (modul is null) return;

        // 1. Связи модуль-порт
        var npcs = (await npcRepo.GetAllAsync(ct, true))
            .Where(n => n.Node?.Id == modulId)
            .ToList();

        foreach (var npc in npcs)
            await npcRepo.DeleteAsync(npc, ct);

        // 2. Граф модуля (и всё внутри — элементы, соединения)
        if (modul.GrafOperation is not null)
            await DeleteGrafAsync(modul.GrafOperation.Id, ct);

        // 3. Сам модуль
        await modulRepo.DeleteAsync(modul, ct);
    }

    // п.8
    public async Task DeleteNodeAsync(Guid nodeId, CancellationToken ct = default)
    {
        var node = await nodeRepo.GetByIdAsync(nodeId, ct);
        if (node is null) return;

        // 1. Связи нода-порт
        var npcs = (await npcRepo.GetAllAsync(ct, true))
            .Where(n => n.Node?.Id == nodeId)
            .ToList();

        foreach (var npc in npcs)
            await npcRepo.DeleteAsync(npc, ct);

        // 2. Элементы графа, использующие эту ноду — через reorganizer (п.3, 8)
        var elements = (await elementRepo.GetAllAsync(ct, true))
            .Where(e => e.Node?.Id == nodeId)
            .ToList();

        foreach (var element in elements)
            await reorganizer.RemoveElementAndRebuildAsync(element.Id, ct);

        // 3. Сама нода. Порты НЕ трогаем — они переиспользуются (п.1)
        var nodeAfter = await nodeRepo.GetByIdAsync(nodeId, ct);
        if (nodeAfter is not null)
            await nodeRepo.DeleteAsync(nodeAfter, ct);
    }

    // п.9
    public async Task DeletePortAsync(Guid portId, CancellationToken ct = default)
    {
        var port = await portRepo.GetByIdAsync(portId, ct);
        if (port is null) return;

        // 1. NodePortConnection, где фигурирует этот порт (и нода, и модуль)
        var npcs = (await npcRepo.GetAllAsync(ct, true))
            .Where(n => n.Port?.Id == portId)
            .ToList();

        foreach (var npc in npcs)
            await npcRepo.DeleteAsync(npc, ct);

        // 2. Сам порт
        var portAfter = await portRepo.GetByIdAsync(portId, ct);
        if (portAfter is not null)
            await portRepo.DeleteAsync(portAfter, ct);
    }
}