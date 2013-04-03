
namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpIgnore marker attribte for ignorable methods and properties
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class TypeSharpIgnore : System.Attribute
    {
    }
}

