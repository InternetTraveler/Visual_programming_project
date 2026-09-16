using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.NodePortConnection;

public record NodePortConnectionModel(
    Guid Id,
    Guid node, 
    Guid port
    ) : IModel<Guid>;