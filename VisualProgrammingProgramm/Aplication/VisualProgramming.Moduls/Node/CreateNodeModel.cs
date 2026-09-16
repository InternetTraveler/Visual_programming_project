using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Node;

public record CreateNodeModel(
    Guid Id, 
    string? name, 
    int typeOperation) : ICreateModel;
