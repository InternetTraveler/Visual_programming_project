using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Modul;

public record ModulModel(
    Guid Id,
    string? name,
    Guid grafOPeration) : IModel<Guid>;