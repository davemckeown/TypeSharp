using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpIgnore marker attribte for compilable constructors, methods and properties
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false)]
    public class TypeSharpCompile : System.Attribute
    {
    }
}
