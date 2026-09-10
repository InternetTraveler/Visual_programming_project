
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Graf;

public record GrafModul(
    Guid Id,
    Guid project) : IModel<Guid>;
