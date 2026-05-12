using VisualProgramming.ValueObject.Base;

namespace VisualProgramming.ValueObject;

/// <summary>
/// Представляет объект-значение "Имя", который инкапсулирует 
/// строковое имя с валидацией.
/// </summary>
/// <remarks>
/// Name является неизменяемым объектом-значением, обеспечивающим
/// валидацию имени через NameValidator.
/// Используется для именования проектов, узлов и других сущностей в системе.
/// </remarks>
public class Name : ValueObjects<string>
{
    private static NameValidator validator = new NameValidator();

    /// <summary>
    /// Инициализирует новый экземпляр имени.
    /// </summary>
    /// <param name="name">Строковое значение имени.</param>
    /// <remarks>
    /// Выполняется валидация переданной строки через NameValidator.
    /// Если валидация не пройдена, будет выброшено соответствующее исключение.
    /// </remarks>
    public Name(string name) : base(validator, name) { }

    /// <summary>
    /// Определяет неявное преобразование из строки в объект Name.
    /// </summary>
    /// <param name="value">Строковое значение имени.</param>
    /// <returns>Новый экземпляр Name, созданный из строки.</returns>
    public static implicit operator Name(string value) => new(value);
}