using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

/// <summary>
/// Представляет валидатор для проверки корректности имени.
/// </summary>
/// <remarks>
/// NameValidator проверяет, что имя:
/// <list type="bullet">
/// <item><description>Не является null или пустой строкой</description></item>
/// <item><description>Имеет длину от 5 до 50 символов (включительно)
/// после удаления пробелов</description></item>
/// </list>
/// </remarks>
public class NameValidator : IValidator<string>
{
    /// <summary>
    /// Получает максимально допустимую длину имени.
    /// </summary>
    /// <value>Максимальная длина составляет 50 символов.</value>
    public static int MaxLenghts => 50;

    /// <summary>
    /// Получает минимально допустимую длину имени.
    /// </summary>
    /// <value>Минимальная длина составляет 5 символов.</value>
    public static int MinLenghts => 5;

    /// <summary>
    /// Выполняет валидацию строки имени.
    /// </summary>
    /// <param name="value">Строка имени для проверки.</param>
    /// <exception cref="NameNullOrEmptyException">Выбрасывается,
    /// если строка имени является null или пустой.</exception>
    /// <exception cref="NameTooLongException">Выбрасывается,
    /// если длина имени превышает максимально допустимую (50 символов).</exception>
    /// <exception cref="NameTooShortException">Выбрасывается,
    /// если длина имени меньше минимально допустимой (5 символов).</exception>
    /// <remarks>
    /// Перед проверкой длины из строки удаляются начальные и конечные пробелы методом Trim().
    /// Имена проектов, узлов и других сущностей должны соответствовать этим требованиям.
    /// </remarks>
    public void Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new NameNullOrEmptyException(value);

        value = value.Trim();
        if (value.Length > MaxLenghts)
            throw new NameTooLongException(value, value.Length, MaxLenghts);

        if (value.Length < MinLenghts)
            throw new NameTooShortException(value, value.Length, MinLenghts);
    }
}