
namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpInterface marker attribute inferface for TypeScript compilable interfaces
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class TypeSharpInterface : System.Attribute
    {
    }
}
