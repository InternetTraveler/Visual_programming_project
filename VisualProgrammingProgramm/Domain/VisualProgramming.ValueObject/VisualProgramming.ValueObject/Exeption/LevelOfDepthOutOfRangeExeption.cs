namespace VisualProgramming.ValueObject.Exeption;

public class LevelOfDepthOutOfRangeExeption : ArgumentOutOfRangeException
{
    public readonly int Value;

    public LevelOfDepthOutOfRangeExeption(int value)
        : base("Переменая не может быть меньше или равен нулю")
    {
        Value = value;
    }
}
