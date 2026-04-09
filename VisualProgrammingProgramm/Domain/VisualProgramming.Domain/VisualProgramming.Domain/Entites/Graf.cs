using System.Collections.ObjectModel;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

public class Graf : Entity<Guid>
{
    public Project Project { get; private set; }

    private ICollection<ElementGraf> elementsGraf = [];
    public IReadOnlyCollection<ElementGraf> ElementsGraf => elementsGraf.ToList().AsReadOnly();

    public Graf(Project project) : base(Guid.NewGuid()) 
       => Project = project ?? throw new GrafNullExeption(this, nameof(project), typeof(Project));

    public bool IsContainsProject(Project project)
        => project == Project;

    public void UpdateProject(Project project)
        => Project = project ?? throw new GrafNullExeption(this, nameof(project), typeof(Project));

    public void AddElement(ElementGraf element)
    {
        if (element is null)
            throw new GrafNullExeption(this, nameof(element), typeof(ElementGraf));

        elementsGraf.Add(element);
    }

    public void RemoveElement(ElementGraf element)
    {
        if(!elementsGraf.Contains(element))
            throw new GrafContainmentException(this, element);

        if(!element.IsContainsGraf(this))
            throw new GrafContainmentException(element, this);

        elementsGraf.Remove(element);
    }
}