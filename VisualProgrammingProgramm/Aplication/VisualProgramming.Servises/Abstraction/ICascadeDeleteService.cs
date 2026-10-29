using System;
using System.Collections.Generic;
using System.Text;

namespace VisualProgramming.Servises.Abstraction;

public interface ICascadeDeleteService
{
    // п.1
    Task DeleteProjectAsync(Guid projectId, CancellationToken ct = default);
    // п.7
    Task DeleteModulAsync(Guid modulId, CancellationToken ct = default);
    // п.8
    Task DeleteNodeAsync(Guid nodeId, CancellationToken ct = default);
    // п.9
    Task DeletePortAsync(Guid portId, CancellationToken ct = default);
    // используется из GrafApplicationService (п.1/7)
    Task DeleteGrafAsync(Guid grafId, CancellationToken ct = default);
}