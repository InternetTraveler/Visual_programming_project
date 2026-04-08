using System.Net.NetworkInformation;
using VisualProgramming.Domain.Base;
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
}
