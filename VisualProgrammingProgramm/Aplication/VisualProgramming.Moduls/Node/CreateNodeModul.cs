using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Node;

public record CreateModulModul(
    Guid Id, 
    string? name, 
    int typeOperation) : ICreateModul;
