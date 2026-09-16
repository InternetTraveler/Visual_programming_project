using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Modul;

public record ModulModul(
    Guid Id,
    string? name,
    Guid grafOPeration) : IModel<Guid>;