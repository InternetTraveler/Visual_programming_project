using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Project;

public record CreateProjectModel(
    Guid Id,
    string? name) : ICreateModel;
