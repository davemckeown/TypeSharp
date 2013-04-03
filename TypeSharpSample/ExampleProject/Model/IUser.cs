using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeSharp;

namespace ExampleProject.Model
{
    [TypeSharpInterface]
    public interface IUser
    {
        string Name { get; set; }
        string Organization { get; set; }
        decimal CombineArguments(string argument1, int argument2);
    }
}
