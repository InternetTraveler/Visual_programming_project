using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions.NullExeption;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет проект, содержащий коллекцию графов.
/// </summary>
public class Project : Entity<Guid>
{
    /// <summary>
    /// Получает имя проекта.
    /// </summary>
    public Name Name { get; private set; }

    // Навигационные свойства
    private ICollection<Graf> _grafs = [];

    /// <summary>
    /// Получает коллекцию графов, принадлежащих проекту.
    /// </summary>
    public IReadOnlyCollection<Graf> Grafs => _grafs.ToList().AsReadOnly();

    /// <summary>
    /// Инициализирует новый экземпляр проекта.
    /// </summary>
    /// <param name="name">Имя проекта.</param>
    public Project(Name name) : base(Guid.NewGuid()) 
        => Name = name;

    protected Project() : base(Guid.NewGuid())
        => Name = default!;

    /// <summary>
    /// Добавляет граф в проект.
    /// </summary>
    /// <param name="graf">Добавляемый граф.</param>
    /// <exception cref="Exception">Выбрасывается, если 
    /// graf равен null.</exception>
    public void AddGraf(Graf graf)
    {
        if (graf is null)
            throw new GrafNullExeption(this, nameof(graf), typeof(Graf));

        if (_grafs.Contains(graf))
            return;

        _grafs.Add(graf);
    }

    /// <summary>
    /// Удаляет граф из проекта.
    /// </summary>
    /// <param name="_graf">Удаляемый граф.</param>
    /// <exception cref="Exception">Выбрасывается, если 
    /// граф не найден в коллекции.</exception>
    public void RemoveGraf(Graf _graf)
    {
        var graf = _grafs.First(n => n == _graf);

        if (graf is null)
            throw new Exception();

        _grafs.Remove(graf);
    }
}