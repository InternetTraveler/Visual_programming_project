namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class PortNullConnection : BaseNullExeption
{
    public PortNullConnection(object Obj, string nameNullObj, Type typeNullObj)
        : base(Obj, nameNullObj, typeNullObj) { }
}