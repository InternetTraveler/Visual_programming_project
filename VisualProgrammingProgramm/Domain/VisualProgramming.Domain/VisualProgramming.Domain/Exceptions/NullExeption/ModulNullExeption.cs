namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class ModulNullExeption : BaseNullExeption
{
    public ModulNullExeption(object Obj, string nameNullObj, Type typeNullObj) 
        : base(Obj, nameNullObj, typeNullObj) {}
}