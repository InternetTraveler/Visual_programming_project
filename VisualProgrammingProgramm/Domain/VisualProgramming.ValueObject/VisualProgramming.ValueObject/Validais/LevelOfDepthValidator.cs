using VisualProgramming.ValueObject.Base;

namespace VisualProgramming.ValueObject.Validais;

internal class LevelOfDepthValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value <= 0)
            throw new Exception();
    }
}
