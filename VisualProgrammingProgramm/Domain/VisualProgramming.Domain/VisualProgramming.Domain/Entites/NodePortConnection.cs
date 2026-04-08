using VisualProgramming.Domain.Base;

namespace VisualProgramming.Domain.Entites;

public class NodePortConnection : Entity<Guid>
{
    public BaseNode? Node { get; private set; }
    public Port? Port { get; private set; }

    public NodePortConnection() : base(Guid.NewGuid()) { }

    public NodePortConnection(BaseNode node, Port port)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new Exception();
        Port = port ?? throw new Exception();

        Node.AddNodePortConnection(this);
        Port.AddNodePortConnection(this);
    }

    public void UpdateNode(BaseNode node)
    {
        Node!.RemoveNodePortConnection(this);
        Node = node ?? throw new Exception();
        Node.AddNodePortConnection(this);
    }

    public void UpdatePort(Port port)
    {
        Port!.RemoveNodePortConnection(this);
        Port = port ?? throw new Exception();
        Port.AddNodePortConnection(this);
    }

    public bool IsContainNode(BaseNode node)
        => Node == node;

    public bool IsContainPort(Port port)
        => Port == port;
}
