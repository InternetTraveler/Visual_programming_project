using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Connection;

public record ConnectionModul(
    Guid Id,
    Guid sourcePort,
    Guid targetPort,
    Guid inElementGraf,
    Guid outElemGraf
    ) : IModel<Guid>;