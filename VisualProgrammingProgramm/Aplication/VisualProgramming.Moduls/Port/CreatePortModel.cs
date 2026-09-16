using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Port;

public record CreatePortModel(
    Guid Id,
    Guid node,
    int typePort,
    string description) : ICreateModel;