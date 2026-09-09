namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class PortNullExeption : BaseNullExeption
{
    public PortNullExeption(object Obj, string nameNullObj, Type typeNullObj)
        : base(Obj, nameNullObj, typeNullObj) { }
}