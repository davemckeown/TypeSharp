using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeSharp;

namespace ExampleProject.Model
{
    [TypeSharpInterface]
    public interface IContext
    {
        string Name { get; set; }
    }
}
