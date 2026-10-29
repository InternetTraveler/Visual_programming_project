namespace VisualProgramming.WebHost.Responses.Port;

public record PortResponse(
    Guid Id,
    int TypePort,
    string? Description);