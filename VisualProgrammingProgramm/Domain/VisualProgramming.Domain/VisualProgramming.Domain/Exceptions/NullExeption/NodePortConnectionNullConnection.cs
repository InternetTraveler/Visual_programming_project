namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class NodePortConnectionNullConnection : BaseNullExeption
{
    public NodePortConnectionNullConnection(object Obj, string nameNullObj, Type typeNullObj)
        : base(Obj, nameNullObj, typeNullObj) { }
}