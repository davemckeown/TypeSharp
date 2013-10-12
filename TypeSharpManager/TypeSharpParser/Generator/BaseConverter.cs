// <copyright file="BaseConverter.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Generator
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Roslyn.Compilers.CSharp;

    /// <summary>
    /// BaseConverter contains common code for interface and class converters
    /// </summary>
    public class BaseConverter
    {
        /// <summary>
        /// Gets or sets the TypeAggregator reference
        /// </summary>
        protected TypeAggregator ParsedTypes { get; set; }

        /// <summary>
        /// Checks a CSharp source code input string to determine if it represents a collection type
        /// </summary>
        /// <param name="parameter">The input string</param>
        /// <returns>True if determined to be a collection type</returns>
        public bool IsCollection(string parameter)
        {
            return parameter.Contains("List<") || parameter.Contains("Collection<") || parameter.Contains("Enumerable<") || parameter.Contains("[]");
        }

        /// <summary>
        /// Converts a TypeSyntax to a TypeScript source code string
        /// </summary>
        /// <param name="property">A TypeSyntax for a property</param>
        /// <param name="module">The containing module</param>
        /// <returns>A TypeScript source code representation</returns>
        public string ConvertType(TypeSyntax property, string module)
        {
            return string.Format("{0}{1}", this.ConvertToTypeScriptType(property is GenericNameSyntax ? (property as GenericNameSyntax).TypeArgumentList.Arguments[0].ToString() : property.ToString(), module), this.IsCollection(property.ToString()) ? "[]" : string.Empty);
        }

        /// <summary>
        /// Converts a CSharp type to a TypeScript type
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="module">The module</param>
        /// <returns>A TypeScript type</returns>
        protected string ConvertToTypeScriptType(string type, string module)
        {
            string moduleType = this.ParsedTypes[module, type];

            if (!string.IsNullOrEmpty(moduleType))
            {
                return moduleType;
            }

            string referencedType = this.ParsedTypes[type];

            if (!string.IsNullOrEmpty(referencedType))
            {
                this.ParsedTypes.References.AddReference(module, referencedType.Replace(string.Format(".{0}", type), string.Empty));
                return referencedType;
            }

            switch (type)
            {
                case "int":
                case "decimal":
                case "long":
                case "short":
                case "double":
                    return "number";
                case "bool":
                    return "bool";
                case "DateTime":
                    return "Date";
                case "string":
                    return "string";
                case "void":
                    return "void";
                default:
                    return "any";
            }
        }

        /// <summary>
        /// Converts the comments on a CSharp class into JSDoc comments in the TypeScript source code
        /// </summary>
        /// <param name="syntax">A ClassDeclarationSyntax to convert the comments for</param>
        /// <returns>Formatted JSDoc comments</returns>
        protected string ConvertSyntaxComments(ClassDeclarationSyntax syntax)
        {
            StringBuilder output = new StringBuilder(string.Empty);
            var comment = syntax.GetLeadingTrivia().FirstOrDefault(x => x.Kind == SyntaxKind.DocumentationCommentTrivia);

            if (comment.Kind != SyntaxKind.None)
            {
                StringBuilder xml = new StringBuilder();

                foreach (string line in comment.GetStructure().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    xml.Append(line.Trim('/', ' ', '\t'));
                }

                var nodes = XDocument.Parse("<comment>" + xml.ToString() + "</comment>");

                var summary = nodes.Descendants("summary").FirstOrDefault();

                if (summary != null)
                {
                    output.Append("/**").Append(Environment.NewLine);
                    output.Append("* @classdesc ").Append(summary.Value).Append(Environment.NewLine);
                    output.Append("*/").Append(Environment.NewLine);
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Converts the comments on a CSharp method into JSDoc comments in the TypeScript source code
        /// </summary>
        /// <param name="syntax">A MethodDeclarationSyntax to convert the comments for</param>
        /// <returns>Formatted JSDoc comments</returns>
        protected string ConvertSyntaxComments(MethodDeclarationSyntax syntax)
        {
            StringBuilder output = new StringBuilder(string.Empty);
            var comment = syntax.GetLeadingTrivia().FirstOrDefault(x => x.Kind == SyntaxKind.DocumentationCommentTrivia);

            if (comment.Kind != SyntaxKind.None)
            {
                StringBuilder xml = new StringBuilder();

                foreach (string line in comment.GetStructure().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    xml.Append(line.Trim('/', ' ', '\t'));
                }

                var nodes = XDocument.Parse("<comment>" + xml.ToString() + "</comment>");

                var summary = nodes.Descendants("summary").FirstOrDefault();

                if (summary != null)
                {
                    output.Append('\t').Append("/**").Append(Environment.NewLine);
                    output.Append('\t').Append("* ").Append(summary.Value).Append(Environment.NewLine);

                    foreach (var param in nodes.Descendants("param"))
                    {
                        output.Append('\t').Append("* @param ").Append(param.Attribute("name").Value).Append(" ").Append(param.Value).Append(Environment.NewLine);
                    }

                    output.Append(nodes.Descendants("returns").Any() ? string.Format("{0}* @return {1}{2}", '\t', nodes.Descendants("returns").First().Value, Environment.NewLine) : string.Empty);

                    output.Append('\t').Append("*/").Append(Environment.NewLine);
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Converts the comments on a CSharp property into JSDoc comments in the TypeScript source code
        /// </summary>
        /// <param name="syntax">A PropertyDeclarationSyntax to convert the comments for</param>
        /// <returns>Formatted JSDoc comments</returns>
        protected string ConvertSyntaxComments(PropertyDeclarationSyntax syntax)
        {
            StringBuilder output = new StringBuilder(string.Empty);
            var comment = syntax.GetLeadingTrivia().FirstOrDefault(x => x.Kind == SyntaxKind.DocumentationCommentTrivia);

            if (comment.Kind != SyntaxKind.None)
            {
                StringBuilder xml = new StringBuilder();

                foreach (string line in comment.GetStructure().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    xml.Append(line.Trim('/', ' ', '\t'));
                }

                var nodes = XDocument.Parse("<comment>" + xml.ToString() + "</comment>");

                var summary = nodes.Descendants("summary").FirstOrDefault();

                if (summary != null)
                {
                    output.Append('\t').Append("/**").Append(Environment.NewLine);
                    output.Append('\t').Append("* ").Append(summary.Value).Append(Environment.NewLine);
                    output.Append('\t').Append("*/").Append(Environment.NewLine);
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Converts method arguments to TypeScript string
        /// </summary>
        /// <param name="method">The method declaration</param>
        /// <param name="module">The module</param>
        /// <returns>A TypeScript string representation</returns>
        protected string ConvertMethodArguments(MethodDeclarationSyntax method, string module)
        {
            StringBuilder result = new StringBuilder();
            string args = string.Empty;

            foreach (ParameterSyntax argument in method.ParameterList.Parameters)
            {
                string argumentName = argument.Identifier.Value.ToString();
                string argumentType = this.ConvertType(argument.Type, module);

                result.Append(string.Format("{0}: {1}, ", argumentName, argumentType));
            }

            args = result.ToString();

            if (result.Length > 0)
            {
                args = args.Substring(0, args.Length - 2);
            }

            return args;
        }
    }
}
