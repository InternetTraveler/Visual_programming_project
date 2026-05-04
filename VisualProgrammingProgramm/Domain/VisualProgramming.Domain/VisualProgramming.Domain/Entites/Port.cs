using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Enum;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет порт узла, который может быть входным или выходным.
/// </summary>
/// <remarks>
/// Порты являются точками подключения для соединений между узлами.
/// Каждый порт принадлежит определённому узлу и имеет тип (входной или выходной),
/// что определяет допустимые направления соединений.
/// </remarks>
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
    /// <param name="typePort">Тип порта (входной или выходной).</param>
    /// <param name="description">Описание порта.</param>
    /// <exception cref="PortNullExeption">Выбрасывается, если node равен null.</exception>
    public Port(BaseNode node, TypePort typePort, string description)
        : base(Guid.NewGuid())
    {
        Node = node ?? throw new PortNullExeption(this, nameof(node), typeof(Node));
        TypePort = typePort;
        Description = description;
    }

    /// <summary>
    /// Инициализирует новый экземпляр порта для десериализации.
    /// </summary>
    protected Port() : base(Guid.NewGuid())
    {
        Node = default!;
        TypePort = default!;
        Description = default!;
    }

    /// <summary>
    /// Инициализирует новый экземпляр порта с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор порта.</param>
    /// <param name="node">Узел-владелец порта.</param>
    /// <param name="typePort">Тип порта (входной или выходной).</param>
    /// <param name="description">Описание порта.</param>
    /// <exception cref="PortNullExeption">Выбрасывается, если node равен null.</exception>
    protected Port(Guid Id, BaseNode node, TypePort typePort, string description)
        : base(Id)
    {
        Node = node ?? throw new PortNullExeption(this, nameof(node), typeof(Node));
        TypePort = typePort;
        Description = description;
    }

    /// <summary>
    /// Добавляет связь узла с портом в коллекцию порта.
    /// </summary>
    /// <param name="_nodePortConnection">Добавляемая связь.</param>
    /// <returns>true, если связь успешно добавлена; false, если связь уже существует.</returns>
    /// <exception cref="PortNullExeption">Выбрасывается, если _nodePortConnection равен null.</exception>
    public bool AddNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (_nodePortConnection is null)
            throw new PortNullExeption(this, nameof(_nodePortConnection), typeof(NodePortConnection));

        if (nodePortConnections.Contains(_nodePortConnection))
            return false;

        nodePortConnections.Add(_nodePortConnection);
        return true;
    }

    /// <summary>
    /// Удаляет связь узла с портом из коллекции порта.
    /// </summary>
    /// <param name="_nodePortConnection">Удаляемая связь.</param>
    /// <returns>true, если удаление выполнено успешно.</returns>
    /// <exception cref="GrafContainmentException">
    /// Выбрасывается, если связь не принадлежит порту или порт не участвует в связи.
    /// </exception>
    public bool RemoveNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (!_nodePortConnection.IsContainPort(this))
            throw new GrafContainmentException(this, _nodePortConnection);

        if (!nodePortConnections.Contains(_nodePortConnection))
            throw new GrafContainmentException(_nodePortConnection, this);

        nodePortConnections.Remove(_nodePortConnection);
        return true;
    }
}