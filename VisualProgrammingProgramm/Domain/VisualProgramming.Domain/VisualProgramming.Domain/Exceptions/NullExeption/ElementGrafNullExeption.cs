namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class ElementGrafNullExeption : BaseNullExeption
{
    public ElementGrafNullExeption(object Obj, string nameNullObj, Type typeNullObj) 
        : base(Obj, nameNullObj, typeNullObj) {}
}
