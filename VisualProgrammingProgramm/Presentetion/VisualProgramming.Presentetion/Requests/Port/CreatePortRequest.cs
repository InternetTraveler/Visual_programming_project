namespace VisualProgramming.Presentetion.Requests.Port;

public record CreatePortRequest(
    int TypePort,
    string? Description);