using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Graf;

public record CreateGrafModel(
    Guid Id, 
    Guid project) : ICreateModel;