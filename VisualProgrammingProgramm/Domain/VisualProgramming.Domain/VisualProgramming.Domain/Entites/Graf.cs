using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет граф, содержащий коллекцию элементов
/// графа (узлов) и связанный с проектом.
/// </summary>
public class Graf : Entity<Guid>
{
    /// <summary>
    /// Получает проект, содержащий этот граф.
    /// </summary>
    public Project Project { get; private set; }

    private ICollection<ElementGraf> elementsGraf = [];

    /// <summary>
    /// Получает коллекцию элементов графа, принадлежащих этому графу.
    /// </summary>
    public IReadOnlyCollection<ElementGraf> ElementsGraf => elementsGraf.ToList().AsReadOnly();

    /// <summary>
    /// Инициализирует новый экземпляр графа.
    /// </summary>
    /// <param name="project">Проект, которому принадлежит граф.</param>
    /// <exception cref="GrafNullExeption">Выбрасывается, 
    /// если project равен null.</exception>
    public Graf(Project project) : base(Guid.NewGuid())
       => Project = project ?? throw new GrafNullExeption(this, nameof(project), typeof(Project));

    /// <summary>
    /// Определяет, содержит ли граф указанный проект.
    /// </summary>
    /// <param name="project">Проект для проверки.</param>
    /// <returns>true, если указанный проект связан 
    /// с графом; иначе false.</returns>
    public bool IsContainsProject(Project project)
        => project == Project;

    /// <summary>
    /// Обновляет проект, связанный с графом.
    /// </summary>
    /// <param name="project">Новый проект.</param>
    /// <exception cref="GrafNullExeption">Выбрасывается,
    /// если project равен null.</exception>
    public void UpdateProject(Project project)
        => Project = project ?? throw new GrafNullExeption(this, nameof(project), typeof(Project));

    /// <summary>
    /// Добавляет элемент в граф.
    /// </summary>
    /// <param name="element">Добавляемый элемент графа.</param>
    /// <exception cref="GrafNullExeption">Выбрасывается,
    /// если element равен null.</exception>
    public void AddElement(ElementGraf element)
    {
        if (element is null)
            throw new GrafNullExeption(this, nameof(element), typeof(ElementGraf));

        elementsGraf.Add(element);
    }

    /// <summary>
    /// Удаляет элемент из графа.
    /// </summary>
    /// <param name="element">Удаляемый элемент графа.</param>
    /// <exception cref="GrafContainmentException">Выбрасывается,
    /// если элемент не принадлежит графу или граф не является 
    /// родительским для элемента.</exception>
    public void RemoveElement(ElementGraf element)
    {
        if (!elementsGraf.Contains(element))
            throw new GrafContainmentException(this, element);

        if (!element.IsContainsGraf(this))
            throw new GrafContainmentException(element, this);

        elementsGraf.Remove(element);
    }
}