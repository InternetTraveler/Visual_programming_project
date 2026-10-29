namespace VisualProgramming.Presentetion.Responses.ElementGraf;

public record ElementGrafResponse(
    Guid Id,
    Guid BaseNodeId,
    int LevelLevelOfDepthOperation,
    bool IsModul,
    Guid? GrafId,
    double PositionX,
    double PositionY);