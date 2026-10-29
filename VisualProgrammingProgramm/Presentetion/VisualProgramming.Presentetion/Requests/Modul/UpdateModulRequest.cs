namespace VisualProgramming.Presentetion.Requests.Modul;

public record UpdateModulRequest(Guid Id, string Name, Guid? GrafId);