namespace VisualProgramming.WebHost.Responses.Connection;

public record ConnectionResponse(
    Guid Id,
    Guid? sourcePortId,
    Guid? targetPortId,
    Guid? inElementGrafId,
    Guid? outElementGrafId);