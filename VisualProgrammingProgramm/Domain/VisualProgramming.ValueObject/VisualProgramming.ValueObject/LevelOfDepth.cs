using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Validais;

namespace VisualProgramming.ValueObject;

/// <summary>
/// Представляет объект-значение "Уровень глубины", который 
/// инкапсулирует целочисленное значение с валидацией.
/// </summary>
/// <remarks>
/// LevelOfDepth является неизменяемым объектом-значением, 
/// используемым для определения уровня вложенности
/// операций в графе. Автоматически валидируется через LevelOfDepthValidator.
/// </remarks>
public class LevelOfDepth : ValueObjects<int>
{
    private static LevelOfDepthValidator validator = new LevelOfDepthValidator();

    /// <summary>
    /// Инициализирует новый экземпляр уровня глубины.
    /// </summary>
    /// <param name="value">Целочисленное значение уровня глубины.</param>
    /// <remarks>
    /// Выполняется валидация переданного числа через LevelOfDepthValidator.
    /// Если валидация не пройдена, будет выброшено соответствующее исключение.
    /// </remarks>
    public LevelOfDepth(int value)
        : base(validator, value) { }

    /// <summary>
    /// Определяет неявное преобразование из целого числа в объект LevelOfDepth.
    /// </summary>
    /// <param name="value">Целочисленное значение уровня глубины.</param>
    /// <returns>Новый экземпляр LevelOfDepth, созданный из целого числа.</returns>
    public static implicit operator LevelOfDepth(int value) => new(value);

    /// <summary>
    /// Определяет неявное преобразование из объекта LevelOfDepth в целое число.
    /// </summary>
    /// <param name="value">Объект LevelOfDepth для преобразования.</param>
    /// <returns>Целочисленное значение уровня глубины.</returns>
    public static implicit operator int(LevelOfDepth value) => value.Value;
}