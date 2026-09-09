using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет модуль - особый тип базового узла, который содержит внутренний граф операций.
/// </summary>
/// <remarks>
/// Modul является составным узлом, который может содержать другие узлы внутри себя.
/// Это позволяет создавать иерархические структуры и повторно использовать группы узлов.
/// </remarks>
public class Modul : BaseNode
{
    /// <summary>
    /// Получает граф операций, связанный с модулем.
    /// </summary>
    /// <value>Граф, содержащий все операции внутри модуля.</value>
    public Graf GrafOperation { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр модуля.
    /// </summary>
    /// <param name="name">Имя модуля.</param>
    /// <param name="grafOPeration">Граф операций модуля.</param>
    public Modul(Name name, Graf grafOPeration) :
        base(name) => GrafOperation = grafOPeration;

    /// <summary>
    /// Инициализирует новый экземпляр модуля для десериализации.
    /// </summary>
    protected Modul() : base(default!) => GrafOperation = default!;

    /// <summary>
    /// Инициализирует новый экземпляр модуля с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор модуля.</param>
    /// <param name="name">Имя модуля.</param>
    /// <param name="grafOPeration">Граф операций модуля.</param>
    protected Modul(Guid Id, Name name, Graf grafOPeration) :
        base(Id, name) => GrafOperation = grafOPeration;
}