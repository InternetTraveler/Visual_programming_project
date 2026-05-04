using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет связь между узлом и портом.
/// </summary>
/// <remarks>
/// NodePortConnection является связующим звеном между BaseNode и Port.
/// При создании автоматически добавляет себя в коллекции как узла, так и порта.
/// Это обеспечивает двунаправленную навигацию между узлами и портами.
/// </remarks>
public class NodePortConnection : Entity<Guid>
{
    /// <summary>
    /// Получает узел, связанный с портом.
    /// </summary>
    public BaseNode? Node { get; private set; }

    /// <summary>
    /// Получает порт, связанный с узлом.
    /// </summary>
    public Port? Port { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр связи между узлом и портом.
    /// </summary>
    /// <param name="node">Узел, который нужно связать.</param>
    /// <param name="port">Порт, который нужно связать.</param>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается, если node или port равен null.</exception>
    /// <remarks>
    /// Конструктор автоматически добавляет созданную связь в коллекции узла и порта.
    /// </remarks>
    public NodePortConnection(BaseNode node, Port port)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));

        Node.AddNodePortConnection(this);
        Port.AddNodePortConnection(this);
    }

    /// <summary>
    /// Инициализирует новый экземпляр связи для десериализации.
    /// </summary>
    protected NodePortConnection() : base(Guid.NewGuid())
    {
        Node = default;
        Port = default;
    }

    /// <summary>
    /// Инициализирует новый экземпляр связи с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор связи.</param>
    /// <param name="node">Узел, который нужно связать.</param>
    /// <param name="port">Порт, который нужно связать.</param>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается, если node или port равен null.</exception>
    protected NodePortConnection(Guid Id, BaseNode node, Port port)
        : base(Id)
    {
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));

        Node.AddNodePortConnection(this);
        Port.AddNodePortConnection(this);
    }

    /// <summary>
    /// Обновляет узел в связи.
    /// </summary>
    /// <param name="node">Новый узел.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается, если node равен null.</exception>
    /// <remarks>
    /// Метод автоматически удаляет связь из старого узла и добавляет в новый.
    /// </remarks>
    public bool UpdateNode(BaseNode node)
    {
        Node!.RemoveNodePortConnection(this);
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Node.AddNodePortConnection(this);
        return true;
    }

    /// <summary>
    /// Обновляет порт в связи.
    /// </summary>
    /// <param name="port">Новый порт.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается, если port равен null.</exception>
    /// <remarks>
    /// Метод автоматически удаляет связь из старого порта и добавляет в новый.
    /// </remarks>
    public bool UpdatePort(Port port)
    {
        Port!.RemoveNodePortConnection(this);
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));
        Port.AddNodePortConnection(this);
        return true;
    }

    /// <summary>
    /// Определяет, содержит ли связь указанный узел.
    /// </summary>
    /// <param name="node">Узел для проверки.</param>
    /// <returns>true, если узел связан с данной связью; иначе false.</returns>
    public bool IsContainNode(BaseNode node)
        => Node == node;

    /// <summary>
    /// Определяет, содержит ли связь указанный порт.
    /// </summary>
    /// <param name="port">Порт для проверки.</param>
    /// <returns>true, если порт связан с данной связью; иначе false.</returns>
    public bool IsContainPort(Port port)
        => Port == port;
}