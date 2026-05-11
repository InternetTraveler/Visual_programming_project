namespace VisualProgramming.ValueObject.Exeption;

internal class ValueObjectNullException : Exception
{
    string NameValue { get; }
    Type TypeValue { get; }

    public ValueObjectNullException(string name, Type type) :
        base($"Переменая '{name}' типа '{type}' не может быть Null")
    {  
        NameValue = name; 
        TypeValue = type;
    }
}
