using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class Project : Entity<Guid>
{
    public Name Name { get; private set; }

    // Навигационные свойства
    private ICollection<Graf> _grafs = [];
    public IReadOnlyCollection<Graf> Grafs => _grafs.ToList().AsReadOnly();
    public Project(Name name) : base(Guid.NewGuid()) => Name = name;

    // Управление узлами
    public void AddGraf(Graf graf)
    {
        if (graf is null)
            throw new Exception();

        _grafs.Add(graf);
    }

    public void RemoveGraf(Graf _graf)
    {
        var graf = _grafs.First(n => n == _graf);

        if (graf is null)
            throw new Exception();

        _grafs.Remove(graf);
    }
}
