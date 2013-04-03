namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpClass marker attribute for TypeScript compilable classes
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TypeSharpClass : System.Attribute
    {
    }
}
