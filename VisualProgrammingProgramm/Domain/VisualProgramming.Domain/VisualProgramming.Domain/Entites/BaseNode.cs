using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public abstract class BaseNode : Entity<Guid>
{
    public Name Name { get; private set; }

    protected ICollection<Port> _ports = [];
    public IReadOnlyCollection<Port> Ports => _ports.ToList().AsReadOnly();

    public BaseNode(Name name)
        :base(Guid.NewGuid())
    {
        Name = name;
    }
}
