using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class Node : BaseNode
{
    public Operation Opperation { get; private set; }
    public Node(Name name, Operation operation)
        : base(name) 
    {
        Opperation = operation ?? throw new Exception();
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
