namespace VisualProgramming.Presentetion.Requests.Connection;

public record CreateConnectionRequest(
    Guid? sourcePort,
    Guid? targetPort,
    Guid? inElementGraf,
    Guid? outElementGraf);
