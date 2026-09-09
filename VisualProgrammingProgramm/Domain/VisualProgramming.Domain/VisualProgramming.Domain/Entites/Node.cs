using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет обычный узел (не модуль) с определённым типом операции.
/// </summary>
/// <remarks>
/// Node является базовым атомарным узлом, который выполняет конкретную операцию.
/// В отличие от Modul, Node не может содержать другие узлы внутри себя.
/// </remarks>
public class Node : BaseNode
{
    /// <summary>
    /// Получает тип операции, выполняемой узлом.
    /// </summary>
    /// <value>Тип операции из перечисления TypeOperation.</value>
    public Enum.TypeOperation TypeOperation { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр узла.
    /// </summary>
    /// <param name="name">Имя узла.</param>
    /// <param name="typeOperation">Тип операции, выполняемой узлом.</param>
    public Node(Name name, Enum.TypeOperation typeOperation)
        : base(name) => TypeOperation = typeOperation;

    /// <summary>
    /// Инициализирует новый экземпляр узла для десериализации.
    /// </summary>
    protected Node() : base(default!)
        => TypeOperation = default!;

    /// <summary>
    /// Инициализирует новый экземпляр узла с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор узла.</param>
    /// <param name="name">Имя узла.</param>
    /// <param name="typeOperation">Тип операции, выполняемой узлом.</param>
    protected Node(Guid Id, Name name, Enum.TypeOperation typeOperation)
        : base(Id, name) => TypeOperation = typeOperation;
}