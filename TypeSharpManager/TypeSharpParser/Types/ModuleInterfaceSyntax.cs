// <copyright file="ModuleInterfaceSyntax.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Types
{
    using Roslyn.Compilers.CSharp;

    /// <summary>
    /// ModuleInterfaceSyntax contains a reference to a CSharp source files interface declaration
    /// </summary>
    public class ModuleInterfaceSyntax
    {
        /// <summary>
        /// Gets or sets the path to the source file
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets or sets the name of the interface
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the reference to the Roslyn InterfaceDeclarationSyntax
        /// </summary>
        public InterfaceDeclarationSyntax Syntax { get; set; }
    }
}
