// <copyright file="ModuleClassSyntax.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Types
{
    using Roslyn.Compilers.CSharp;

    /// <summary>
    /// ModuleClassSyntax contains a reference to a CSharp source files class declaration
    /// </summary>
    public class ModuleClassSyntax
    {
        /// <summary>
        /// Gets or sets the path to the CSharp source file
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets or sets the name of the class
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the Roslyn ClassDeclarationSyntax
        /// </summary>
        public ClassDeclarationSyntax Syntax { get; set; }
    }
}
