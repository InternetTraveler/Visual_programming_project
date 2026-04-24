using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет связь между узлом и портом.
/// </summary>
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
    /// Автоматически добавляет связь в коллекции узла и порта.
    /// </summary>
    /// <param name="node">Узел.</param>
    /// <param name="port">Порт.</param>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается,
    /// если node или port равен null.</exception>
    public NodePortConnection(BaseNode node, Port port)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));

        Node.AddNodePortConnection(this);
        Port.AddNodePortConnection(this);
    }

    protected NodePortConnection() : base(Guid.NewGuid())
    {
        Node = default;
        Port = default;
    }

    /// <summary>
    /// Обновляет узел в связи.
    /// </summary>
    /// <param name="node">Новый узел.</param>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается,
    /// если node равен null.</exception>
    public void UpdateNode(BaseNode node)
    {
        Node!.RemoveNodePortConnection(this);
        Node = node ?? throw new NodePortConnectionNullConnection(this, nameof(node), typeof(Node));
        Node.AddNodePortConnection(this);
    }

    /// <summary>
    /// Обновляет порт в связи.
    /// </summary>
    /// <param name="port">Новый порт.</param>
    /// <exception cref="NodePortConnectionNullConnection">Выбрасывается,
    /// если port равен null.</exception>
    public void UpdatePort(Port port)
    {
        Port!.RemoveNodePortConnection(this);
        Port = port ?? throw new NodePortConnectionNullConnection(this, nameof(port), typeof(Port));
        Port.AddNodePortConnection(this);
    }

    /// <summary>
    /// Определяет, содержит ли связь указанный узел.
    /// </summary>
    /// <param name="node">Узел для проверки.</param>
    /// <returns>true, если узел связан; иначе false.</returns>
    public bool IsContainNode(BaseNode node)
        => Node == node;

    /// <summary>
    /// Определяет, содержит ли связь указанный порт.
    /// </summary>
    /// <param name="port">Порт для проверки.</param>
    /// <returns>true, если порт связан; иначе false.</returns>
    public bool IsContainPort(Port port)
        => Port == port;
}