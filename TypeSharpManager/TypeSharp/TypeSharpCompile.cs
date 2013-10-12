// <copyright file="TypeSharpCompile.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpIgnore marker attribute for compile-able constructors, methods and properties
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false)]
    public class TypeSharpCompile : System.Attribute
    {
    }
}
