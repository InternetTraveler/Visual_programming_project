namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class BaseNullExeption : ArgumentNullException
{
    public readonly object Object;
    public readonly string NameNullObj;
    public readonly Type TypeNullObj;

    public BaseNullExeption(object Obj, string nameNullObj, Type typeNullObj)
        : base($"Объект '{nameNullObj}' типа '{typeNullObj}' не может быть Null в объекта типа '{Obj.GetType()}'")
    {
        Object = Obj;
        NameNullObj = nameNullObj;
        TypeNullObj = typeNullObj;
    }
}