
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Connection;

public record CreateConnectionModul(
    Guid Id, 
    Guid sourcePort, 
    Guid targetPort, 
    Guid inElementGraf, 
    Guid outElemGraf
    ) : ICreateModul;