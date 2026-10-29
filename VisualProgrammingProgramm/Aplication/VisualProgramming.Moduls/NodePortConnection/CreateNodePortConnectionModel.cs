using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.NodePortConnection;

public record CreateNodePortConnectionModel(
    Guid nodeId,
    Guid portId
    ) : ICreateModel;
