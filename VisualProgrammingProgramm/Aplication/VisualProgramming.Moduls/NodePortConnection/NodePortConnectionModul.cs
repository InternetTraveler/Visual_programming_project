using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.NodePortConnection;

public record NodePortConnectionModul(
    Guid Id,
    Guid node, 
    Guid port
    ) : IModel<Guid>;