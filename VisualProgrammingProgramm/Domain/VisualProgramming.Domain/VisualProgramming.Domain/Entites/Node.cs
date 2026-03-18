using VisualProgramming.Domain.Enum;

namespace VisualProgramming.Domain.Entites;

public class Node : BaseNode
{
    public Guid Id { get; private set; }

    public Node(string name, Project projectId, double positionX, double positionY)
        : base(name, projectId, positionX, positionY) { }

    public Port AddPort(TypePort typePort, string description)
    {
        var port = new Port(Id, typePort, description);
        _ports.Add(port);
        return port;
    }

    public void RemovePort(Port portId)
    {
        var port = _ports.First(p => p == portId);
        if (port != null)
            _ports.Remove(port);
    }
}
