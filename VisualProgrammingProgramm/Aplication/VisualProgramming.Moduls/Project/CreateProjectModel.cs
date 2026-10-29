using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Project;

public record CreateProjectModel(
    string name) : ICreateModel;
