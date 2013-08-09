
namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpIgnore marker attribte for ignorable methods and properties
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false)]
    public class TypeSharpIgnore : System.Attribute
    {
    }
}

