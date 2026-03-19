using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class Project : Entity<Guid>
{
    public Name Name { get; private set; }

    // Навигационные свойства
    private readonly List<BaseNode> _nodes = new();
    public IReadOnlyCollection<BaseNode> Nodes => _nodes.AsReadOnly();
    public Project(string name) : base(Guid.NewGuid()) => Name = name;

    // Управление узлами
    public void AddNode(BaseNode node)
    {
        _nodes.Add(node);
    }

    public void RemoveNode(BaseNode _node)
    {
        var node = _nodes.First(n => n == _node);
        if (node != null)
            _nodes.Remove(node);
    }
}
