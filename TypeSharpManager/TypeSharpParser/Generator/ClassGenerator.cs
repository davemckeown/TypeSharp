// <copyright file="ClassGenerator.cs" company="TypeSharp Project">
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
    /// ClassGenerator that converts a ClassDeclarationSyntax to TypeScript
    /// </summary>
    public class ClassGenerator : BaseConverter
    {
        /// <summary>
        /// Generates the TypeScript based on a ClassDeclarationSyntax
        /// </summary>
        /// <param name="syntax">The class declaration syntax</param>
        /// <param name="module">The module</param>
        /// <param name="parsedTypes">TypeAggregator reference</param>
        /// <returns>TypeScript syntax</returns>
        public string Generate(Roslyn.Compilers.CSharp.ClassDeclarationSyntax syntax, string module, TypeAggregator parsedTypes)
        {
            StringBuilder output = new StringBuilder();
            this.ParsedTypes = parsedTypes;

            output.Append(this.ConvertSyntaxComments(syntax));
            output.Append(string.Format("export class {0} {{", syntax.Identifier.Value.ToString())).Append(Environment.NewLine).Append(Environment.NewLine);

            foreach (PropertyDeclarationSyntax property in syntax.DescendantNodes().OfType<PropertyDeclarationSyntax>())
            {
                string propertyName = property.Identifier.Value.ToString();
                string propertyType = ConvertType(property.Type, module);

                try
                {
                    output.Append(this.ConvertSyntaxComments(property));
                    output.Append('\t').Append(string.Format("{0}: {1};", propertyName, propertyType)).Append(Environment.NewLine).Append(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    var str = ex.ToString();
                }
            }

            foreach (MethodDeclarationSyntax method in syntax.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                string methodName = method.Identifier.Value.ToString();
                string methodArgs = this.ConvertMethodArguments(method, module);
                string methodType = ConvertType(method.ReturnType, module);
                string methodBody = this.ConvertMethodBody(method);

                output.Append(this.ConvertSyntaxComments(method));
                output.Append('\t').Append(string.Format("{0}({1}): {2} {{", methodName, methodArgs, methodType)).Append(Environment.NewLine);
                output.Append('\t', 2).Append(methodBody).Append(Environment.NewLine);
                output.Append('\t').Append("}").Append(Environment.NewLine).Append(Environment.NewLine);
            }

            output.Append("}").Append(Environment.NewLine);

            return output.ToString();
        }

        /// <summary>
        /// Generates a default body stub for a method
        /// </summary>
        /// <param name="method">The method declaration</param>
        /// <returns>The method body</returns>
        private string ConvertMethodBody(MethodDeclarationSyntax method)
        {
            StringBuilder output = new StringBuilder(string.Empty);

            output.Append(string.Format("/** @todo Implement {0} */", method.Identifier.ToString())).Append(Environment.NewLine).Append('\t', 2);

            switch (method.ReturnType.ToString())
            {
                case "int":
                case "decimal":
                case "long":
                case "short":
                case "double":
                    output.Append("return 0;");
                    break;
                case "string":
                    return "return \"\";";
                case "void":
                    output.Append("return;");
                    break;
                default:
                    output.Append("return null;");
                    break;
            }

            return output.ToString();
        }
    }
}