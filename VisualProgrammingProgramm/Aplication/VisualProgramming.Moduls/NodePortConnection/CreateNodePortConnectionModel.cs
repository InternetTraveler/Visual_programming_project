using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.NodePortConnection;

public record CreateNodePortConnectionModel(
    Guid Id,
    Guid node,
    Guid port
    ) : ICreateModel;
