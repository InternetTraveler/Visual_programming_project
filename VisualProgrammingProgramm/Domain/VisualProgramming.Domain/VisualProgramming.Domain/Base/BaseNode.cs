using VisualProgramming.Domain.Entites;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Base;

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
