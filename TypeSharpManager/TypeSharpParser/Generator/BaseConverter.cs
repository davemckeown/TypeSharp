// <copyright file="BaseConverter.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Generator
{
    using System.Text;
    using System.Linq;
    using Roslyn.Compilers.CSharp;
    using System;
    using System.Xml.Linq;

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
                string argumentType = ConvertType(argument.Type, module);

                result.Append(string.Format("{0}: {1}, ", argumentName, argumentType));
            }

            args = result.ToString();

            if (result.Length > 0)
            {
                args = args.Substring(0, args.Length - 2);
            }

            return args;
        }

        public bool IsCollection(string parameter)
        {
            return parameter.Contains("List<") || parameter.Contains("Collection<") || parameter.Contains("Enumerable<") || parameter.Contains("[]");
        }

        public string ConvertType(TypeSyntax property, string module)
        {
            return string.Format("{0}{1}", ConvertToTypeScriptType(property is GenericNameSyntax ? (property as GenericNameSyntax).TypeArgumentList.Arguments[0].ToString() : property.ToString(), module), IsCollection(property.ToString()) ? "[]" : string.Empty);
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

        protected string ConvertSyntaxComments(ClassDeclarationSyntax syntax)
        {
            StringBuilder output = new StringBuilder(string.Empty);
            var comment = syntax.GetLeadingTrivia().FirstOrDefault(x => x.Kind == SyntaxKind.DocumentationCommentTrivia);

            if (comment != null)
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
                    output.Append("* ").Append(summary.Value).Append(Environment.NewLine);
                    output.Append("*/").Append(Environment.NewLine);
                }
            }

            return output.ToString();
        }

        protected string ConvertSyntaxComments(MethodDeclarationSyntax syntax)
        {
            StringBuilder output = new StringBuilder(string.Empty);
            var comment = syntax.GetLeadingTrivia().FirstOrDefault(x => x.Kind == SyntaxKind.DocumentationCommentTrivia);

            if (comment != null)
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

        protected string ConvertSyntaxComments(PropertyDeclarationSyntax syntax)
        {
            StringBuilder output = new StringBuilder(string.Empty);
            var comment = syntax.GetLeadingTrivia().FirstOrDefault(x => x.Kind == SyntaxKind.DocumentationCommentTrivia);

            if (comment != null)
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
    }
}
