using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Port;

public record PortModul(
    Guid Id, 
    Guid node, 
    int typePort, 
    string description) : IModel<Guid>;