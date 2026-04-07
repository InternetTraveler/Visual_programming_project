using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.ValueObject;
namespace VisualProgramming.Domain.Entites;

public class Port : Entity<Guid>
{
    // Если портов есть определёное количиство
    // от их надо связать через другую таблицу
    // ???????????????????????????????????????
    public BaseNode Node { get; private set; }
    public TypePort TypePort { get; private set; }
    public Description Description { get; private set; }

    public Port(BaseNode node, TypePort typePort, string description)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new InvalidPortDataException(node, typePort, description, "Not node");
        TypePort = typePort;
        Description = description;
    }
}
