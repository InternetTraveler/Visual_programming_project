using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Port;

public record PortModel(
    Guid Id,
    int typePort,
    string? description
) : IModel<Guid>;