using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Validais;
namespace VisualProgramming.ValueObject;

public class LevelOfDepth : ValueObjects<int>
{
    private static LevelOfDepthValidator validator = new LevelOfDepthValidator();
    public LevelOfDepth(int value) 
        : base(validator, value){}

    public static implicit operator LevelOfDepth(int value) => new(value);
    public static implicit operator int(LevelOfDepth value) => value.Value;
}
