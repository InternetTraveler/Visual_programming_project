namespace VisualProgramming.Presentetion.Requests.Port;

public record UpdatePortRequest(
    Guid Id,
    Guid? NodeId,
    int TypePort,
    string? Description);