namespace VisualProgramming.WebHost.Responses.NodePortConnection;

public record NodePortConnectionResponse(
    Guid Id,
    Guid? node,
    Guid? port);