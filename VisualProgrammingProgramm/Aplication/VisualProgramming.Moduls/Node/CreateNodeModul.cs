using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Node;

public record CreateNodeModul(
    Guid Id, 
    string? name, 
    int typeOperation) : ICreateModul;
