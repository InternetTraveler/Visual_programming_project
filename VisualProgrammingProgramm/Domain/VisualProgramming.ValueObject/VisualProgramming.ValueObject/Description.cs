using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Validais;

namespace VisualProgramming.ValueObject;

/// <summary>
/// Представляет объект-значение "Описание", который 
/// инкапсулирует текстовое описание с валидацией.
/// </summary>
/// <remarks>
/// Description является неизменяемым объектом-значением, 
/// обеспечивающим валидацию описания через DescriptionValidator.
/// </remarks>
public class Description : ValueObjects<string>
{
    private static DescriptionValidator validator = new DescriptionValidator();

    /// <summary>
    /// Инициализирует новый экземпляр описания.
    /// </summary>
    /// <param name="name">Текст описания.</param>
    /// <remarks>
    /// Выполняется валидация переданной строки через DescriptionValidator.
    /// Если валидация не пройдена, будет выброшено соответствующее исключение.
    /// </remarks>
    public Description(string name) : base(validator, name) { }

    /// <summary>
    /// Определяет неявное преобразование из строки в объект Description.
    /// </summary>
    /// <param name="value">Строковое значение описания.</param>
    /// <returns>Новый экземпляр Description, созданный из строки.</returns>
    public static implicit operator Description(string value) => new(value);
}