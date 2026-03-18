namespace VisualProgramming.Domain.Entites;

public class Modul : BaseNode
{
    public Guid Id { get; private set; }
    public Guid ProjectCreateId { get; private set; }

    private ICollection<Operation> operations = [];
    public IReadOnlyCollection<Operation> Operations => operations.ToList().AsReadOnly();

    public Modul(string name, Guid projectCreateId, Project projectId, double positionX, double positionY) :
        base(name, projectId, positionX, positionY)
    {
        Id = Guid.NewGuid();
        ProjectCreateId = projectCreateId;
    }
}
