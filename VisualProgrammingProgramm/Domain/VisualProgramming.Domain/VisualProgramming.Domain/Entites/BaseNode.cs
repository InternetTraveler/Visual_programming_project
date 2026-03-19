using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public abstract class BaseNode : Entity<Guid>
{
    public Name Name { get; private set; }
    public Project ProjectId { get; private set; }
    public double PositionX { get; private set; }
    public double PositionY { get; private set; }

    protected ICollection<Port> _ports = [];
    protected ICollection<Connection> _connection = [];
    public IReadOnlyCollection<Port> Ports => _ports.ToList().AsReadOnly();
    public IReadOnlyCollection<Connection> Connections => _connection.ToList().AsReadOnly();

    public BaseNode(string name, Project projectId, double positionX, double positionY)
        :base(Guid.NewGuid())
    {
        Name = name;
        ProjectId = projectId;
        PositionX = positionX;
        PositionY = positionY;
    }

    public void UpdatePosition(double x, double y)
    {
        PositionX = x;
        PositionY = y;
    }
}
