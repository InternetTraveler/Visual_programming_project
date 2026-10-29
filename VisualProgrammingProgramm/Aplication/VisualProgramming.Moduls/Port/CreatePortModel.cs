using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.Port;

public record CreatePortModel(
    int typePort,
    string description) : ICreateModel;