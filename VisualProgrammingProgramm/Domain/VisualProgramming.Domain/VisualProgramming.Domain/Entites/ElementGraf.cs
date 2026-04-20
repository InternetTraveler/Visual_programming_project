using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;
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

    private ICollection<Connection> _elementGrafConnection = [];
    public IReadOnlyCollection<Connection> ElementGrafConnections 
        => _elementGrafConnection.ToList().AsReadOnly();

    public ElementGraf(BaseNode baseNode, LevelOfDepth levelLevelOfDepthOperation, bool isModul,
        Graf graf, double positionX, double positionY)
        : base(Guid.NewGuid())
    {
        Node = baseNode ?? throw new ElementGrafNullExeption(this, nameof(baseNode), typeof(BaseNode));
        LevelLevelOfDepthOperation = levelLevelOfDepthOperation;
        IsModul = isModul;
        ParentGraf = graf ?? throw new ElementGrafNullExeption(this, nameof(graf), typeof(Graf));
        PositionX = positionX;
        PositionY = positionY;
    }

    public void UpdateNode(BaseNode node)
    {
        if(node is null)
            throw new ElementGrafNullExeption(this, nameof(node), typeof(BaseNode));

        Node = node;
    }

    public void UpdatePosition(double positionX, double positionY)
    {
        PositionX = positionX;
        PositionY = positionY;
    }

    public void UpdateGraf(Graf graf)
        => ParentGraf = graf ?? throw new ElementGrafNullExeption(this, nameof(graf), typeof(Graf));

    public void UpdateLevelOfDepth(LevelOfDepth newLevel)
        => LevelLevelOfDepthOperation = newLevel;

    public bool IsContainsGraf(Graf graf) =>
        graf == ParentGraf;

    public void AddConnection(Connection connection)
        => _elementGrafConnection.Add(connection);

    public void RemuveConnection(Connection connection)
    {
        if (!_elementGrafConnection.Contains(connection))
            throw new ConnectionContainmentExeption(this, connection);

        if(!connection.IsContainsElementGraf(this))
            throw new ConnectionContainmentExeption(connection, this);

        _elementGrafConnection.Remove(connection);
    }
}
