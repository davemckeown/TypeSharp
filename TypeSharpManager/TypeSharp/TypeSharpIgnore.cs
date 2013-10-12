// <copyright file="TypeSharpIgnore.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpIgnore marker attribute for ignorable methods and properties
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false)]
    public class TypeSharpIgnore : System.Attribute
    {
    }
}