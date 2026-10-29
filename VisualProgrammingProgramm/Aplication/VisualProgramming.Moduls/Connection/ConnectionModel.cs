using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Connection;

public record ConnectionModel(
    Guid Id,
    Guid? sourcePortId,
    Guid? targetPortId,
    Guid? inElementGrafId,
    Guid? outElementGrafId
) : IModel<Guid>;