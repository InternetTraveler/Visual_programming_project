using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

namespace VisualProgramming.ValueObject.Validais;

public class LevelOfDepthValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value <= 0)
            throw new LevelOfDepthOutOfRangeExeption(value);
    }
}
