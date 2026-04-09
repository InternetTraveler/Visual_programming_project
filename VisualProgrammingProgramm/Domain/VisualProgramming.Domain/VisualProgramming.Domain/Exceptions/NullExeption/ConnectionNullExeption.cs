using System;
using System.Collections.Generic;
using System.Text;

namespace VisualProgramming.Domain.Exceptions.NullExeption;

public class ConnectionNullExeption : BaseNullExeption
{
    public ConnectionNullExeption(object Obj, string nameNullObj, Type typeNullObj) 
        : base(Obj, nameNullObj, typeNullObj){}
}
