using System.Xml.Linq;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

public class NodePortConnection : Entity<Guid>
{
    public BaseNode? Node { get; private set; }
    public Port? Port { get; private set; }

    public NodePortConnection() : base(Guid.NewGuid()) { }

    public NodePortConnection(BaseNode node, Port port)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));

        Node.AddNodePortConnection(this);
        Port.AddNodePortConnection(this);
    }

    public void UpdateNode(BaseNode node)
    {
        Node!.RemoveNodePortConnection(this);
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Node.AddNodePortConnection(this);
    }

    public void UpdatePort(Port port)
    {
        Port!.RemoveNodePortConnection(this);
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));
        Port.AddNodePortConnection(this);
    }

    public bool IsContainNode(BaseNode node)
        => Node == node;

    public bool IsContainPort(Port port)
        => Port == port;
}
