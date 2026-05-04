using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет элемент (узел) внутри графа, включая его позицию, уровень вложенности и связи.
/// </summary>
/// <remarks>
/// ElementGraf является контейнером для BaseNode в контексте графа.
/// Он добавляет пространственную информацию (позицию), уровень глубины операции
/// и связь с родительским графом.
/// </remarks>
public class ElementGraf : Entity<Guid>
{
    /// <summary>
    /// Получает базовый узел (модуль или обычный узел), связанный с этим элементом графа.
    /// </summary>
    public BaseNode? Node { get; private set; }

    /// <summary>
    /// Получает уровень глубины операции.
    /// </summary>
    public LevelOfDepth LevelLevelOfDepthOperation { get; private set; }

    /// <summary>
    /// Получает значение, указывающее, является ли элемент модулем.
    /// </summary>
    public bool IsModul { get; private set; }

    /// <summary>
    /// Получает родительский граф, содержащий этот элемент.
    /// </summary>
    public Graf? ParentGraf { get; private set; }

    /// <summary>
    /// Получает координату X позиции элемента.
    /// </summary>
    public double PositionX { get; private set; }

    /// <summary>
    /// Получает координату Y позиции элемента.
    /// </summary>
    public double PositionY { get; private set; }

    private ICollection<Connection> _elementGrafConnection = [];

    /// <summary>
    /// Получает коллекцию соединений, связанных с этим элементом графа.
    /// </summary>
    public IReadOnlyCollection<Connection> ElementGrafConnections
        => _elementGrafConnection.ToList().AsReadOnly();

    /// <summary>
    /// Инициализирует новый экземпляр элемента графа.
    /// </summary>
    /// <param name="baseNode">Базовый узел.</param>
    /// <param name="levelLevelOfDepthOperation">Уровень глубины операции.</param>
    /// <param name="isModul">Определяет, является ли элемент модулем.</param>
    /// <param name="graf">Родительский граф.</param>
    /// <param name="positionX">Координата X позиции элемента.</param>
    /// <param name="positionY">Координата Y позиции элемента.</param>
    /// <exception cref="ElementGrafNullExeption">Выбрасывается, если baseNode или graf равен null.</exception>
    public ElementGraf(BaseNode baseNode, LevelOfDepth levelLevelOfDepthOperation, bool isModul,
        Graf graf, double positionX, double positionY)
        : base(Guid.NewGuid())
    {
        Node = baseNode ?? throw new ElementGrafNullExeption(this, nameof(baseNode), typeof(BaseNode));
        LevelLevelOfDepthOperation = levelLevelOfDepthOperation;
        IsModul = isModul;
        ParentGraf = graf ?? throw new ElementGrafNullExeption(this, nameof(graf), typeof(Graf));
        PositionX = positionX;
        PositionY = positionY;
    }

    /// <summary>
    /// Инициализирует новый экземпляр элемента графа для десериализации.
    /// </summary>
    protected ElementGraf() : base(Guid.NewGuid())
    {
        Node = default;
        LevelLevelOfDepthOperation = default!;
        IsModul = default;
        ParentGraf = default;
        PositionX = default;
        PositionY = default;
    }

    /// <summary>
    /// Инициализирует новый экземпляр элемента графа с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор элемента графа.</param>
    /// <param name="baseNode">Базовый узел.</param>
    /// <param name="levelLevelOfDepthOperation">Уровень глубины операции.</param>
    /// <param name="isModul">Определяет, является ли элемент модулем.</param>
    /// <param name="graf">Родительский граф.</param>
    /// <param name="positionX">Координата X позиции элемента.</param>
    /// <param name="positionY">Координата Y позиции элемента.</param>
    /// <exception cref="ElementGrafNullExeption">Выбрасывается, если baseNode или graf равен null.</exception>
    protected ElementGraf(Guid Id, BaseNode baseNode, LevelOfDepth levelLevelOfDepthOperation, bool isModul,
        Graf graf, double positionX, double positionY)
        : base(Id)
    {
        Node = baseNode ?? throw new ElementGrafNullExeption(this, nameof(baseNode), typeof(BaseNode));
        LevelLevelOfDepthOperation = levelLevelOfDepthOperation;
        IsModul = isModul;
        ParentGraf = graf ?? throw new ElementGrafNullExeption(this, nameof(graf), typeof(Graf));
        PositionX = positionX;
        PositionY = positionY;
    }

    /// <summary>
    /// Обновляет базовый узел элемента графа.
    /// </summary>
    /// <param name="node">Новый базовый узел.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    /// <exception cref="ElementGrafNullExeption">Выбрасывается, если node равен null.</exception>
    public bool UpdateNode(BaseNode node)
    {
        if (node is null)
            throw new ElementGrafNullExeption(this, nameof(node), typeof(BaseNode));

        Node = node;
        return true;
    }

    /// <summary>
    /// Обновляет позицию элемента графа.
    /// </summary>
    /// <param name="positionX">Новая координата X.</param>
    /// <param name="positionY">Новая координата Y.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    public bool UpdatePosition(double positionX, double positionY)
    {
        PositionX = positionX;
        PositionY = positionY;
        return true;
    }

    /// <summary>
    /// Обновляет родительский граф элемента.
    /// </summary>
    /// <param name="graf">Новый родительский граф.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    /// <exception cref="ElementGrafNullExeption">Выбрасывается, если graf равен null.</exception>
    public bool UpdateGraf(Graf graf)
    {
        ParentGraf = graf ?? throw new ElementGrafNullExeption(this, nameof(graf), typeof(Graf));
        return true;
    }

    /// <summary>
    /// Обновляет уровень глубины операции.
    /// </summary>
    /// <param name="newLevel">Новый уровень глубины.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    public bool UpdateLevelOfDepth(LevelOfDepth newLevel)
    {
        LevelLevelOfDepthOperation = newLevel;
        return true;
    }

    /// <summary>
    /// Определяет, содержит ли этот элемент указанный граф в качестве родительского.
    /// </summary>
    /// <param name="graf">Граф для проверки.</param>
    /// <returns>true, если указанный граф является родительским; иначе false.</returns>
    public bool IsContainsGraf(Graf graf) =>
        graf == ParentGraf;

    /// <summary>
    /// Добавляет соединение к элементу графа.
    /// </summary>
    /// <param name="connection">Добавляемое соединение.</param>
    /// <returns>true, если соединение успешно добавлено; false, если соединение уже существует.</returns>
    /// <exception cref="ConnectionNullExeption">Выбрасывается, если connection равен null.</exception>
    public bool AddConnection(Connection connection)
    {
        if (connection is null)
            throw new ConnectionNullExeption(this, nameof(connection), typeof(Connection));

        if (_elementGrafConnection.Contains(connection))
            return false;

        _elementGrafConnection.Add(connection);
        return true;
    }

    /// <summary>
    /// Удаляет соединение из элемента графа.
    /// </summary>
    /// <param name="connection">Удаляемое соединение.</param>
    /// <returns>true, если удаление выполнено успешно.</returns>
    /// <exception cref="ConnectionContainmentExeption">
    /// Выбрасывается, если соединение не принадлежит этому элементу или элемент не участвует в соединении.
    /// </exception>
    public bool RemuveConnection(Connection connection)
    {
        if (!_elementGrafConnection.Contains(connection))
            throw new ConnectionContainmentExeption(this, connection);

        if (!connection.IsContainsElementGraf(this))
            throw new ConnectionContainmentExeption(connection, this);

        _elementGrafConnection.Remove(connection);
        return true;
    }
}