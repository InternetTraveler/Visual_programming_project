namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class NodeNullExeption : BaseNullExeption
{
    public NodeNullExeption(object Obj, string nameNullObj, Type typeNullObj)
        : base(Obj, nameNullObj, typeNullObj) { }
}