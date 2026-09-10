using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Modul;

public record CreateModulModul(
    Guid Id,
    string? name,
    Guid grafOPeration) : IModel<Guid>;