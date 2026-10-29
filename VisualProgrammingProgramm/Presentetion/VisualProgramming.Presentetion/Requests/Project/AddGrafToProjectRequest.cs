using VisualProgramming.Presentetion.Requests.Graf;

namespace VisualProgramming.Presentetion.Requests.Project;

public record AddGrafToProjectRequest(Guid ProjectId, CreateGrafRequest Graf);