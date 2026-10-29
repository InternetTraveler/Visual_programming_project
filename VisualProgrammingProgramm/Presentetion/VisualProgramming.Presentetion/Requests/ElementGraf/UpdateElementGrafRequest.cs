namespace VisualProgramming.Presentetion.Requests.ElementGraf;

public record UpdateElementGrafRequest(
    Guid Id,
    Guid? BaseNodeId,
    int LevelLevelOfDepthOperation,
    bool IsModul,
    Guid? GrafId,
    double PositionX,
    double PositionY);