namespace VisualProgramming.Presentetion.Requests.ElementGraf;

public record CreateElementGrafRequest(
    Guid? baseNodeId,
    bool isModul,
    double positionX,
    double positionY,
    Guid? projectID);