using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class ElementGraf : Entity<Guid>
{
    public BaseNode? Node { get; private set; }
    public LevelOfDepth LevelLevelOfDepthOperation { get; private set; }
    public bool IsModul { get; private set; }
    public Graf? ParentGraf { get; private set; }


    public double PositionX {  get; private set; }
    public double PositionY { get; private set; }

    public ElementGraf(BaseNode baseNode, int levelLevelOfDepthOperation, bool isModul,
        Graf graf, double positionX, double positionY)
        : base(Guid.NewGuid())
    {
        Node = baseNode ?? throw new Exception();
        LevelLevelOfDepthOperation = levelLevelOfDepthOperation;
        IsModul = isModul;
        ParentGraf = graf ?? throw new Exception();
        PositionX = positionX;
        PositionY = positionY;
    }

    public void UpdateNode(BaseNode node)
    {
        if(node is null)
            throw new Exception();

        Node = node;
    }

    public void UpdatePosition(double positionX, double positionY)
    {
        PositionX = positionX;
        PositionY = positionY;
    }

    public void UpdateGraf(Graf graf)
        => ParentGraf = graf ?? throw new Exception();

    public void UpdateLevelOfDepth(LevelOfDepth newLevel)
        => LevelLevelOfDepthOperation = newLevel;

    public bool IsContainsGraf(Graf graf) =>
        graf == ParentGraf;
}
