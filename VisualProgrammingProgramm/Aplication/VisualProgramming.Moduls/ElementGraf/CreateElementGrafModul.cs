
using VisualProgramming.Moduls.Base;

namespace VisualProgramming.Moduls.ElementGraf;

public record CreateElementGrafModul(
    Guid Id,
    Guid baseNode,
    int levelLevelOfDepthOperation,
    bool isModul,
    Guid graf,
    double positionX,
    double positionY) : ICreateModul;
