// <copyright file="InterfaceGenerator.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Generator
{
    using System;
    using System.Linq;
    using System.Text;
    using Roslyn.Compilers.CSharp;

    /// <summary>
    /// The InterfaceGenerator that converts an InterfaceDeclarationSyntax to TypeScript
    /// </summary>
    public class InterfaceGenerator : BaseConverter
    {
        /// <summary>
        /// Generates the TypeScript for an interface
        /// </summary>
        /// <param name="syntax">The interface syntax</param>
        /// <param name="module">The module</param>
        /// <param name="parsedTypes">TypeAggregator reference</param>
        /// <returns>TypeScript syntax</returns>
        public string Generate(InterfaceDeclarationSyntax syntax, string module, TypeAggregator parsedTypes)
        {
            StringBuilder output = new StringBuilder();
            this.ParsedTypes = parsedTypes;

            output.Append(string.Format("export interface {0} {{", syntax.Identifier.Value.ToString())).Append(Environment.NewLine).Append(Environment.NewLine);

            foreach (PropertyDeclarationSyntax property in syntax.DescendantNodes().OfType<PropertyDeclarationSyntax>())
            {
                string propName = property.Identifier.Value.ToString();
                string propType = ConvertType(property.Type, module);

                output.Append(this.ConvertSyntaxComments(property));
                output.Append('\t').Append(string.Format("{0}: {1};", propName, propType)).Append(Environment.NewLine).Append(Environment.NewLine);
            }

            foreach (MethodDeclarationSyntax method in syntax.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                string methName = method.Identifier.Value.ToString();
                string methArgs = ConvertMethodArguments(method, module);
                string methType = ConvertType(method.ReturnType, module);

                output.Append(this.ConvertSyntaxComments(method));
                output.Append('\t').Append(string.Format("{0}({1}): {2};", methName, methArgs, methType)).Append(Environment.NewLine).Append(Environment.NewLine);
            }

            output.Append("}").Append(Environment.NewLine);

            return output.ToString();
        }
    }
}
