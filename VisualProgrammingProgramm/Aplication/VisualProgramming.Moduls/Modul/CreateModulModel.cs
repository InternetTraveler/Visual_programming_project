
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Modul;

public record CreateModulModel(
    Guid Id, 
    string? name, 
    Guid grafOPeration) : ICreateModel;