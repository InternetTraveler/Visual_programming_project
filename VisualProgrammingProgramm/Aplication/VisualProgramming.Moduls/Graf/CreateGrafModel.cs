using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Graf;

public record CreateGrafModel(
    Guid project) : ICreateModel;