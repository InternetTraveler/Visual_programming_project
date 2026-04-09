namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class ProjectNullExeption : BaseNullExeption
{
    public ProjectNullExeption(object Obj, string nameNullObj, Type typeNullObj)
        : base(Obj, nameNullObj, typeNullObj) { }
}