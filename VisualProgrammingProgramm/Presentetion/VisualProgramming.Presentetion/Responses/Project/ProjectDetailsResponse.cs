using VisualProgramming.WebHost.Responses.Graf;

namespace VisualProgramming.Presentetion.Responses.Project;

public record ProjectDetailsResponse(
    Guid Id,
    string Name);