using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

namespace VisualProgramming.ValueObject.Validais;

internal class LevelOfDepthValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value <= 0)
            throw new LevelOfDepthOutOfRangeExeption(value);
    }
}
