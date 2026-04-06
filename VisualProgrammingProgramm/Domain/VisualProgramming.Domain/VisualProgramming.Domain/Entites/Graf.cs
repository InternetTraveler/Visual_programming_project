using System.Collections.ObjectModel;
using VisualProgramming.Domain.Base;

namespace VisualProgramming.Domain.Entites;

public class Graf : Entity<Guid>
{
    private ICollection<ElementGraf> elementsGraf = [];
    public ReadOnlyCollection<ElementGraf> ElementsGraf => elementsGraf.ToList().AsReadOnly();

    public Graf() : base(Guid.NewGuid()) { }

    public void AddElement(ElementGraf element)
    {
        if (element is null)
            throw new Exception();

        elementsGraf.Add(element);
    }

    public void RemoveElement(ElementGraf element)
    {
        if(!elementsGraf.Contains(element))
            throw new Exception();

        if(!element.IsContainsGraf(this))
            throw new Exception();

        elementsGraf.Remove(element);
    }
}