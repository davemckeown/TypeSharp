// <copyright file="ClassGenerator.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.Parser.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
        public string Generate(ClassDeclarationSyntax syntax, string module, TypeAggregator parsedTypes)
        {
            StringBuilder output = new StringBuilder();
            this.ParsedTypes = parsedTypes;

            string classIdentifier = syntax.TypeParameterList == null
                                         ? syntax.Identifier.Value.ToString()
                                         : ConvertClassTypeParameters(syntax);

            output.Append(this.ConvertSyntaxComments(syntax));
            output.Append(string.Format("export class {0} {{", classIdentifier)).Append(Environment.NewLine).Append(Environment.NewLine);


            foreach (var field in syntax.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                output.Append('\t', 2).Append(string.Format("{0} {1}: {2};", field.Modifiers.Any(x => x.Kind == SyntaxKind.PrivateKeyword) ? "private" : "public", field.Declaration.Variables, field.Declaration.Type));
                output.Append(Environment.NewLine);
            }

            output.Append(Environment.NewLine);

            List<PropertyDeclarationSyntax> properties =
                syntax.DescendantNodes().OfType<PropertyDeclarationSyntax>().ToList();

            foreach (PropertyDeclarationSyntax property in properties)
            {
                output.Append('\t', 2).Append(string.Format("private _{0} : {1};", property.Identifier.Value, ConvertType(property.Type, module)));
                output.Append(Environment.NewLine);
            }

            output.Append(Environment.NewLine);

            foreach (PropertyDeclarationSyntax property in properties)
            {
                string script = ConvertPropertySyntax(property, module);
                output.Append(script);
            }

            foreach (MethodDeclarationSyntax method in syntax.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                string methodName = method.TypeParameterList == null
                                        ? method.Identifier.Value.ToString()
                                        : ConvertMethodTypeParameters(method);
                string methodArgs = this.ConvertMethodArguments(method, module);
                string methodType = ConvertType(method.ReturnType, module);
                string methodBody = ConvertMethodBody(method);

                output.Append(this.ConvertSyntaxComments(method));
                output.Append('\t').Append(string.Format("{0}({1}): {2} {{", methodName, methodArgs, methodType)).Append(Environment.NewLine);
                output.Append('\t', 2).Append(methodBody).Append(Environment.NewLine);
                output.Append('\t').Append("}").Append(Environment.NewLine).Append(Environment.NewLine);
            }

            output.Append("}").Append(Environment.NewLine);

            return output.ToString();
        }

        /// <summary>
        /// Converts the generic method type parameters in the method signature
        /// </summary>
        /// <param name="method">The method declaration syntax</param>
        /// <returns>TypeScript method string with generics</returns>
        private static string ConvertMethodTypeParameters(MethodDeclarationSyntax method)
        {
            StringBuilder signature = new StringBuilder();

            signature.Append(string.Format("{0}<", method.Identifier.Value));

            foreach (TypeParameterSyntax parameter in method.TypeParameterList.Parameters)
            {
                signature.Append(string.Format("{0}, ", parameter.Identifier));
            }

            signature.Remove(signature.Length - 2, 2);
            signature.Append(">");

            return signature.ToString();
        }

        /// <summary>
        /// Converts the generic method type parameters in the class signature
        /// </summary>
        /// <param name="syntax">The class declaration syntax</param>
        /// <returns>TypeScript class identifier with generic types</returns>
        private static string ConvertClassTypeParameters(ClassDeclarationSyntax syntax)
        {
            StringBuilder signature = new StringBuilder();

            signature.Append(string.Format("{0}<", syntax.Identifier.Value));

            foreach (TypeParameterSyntax parameter in syntax.TypeParameterList.Parameters)
            {
                signature.Append(string.Format("{0}, ", parameter.Identifier));
            }

            signature.Remove(signature.Length - 2, 2);
            signature.Append(">");

            return signature.ToString();
        }

        private string ConvertPropertySyntax(PropertyDeclarationSyntax property, string module)
        {
            StringBuilder output = new StringBuilder();

            try
            {
                string propertyName = property.Identifier.Value.ToString();
                string propertyType = ConvertType(property.Type, module);
                output.Append(this.ConvertSyntaxComments(property));

                if (property.AccessorList.Accessors.Any(x => x.Keyword.Kind == SyntaxKind.GetKeyword))
                {
                    output.Append('\t', 2).Append(string.Format("get {0}() : {1} {{ return this._{0}; }}", propertyName, propertyType));
                    output.Append(Environment.NewLine).Append(Environment.NewLine);
                }

                if (property.AccessorList.Accessors.Any(x => x.Keyword.Kind == SyntaxKind.SetKeyword))
                {
                    output.Append('\t', 2).Append(string.Format("set {0}(value: {1}) {{ this._{0} = value; }}", propertyName, propertyType));
                    output.Append(Environment.NewLine).Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
            }

            return output.ToString();
        }

        /// <summary>
        /// Generates a default body stub for a method
        /// </summary>
        /// <param name="method">The method declaration</param>
        /// <returns>The method body</returns>
        private static string ConvertMethodBody(MethodDeclarationSyntax method)
        {
            StringBuilder output = new StringBuilder(string.Empty);

            output.Append(string.Format("/** @todo Implement {0} */", method.Identifier)).Append(Environment.NewLine).Append('\t', 2);


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