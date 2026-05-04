using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Base;

/// <summary>
/// Представляет абстрактный базовый узел в визуальной среде программирования.
/// </summary>
/// <remarks>
/// BaseNode является фундаментальным строительным блоком 
/// для всех типов узлов в системе.
/// Он управляет подключениями портов и обеспечивает базовую 
/// функциональность для работы с соединениями.
/// </remarks>
public abstract class BaseNode : Entity<Guid>
{
    /// <summary>
    /// Получает имя узла.
    /// </summary>
    /// <value>Объект Name, содержащее имя узла.</value>
    /// <remarks>Имя устанавливается только при создании узла и 
    /// не может быть изменено в дальнейшем.</remarks>
    public Name Name { get; private set; }

    /// <summary>
    /// Коллекция соединений портов узла.
    /// </summary>
    protected ICollection<NodePortConnection> nodePortConnections = [];

    /// <summary>
    /// Получает доступное только для чтения представление 
    /// коллекции соединений портов узла.
    /// </summary>
    /// <value>Коллекция соединений портов только для чтения.</value>
    /// <remarks>Используйте этот метод для безопасного доступа 
    /// к соединениям без возможности их изменения.</remarks>
    public IReadOnlyCollection<NodePortConnection> NodePortConnections => 
        nodePortConnections.ToList().AsReadOnly();

    /// <summary>
    /// Инициализирует новый экземпляр класса BaseNode с указанным именем.
    /// </summary>
    /// <param name="name">Имя узла, представленное объектом Name.</param>
    /// <remarks>
    /// При создании узла автоматически генерируется 
    /// новый уникальный идентификатор (Guid).
    /// </remarks>
    public BaseNode(Name name)
        : base(Guid.NewGuid()) => Name = name;

    protected BaseNode()
        : base(Guid.NewGuid()) => Name = default!;

    protected BaseNode(Guid Id, Name name)
        : base(Id) => Name = name;

    /// <summary>
    /// Добавляет новое соединение порта к узлу.
    /// </summary>
    /// <param name="_nodePortConnection">Соединение порта 
    /// узла для добавления.</param>
    /// <exception cref="NodeNullExeption">Выбрасывается, 
    /// если переданное соединение порта является null.</exception>
    /// <remarks>
    /// Метод проверяет переданное соединение на null 
    /// перед добавлением в коллекцию.
    /// </remarks>
    public bool AddNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (_nodePortConnection is null)
            throw new NodeNullExeption(this, nameof(_nodePortConnection), typeof(NodePortConnection));

        if (nodePortConnections.Contains(_nodePortConnection))
            return false;

        nodePortConnections.Add(_nodePortConnection);

        return true;
    }

    /// <summary>
    /// Удаляет существующее соединение порта из узла.
    /// </summary>
    /// <param name="_nodePortConnection">Соединение порта 
    /// узла для удаления.</param>
    /// <exception cref="GrafContainmentException"/>
    /// <remarks>
    /// Перед удалением метод проверяет наличие соединения 
    /// в коллекции и принадлежность соединения данному узлу.
    /// </remarks>
    public void RemoveNodePortConnection(NodePortConnection _nodePortConnection)
    {
        if (!nodePortConnections.Contains(_nodePortConnection))
            throw new GrafContainmentException(this, _nodePortConnection);

        if (!_nodePortConnection.IsContainNode(this))
            throw new GrafContainmentException(_nodePortConnection, this);

        nodePortConnections.Remove(_nodePortConnection);
    }
}