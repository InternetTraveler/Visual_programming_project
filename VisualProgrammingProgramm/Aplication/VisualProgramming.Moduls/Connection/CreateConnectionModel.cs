
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Connection;

public record CreateConnectionModel(
    Guid Id, 
    Guid sourcePort, 
    Guid targetPort, 
    Guid inElementGraf, 
    Guid outElemGraf
    ) : ICreateModel;