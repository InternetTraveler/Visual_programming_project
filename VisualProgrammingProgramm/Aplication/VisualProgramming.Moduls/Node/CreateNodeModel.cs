using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Node;

public record CreateNodeModel( 
    string? name, 
    int typeOperation) : ICreateModel;
