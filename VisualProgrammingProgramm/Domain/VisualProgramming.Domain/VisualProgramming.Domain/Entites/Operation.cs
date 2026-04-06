using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class Operation : Entity<Guid>
{
    public TypeOperation? TypeOperation { get; private set; }

    public Operation() : base(Guid.NewGuid()) { }
    public Operation(TypeOperation typeOperation) : base(Guid.NewGuid())
    {
        TypeOperation = typeOperation;
    }

    public void SetOperation(TypeOperation typeOperation)
        => TypeOperation = typeOperation;
}
