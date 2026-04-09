using System.Xml.Linq;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;
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
            throw new NodeNullExeption(this, nameof(_nodePortConnection), typeof(NodePortConnection));

        nodePortConnections.Add(_nodePortConnection);
    }

    public void RemoveNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (!nodePortConnections.Contains(_nodePortConnection))
            throw new GrafContainmentException(this, _nodePortConnection);

        if (!_nodePortConnection.IsContainNode(this))
            throw new GrafContainmentException(_nodePortConnection, this);

        nodePortConnections.Remove(_nodePortConnection);
    }
}
