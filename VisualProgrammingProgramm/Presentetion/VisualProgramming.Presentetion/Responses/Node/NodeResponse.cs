namespace VisualProgramming.WebHost.Responses.Node;

public record NodeResponse(
    Guid Id,
    string Name,
    int TypeOperation);