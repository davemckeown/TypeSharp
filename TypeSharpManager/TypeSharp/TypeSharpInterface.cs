// <copyright file="TypeSharpInterface.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp
{
    using System;

    /// <summary>
    /// TypeSharpInterface marker attribute interface for TypeScript compile-able interfaces
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class TypeSharpInterface : System.Attribute
    {
    }
}
