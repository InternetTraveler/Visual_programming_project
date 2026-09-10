using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.NodePortConnection;

public record CreateNodePortConnectionModul(
    Guid Id,
    Guid node,
    Guid port
    ) : ICreateModul;
