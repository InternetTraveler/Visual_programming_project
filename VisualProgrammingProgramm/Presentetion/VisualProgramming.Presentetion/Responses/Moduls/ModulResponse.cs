namespace VisualProgramming.WebHost.Responses.Modul;

public record ModulResponse(
    Guid Id,
    string name,
    Guid? grafOPeration);