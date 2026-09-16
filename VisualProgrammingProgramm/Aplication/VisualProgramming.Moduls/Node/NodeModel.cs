using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Node;

public record NodeModel(
    Guid Id,
    string? name,
    int typeOperation) : IModel<Guid>;