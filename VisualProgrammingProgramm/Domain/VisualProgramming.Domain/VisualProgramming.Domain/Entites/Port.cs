using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.ValueObject;
namespace VisualProgramming.Domain.Entites;

public class Port : Entity<Guid>
{
    public BaseNode Node { get; private set; }
    public TypePort TypePort { get; private set; }
    public Description Description { get; private set; }

    private ICollection<NodePortConnection> nodePortConnections = [];
    public IReadOnlyCollection<NodePortConnection> NodePortConnections => nodePortConnections.ToList().AsReadOnly();

    public Port(BaseNode node, TypePort typePort, string description)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new InvalidPortDataException(node, typePort, description, "Not node");
        TypePort = typePort;
        Description = description;
    }

    public void AddNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (_nodePortConnection is null)
            throw new Exception();

        nodePortConnections.Add(_nodePortConnection);
    }

    public void RemoveNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (_nodePortConnection is null)
            throw new Exception();

        if (!nodePortConnections.Contains(_nodePortConnection))
            throw new Exception();

        nodePortConnections.Remove(_nodePortConnection);
    }
}
