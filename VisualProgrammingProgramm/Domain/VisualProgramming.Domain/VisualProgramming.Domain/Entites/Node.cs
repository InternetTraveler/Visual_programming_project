using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет обычный узел (не модуль) с определённым типом операции.
/// </summary>
public class Node : BaseNode
{
    /// <summary>
    /// Получает тип операции, выполняемой узлом.
    /// </summary>
    public Enum.TypeOperation TypeOperation { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр узла.
    /// </summary>
    /// <param name="name">Имя узла.</param>
    /// <param name="typeOperation">Тип операции.</param>
    public Node(Name name, Enum.TypeOperation typeOperation)
        : base(name) => TypeOperation = typeOperation;

    protected Node() : base(default!) 
        => TypeOperation = default!;

    protected Node(Guid Id, Name name, Enum.TypeOperation typeOperation)
        : base(Id, name) => TypeOperation = typeOperation;
}