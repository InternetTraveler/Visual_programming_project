using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

namespace VisualProgramming.ValueObject.Validais;

/// <summary>
/// Представляет валидатор для проверки корректности описания.
/// </summary>
/// <remarks>
/// DescriptionValidator проверяет, что описание:
/// <list type="bullet">
/// <item><description>Не является null или пустой строкой</description></item>
/// <item><description>Имеет длину от 5 до 50 символов 
/// (включительно) после удаления пробелов</description></item>
/// </list>
/// </remarks>
public class DescriptionValidator : IValidator<string>
{
    /// <summary>
    /// Получает максимально допустимую длину описания.
    /// </summary>
    /// <value>Максимальная длина составляет 50 символов.</value>
    public static int MaxLenghts => 50;

    /// <summary>
    /// Получает минимально допустимую длину описания.
    /// </summary>
    /// <value>Минимальная длина составляет 5 символов.</value>
    public static int MinLenghts => 5;

    /// <summary>
    /// Выполняет валидацию строки описания.
    /// </summary>
    /// <param name="value">Строка описания для проверки.</param>
    /// <exception cref="DescriptionNullOrEmptyException">Выбрасывается,
    /// если строка описания является null или пустой.</exception>
    /// <exception cref="DescriprionTooLongException">Выбрасывается,
    /// если длина описания превышает максимально допустимую (50 символов).</exception>
    /// <exception cref="DescriptionTooShortException">Выбрасывается,
    /// если длина описания меньше минимально допустимой (5 символов).</exception>
    /// <remarks>
    /// Перед проверкой длины из строки удаляются начальные 
    /// и конечные пробелы методом Trim().
    /// </remarks>
    public void Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new DescriptionNullOrEmptyException(value);

        value = value.Trim();
        if (value.Length > MaxLenghts)
            throw new DescriprionTooLongException(value, value.Length, MaxLenghts);

        if (value.Length < MinLenghts)
            throw new DescriptionTooShortException(value, value.Length, MinLenghts);
    }
}