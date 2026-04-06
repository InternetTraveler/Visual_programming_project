using VisualProgramming.Domain.Enum;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class Node : BaseNode
{
    public Enum.TypeOperation TypeOperation { get; private set; }
    public Node(Name name, Enum.TypeOperation typeOperation)
        : base(name) 
    {
        TypeOperation = typeOperation;
    }

    public void AddPort(Port port)
    {
        if(port is null)
            throw new Exception();

        _ports.Add(port);
    }

    public void RemovePort(Port port)
    {
        if(port is null)
            throw new ArgumentNullException("port");

        _ports.Remove(port);
    }
}
