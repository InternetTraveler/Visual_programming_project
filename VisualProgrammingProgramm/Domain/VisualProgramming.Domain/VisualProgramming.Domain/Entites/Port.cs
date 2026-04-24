using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет порт узла, который может быть входным или выходным.
/// </summary>
public class Port : Entity<Guid>
{
    /// <summary>
    /// Получает узел, которому принадлежит порт.
    /// </summary>
    public BaseNode Node { get; private set; }

    /// <summary>
    /// Получает тип порта (входной или выходной).
    /// </summary>
    public TypePort TypePort { get; private set; }

    /// <summary>
    /// Получает описание порта.
    /// </summary>
    public Description Description { get; private set; }

    private ICollection<NodePortConnection> nodePortConnections = [];

    /// <summary>
    /// Получает коллекцию связей узла с портом.
    /// </summary>
    public IReadOnlyCollection<NodePortConnection> NodePortConnections 
        => nodePortConnections.ToList().AsReadOnly();

    /// <summary>
    /// Инициализирует новый экземпляр порта.
    /// </summary>
    /// <param name="node">Узел-владелец порта.</param>
    /// <param name="typePort">Тип порта.</param>
    /// <param name="description">Описание порта.</param>
    /// <exception cref="PortNullExeption">Выбрасывается, если node равен null.</exception>
    public Port(BaseNode node, TypePort typePort, string description)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new PortNullExeption(this, nameof(node), typeof(Node));
        TypePort = typePort;
        Description = description;
    }

    protected Port() : base(Guid.NewGuid())
    {
        Node = default!;
        TypePort = default!;
        Description = default!;
    }

    /// <summary>
    /// Добавляет связь узла с портом в коллекцию порта.
    /// </summary>
    /// <param name="_nodePortConnection">Добавляемая связь.</param>
    /// <exception cref="PortNullExeption">Выбрасывается,
    /// если _nodePortConnection равен null.</exception>
    public void AddNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (_nodePortConnection is null)
            throw new PortNullExeption(this, nameof(_nodePortConnection), typeof(NodePortConnection));

        if (nodePortConnections.Contains(_nodePortConnection))
            return;

        nodePortConnections.Add(_nodePortConnection);
    }

    /// <summary>
    /// Удаляет связь узла с портом из коллекции порта.
    /// </summary>
    /// <param name="_nodePortConnection">Удаляемая связь.</param>
    /// <exception cref="GrafContainmentException">Выбрасывается,
    /// если связь не принадлежит порту или порт не участвует в связи.</exception>
    public void RemoveNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (!_nodePortConnection.IsContainPort(this))
            throw new GrafContainmentException(this, _nodePortConnection);

        if (!nodePortConnections.Contains(_nodePortConnection))
            throw new GrafContainmentException(_nodePortConnection, this);

        nodePortConnections.Remove(_nodePortConnection);
    }
}