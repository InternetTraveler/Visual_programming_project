using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Project;

public record CreateProjectModul(
    Guid Id,
    string? name) : IModel<Guid>;
