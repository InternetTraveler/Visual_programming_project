namespace VisualProgramming.ValueObject.Exeption;

internal class ValidatorValueObjectNullExeption : Exception
{
    Type TypeValidator { get; }
    public ValidatorValueObjectNullExeption(Type typeValidator) :
        base($"Валидатор типа '{typeValidator}' не может быть Null")
    {
        TypeValidator = typeValidator;
    }
}
