using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет модуль - особый тип базового узла, 
/// который содержит внутренний граф операций.
/// </summary>
public class Modul : BaseNode
{
    /// <summary>
    /// Получает граф операций, связанный с модулем.
    /// </summary>
    public Graf GrafOperation { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр модуля.
    /// </summary>
    /// <param name="name">Имя модуля.</param>
    /// <param name="grafOPeration">Граф операций модуля.</param>
    public Modul(Name name, Graf grafOPeration) :
        base(name) => GrafOperation = grafOPeration;

    protected Modul() : base(default!) => GrafOperation = default!;
}