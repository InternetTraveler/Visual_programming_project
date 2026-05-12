using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions.NullExeption;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет проект, содержащий коллекцию графов.
/// </summary>
/// <remarks>
/// Project является корневым контейнером в иерархии.
/// Он объединяет все графы, которые используются в рамках одного проекта,
/// и обеспечивает управление ими на верхнем уровне.
/// </remarks>
public class Project : Entity<Guid>
{
    /// <summary>
    /// Получает имя проекта.
    /// </summary>
    public Name Name { get; private set; }

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

    /// <summary>
    /// Инициализирует новый экземпляр проекта для десериализации.
    /// </summary>
    protected Project() : base(Guid.NewGuid())
        => Name = default!;

    /// <summary>
    /// Инициализирует новый экземпляр проекта с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор проекта.</param>
    /// <param name="name">Имя проекта.</param>
    protected Project(Guid Id, Name name) : base(Id)
        => Name = name;

    /// <summary>
    /// Добавляет граф в проект.
    /// </summary>
    /// <param name="graf">Добавляемый граф.</param>
    /// <returns>true, если граф успешно добавлен; false, если граф уже существует в проекте.</returns>
    /// <exception cref="GrafNullExeption">Выбрасывается, если graf равен null.</exception>
    public bool AddGraf(Graf graf)
    {
        if (graf is null)
            throw new GrafNullExeption(this, nameof(graf), typeof(Graf));

        if (_grafs.Contains(graf))
            return false;

        _grafs.Add(graf);
        return true;
    }

    /// <summary>
    /// Удаляет граф из проекта.
    /// </summary>
    /// <param name="_graf">Удаляемый граф.</param>
    /// <returns>true, если удаление выполнено успешно.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если граф не найден в коллекции проекта.</exception>
    public bool RemoveGraf(Graf _graf)
    {
        var graf = _grafs.FirstOrDefault(n => n == _graf);

        if (graf is null)
            throw new InvalidOperationException($"Граф с идентификатором '{_graf?.Id}' не найден в проекте.");

        _grafs.Remove(graf);
        return true;
    }
}