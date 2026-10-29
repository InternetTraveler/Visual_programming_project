using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.ElementGraf;

public record ElementGrafModel(
    Guid Id,
    Guid BaseNodeId,
    int LevelLevelOfDepthOperation,
    bool IsModul,
    Guid? GrafId,
    double PositionX,
    double PositionY
) : IModel<Guid>;