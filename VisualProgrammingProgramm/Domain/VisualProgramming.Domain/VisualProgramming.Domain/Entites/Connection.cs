using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

/// <summary>
/// Представляет соединение между двумя портами двух разных элементов графа.
/// </summary>
/// <remarks>
/// Соединение всегда связывает входной порт с выходным портом.
/// InElementGraf и SourcePort представляют источник (выход),
/// OutElementGraf и TargetPort представляют приёмник (вход).
/// </remarks>
public class Connection : Entity<Guid>
{
    /// <summary>
    /// Получает элемент графа, из которого исходит соединение (источник).
    /// </summary>
    public ElementGraf InElementGraf { get; private set; }

    /// <summary>
    /// Получает исходный порт соединения (порт источника).
    /// </summary>
    public Port? SourcePort { get; private set; }

    /// <summary>
    /// Получает элемент графа, в который входит соединение (приёмник).
    /// </summary>
    public ElementGraf OutElementGraf { get; private set; }

    /// <summary>
    /// Получает целевой порт соединения (порт приёмника).
    /// </summary>
    public Port? TargetPort { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр соединения между двумя портами.
    /// </summary>
    /// <param name="sourcePort">Исходный порт (порт источника).</param>
    /// <param name="targetPort">Целевой порт (порт приёмника).</param>
    /// <param name="inElementGraf">Элемент графа-источник.</param>
    /// <param name="outElemGraf">Элемент графа-приёмник.</param>
    /// <exception cref="TypePortConnectionExeption">Выбрасывается, если типы портов совпадают.</exception>
    /// <exception cref="EqvelElementGrafExeption">Выбрасывается, если оба элемента графа одинаковы.</exception>
    /// <exception cref="ConnectionNullExeption">Выбрасывается, если любой из параметров равен null.</exception>
    public Connection(Port sourcePort, Port targetPort, ElementGraf inElementGraf, ElementGraf outElemGraf)
        : base(Guid.NewGuid())
    {
        if (sourcePort.TypePort == targetPort.TypePort)
            throw new TypePortConnectionExeption(this, sourcePort, targetPort);

        if (inElementGraf == outElemGraf)
            throw new EqvelElementGrafExeption(this, inElementGraf);

        SourcePort = sourcePort ?? throw new ConnectionNullExeption(this, nameof(sourcePort), typeof(Port));
        TargetPort = targetPort ?? throw new ConnectionNullExeption(this, nameof(targetPort), typeof(Port));
        OutElementGraf = outElemGraf ?? throw new ConnectionNullExeption(this, nameof(outElemGraf), typeof(ElementGraf));
        InElementGraf = inElementGraf ?? throw new ConnectionNullExeption(this, nameof(inElementGraf), typeof(ElementGraf));
    }

    /// <summary>
    /// Инициализирует новый экземпляр соединения для десериализации.
    /// </summary>
    protected Connection() : base(Guid.NewGuid())
    {
        SourcePort = default!;
        TargetPort = default!;
        OutElementGraf = default!;
        InElementGraf = default!;
    }

    /// <summary>
    /// Инициализирует новый экземпляр соединения с указанным идентификатором.
    /// </summary>
    /// <param name="Id">Уникальный идентификатор соединения.</param>
    /// <param name="sourcePort">Исходный порт (порт источника).</param>
    /// <param name="targetPort">Целевой порт (порт приёмника).</param>
    /// <param name="inElementGraf">Элемент графа-источник.</param>
    /// <param name="outElemGraf">Элемент графа-приёмник.</param>
    /// <exception cref="TypePortConnectionExeption">Выбрасывается, если типы портов совпадают.</exception>
    /// <exception cref="EqvelElementGrafExeption">Выбрасывается, если оба элемента графа одинаковы.</exception>
    /// <exception cref="ConnectionNullExeption">Выбрасывается, если любой из параметров равен null.</exception>
    protected Connection(Guid Id, Port sourcePort, Port targetPort, ElementGraf inElementGraf, ElementGraf outElemGraf)
        : base(Id)
    {
        if (sourcePort.TypePort == targetPort.TypePort)
            throw new TypePortConnectionExeption(this, sourcePort, targetPort);

        if (inElementGraf == outElemGraf)
            throw new EqvelElementGrafExeption(this, inElementGraf);

        SourcePort = sourcePort ?? throw new ConnectionNullExeption(this, nameof(sourcePort), typeof(Port));
        TargetPort = targetPort ?? throw new ConnectionNullExeption(this, nameof(targetPort), typeof(Port));
        OutElementGraf = outElemGraf ?? throw new ConnectionNullExeption(this, nameof(outElemGraf), typeof(ElementGraf));
        InElementGraf = inElementGraf ?? throw new ConnectionNullExeption(this, nameof(inElementGraf), typeof(ElementGraf));
    }

    /// <summary>
    /// Обновляет входящее соединение (источник).
    /// </summary>
    /// <param name="element">Новый элемент графа-источник.</param>
    /// <param name="port">Новый исходный порт.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    /// <exception cref="ConnectionNullExeption">Выбрасывается, если element или port равен null.</exception>
    /// <exception cref="TypePortConnectionExeption">Выбрасывается, если тип нового порта совпадает с типом целевого порта.</exception>
    /// <exception cref="EqvelElementGrafExeption">Выбрасывается, если новый элемент совпадает с текущим входящим элементом.</exception>
    public bool UpdateInConnection(ElementGraf element, Port port)
    {
        if (port is null)
            throw new ConnectionNullExeption(this, nameof(port), typeof(Port));

        if (port.TypePort == TargetPort!.TypePort)
            throw new TypePortConnectionExeption(this, port, TargetPort);

        if (element is null)
            throw new ConnectionNullExeption(this, nameof(element), typeof(ElementGraf));

        if (InElementGraf == element)
            throw new EqvelElementGrafExeption(this, element);

        InElementGraf = element;
        SourcePort = port;
        return true;
    }

    /// <summary>
    /// Обновляет исходящее соединение (приёмник).
    /// </summary>
    /// <param name="element">Новый элемент графа-приёмник.</param>
    /// <param name="port">Новый целевой порт.</param>
    /// <returns>true, если обновление выполнено успешно.</returns>
    /// <exception cref="ConnectionNullExeption">Выбрасывается, если element или port равен null.</exception>
    /// <exception cref="TypePortConnectionExeption">Выбрасывается, если тип нового порта совпадает с типом исходного порта.</exception>
    /// <exception cref="EqvelElementGrafExeption">Выбрасывается, если новый элемент совпадает с текущим исходящим элементом.</exception>
    public bool UpdateOutConnection(ElementGraf element, Port port)
    {
        if (port is null)
            throw new ConnectionNullExeption(this, nameof(port), typeof(Port));

        if (port.TypePort == SourcePort!.TypePort)
            throw new TypePortConnectionExeption(this, SourcePort, port);

        if (element is null)
            throw new ConnectionNullExeption(this, nameof(element), typeof(ElementGraf));

        if (OutElementGraf == element)
            throw new EqvelElementGrafExeption(this, element);

        OutElementGraf = element;
        TargetPort = port;
        return true;
    }

    /// <summary>
    /// Определяет, содержит ли соединение указанный элемент графа.
    /// </summary>
    /// <param name="element">Элемент графа для проверки.</param>
    /// <returns>true, если элемент является источником или приёмником соединения; иначе false.</returns>
    public bool IsContainsElementGraf(ElementGraf element)
        => element == InElementGraf || element == OutElementGraf;
}