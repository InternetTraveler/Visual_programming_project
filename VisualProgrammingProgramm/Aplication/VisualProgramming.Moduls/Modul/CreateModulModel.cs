
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Modul;

public record CreateModulModel(
    string? name, 
    Guid grafOPeration) : ICreateModel;