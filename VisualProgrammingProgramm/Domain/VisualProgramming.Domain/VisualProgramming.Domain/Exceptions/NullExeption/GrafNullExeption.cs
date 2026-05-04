namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class GrafNullExeption : BaseNullExeption
{
    public GrafNullExeption(object Obj, string nameNullObj, Type typeNullObj)
        : base(Obj, nameNullObj, typeNullObj) { }
}
