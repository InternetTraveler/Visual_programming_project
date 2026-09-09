using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

namespace VisualProgramming.ValueObject.Validais;

/// <summary>
/// Представляет валидатор для проверки корректности уровня глубины.
/// </summary>
/// <remarks>
/// LevelOfDepthValidator проверяет, что значение уровня 
/// глубины является положительным числом (больше нуля).
/// </remarks>
public class LevelOfDepthValidator : IValidator<int>
{
    /// <summary>
    /// Выполняет валидацию целочисленного значения уровня глубины.
    /// </summary>
    /// <param name="value">Целочисленное значение уровня 
    /// глубины для проверки.</param>
    /// <exception cref="LevelOfDepthOutOfRangeExeption">Выбрасывается, 
    /// если значение меньше или равно нулю.</exception>
    /// <remarks>
    /// Уровень глубины должен быть положительным целым числом (1, 2, 3, ...).
    /// Значение 0 или отрицательные числа считаются недопустимыми.
    /// </remarks>
    public void Validate(int value)
    {
        if (value <= 0)
            throw new LevelOfDepthOutOfRangeExeption(value);
    }
}