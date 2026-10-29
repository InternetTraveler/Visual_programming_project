using VisualProgramming.WebHost.Responses.Graf;
using VisualProgramming.WebHost.Responses.NodePortConnection;

namespace VisualProgramming.Presentetion.Responses.Moduls;

public record ModulDetailsResponse(
    Guid Id,
    string Name,
    GrafResponse? GrafOperation);