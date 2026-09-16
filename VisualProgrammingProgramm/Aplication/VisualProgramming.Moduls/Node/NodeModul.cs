using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Node;

public record NodeModul(
    Guid Id,
    string? name,
    int typeOperation) : IModel<Guid>;