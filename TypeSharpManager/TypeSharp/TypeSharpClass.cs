// <copyright file="TypeSharpClass.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpClass marker attribute for TypeScript compile-able classes
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TypeSharpClass : System.Attribute
    {
    }
}
