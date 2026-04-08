using VisualProgramming.Domain.Entites;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Base;

public abstract class BaseNode : Entity<Guid>
{
    public Name Name { get; private set; }

    protected ICollection<NodePortConnection> nodePortConnections = [];
    public IReadOnlyCollection<NodePortConnection> NodePortConnections => nodePortConnections.ToList().AsReadOnly();

    public BaseNode(Name name)
        :base(Guid.NewGuid())
    {
        Name = name;
    }

    public void AddNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (_nodePortConnection is null)
            throw new Exception();

        nodePortConnections.Add(_nodePortConnection);
    }

    public void RemoveNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (nodePortConnections.Contains(_nodePortConnection))
            throw new Exception();

        if (_nodePortConnection is null)
            throw new Exception();

        nodePortConnections.Remove(_nodePortConnection);
    }
}
