using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Project;

public record ProjectModel(
    Guid Id, 
    string? name) : IModel<Guid>;