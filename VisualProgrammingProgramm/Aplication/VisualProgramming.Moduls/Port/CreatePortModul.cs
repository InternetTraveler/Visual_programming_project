using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Port;

public record CreatePortModul(
    Guid Id,
    Guid node,
    int typePort,
    string description) : ICreateModul;