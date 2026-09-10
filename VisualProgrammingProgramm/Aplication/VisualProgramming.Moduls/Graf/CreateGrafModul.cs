using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Graf;

public record CreateGrafModul(
    Guid Id, 
    Guid project) : ICreateModul;