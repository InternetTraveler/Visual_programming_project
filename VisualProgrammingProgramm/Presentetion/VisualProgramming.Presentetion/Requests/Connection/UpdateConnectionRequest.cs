namespace VisualProgramming.Presentetion.Requests.Connection;

public record UpdateConnectionRequest(
    Guid Id,
    Guid? SourcePortId,
    Guid? TargetPortId,
    Guid? InElementGrafId,
    Guid? OutElementGrafId);