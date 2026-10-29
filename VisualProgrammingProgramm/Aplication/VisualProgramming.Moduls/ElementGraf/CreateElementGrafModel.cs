
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.ElementGraf;

public record CreateElementGrafModel(
    Guid baseNodeId,
    bool isModul,
    double positionX,
    double positionY,
    Guid projectID) : ICreateModel;
