using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Project;

public record ProjectModul(
    Guid Id, 
    string? name) : IModel<Guid>;