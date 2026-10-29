
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Graf;

public record GrafModel(
    Guid Id,
    Guid projectId
) : IModel<Guid>;