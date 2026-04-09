using VisualProgramming.Domain.Base;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Domain.Entites;

public class Modul : BaseNode
{
    public Graf GrafOperation { get; private set; }

    public Modul(Name name, Graf grafOPeration) :
        base(name)
    {
        GrafOperation = grafOPeration;
    }
}
