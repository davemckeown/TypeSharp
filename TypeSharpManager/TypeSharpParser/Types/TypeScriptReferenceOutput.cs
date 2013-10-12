// <copyright file="TypeScriptReferenceOutput.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Types
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// TypeScriptReferenceOutput manages references between multiple TypeScript files
    /// </summary>
    public class TypeScriptReferenceOutput
    {
        /// <summary>
        /// The list of TypeScriptReferenceOutput instances for external module references
        /// </summary>
        private readonly List<TypeScriptReferenceOutput> moduleReferences = new List<TypeScriptReferenceOutput>();

        /// <summary>
        /// The list of TypeScriptOutput instances for internal module references
        /// </summary>
        private readonly List<TypeScriptOutput> references = new List<TypeScriptOutput>();

        /// <summary>
        /// Initializes a new instance of the TypeScriptReferenceOutput class
        /// </summary>
        /// <param name="module">The target module</param>
        public TypeScriptReferenceOutput(string module)
        {
            this.Module = module;
        }

        /// <summary>
        /// Gets or sets the Module name
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Gets the list of external module references
        /// </summary>
        public List<TypeScriptReferenceOutput> ModuleReferences
        {
            get
            {
                return this.moduleReferences;
            }
        }

        /// <summary>
        /// Gets the list of internal module references
        /// </summary>
        public List<TypeScriptOutput> References
        {
            get
            {
                return this.references;
            }
        }

        /// <summary>
        /// Gets the TypeScript reference declarations
        /// </summary>
        public string Content
        {
            get
            {
                StringBuilder content = new StringBuilder();

                foreach (TypeScriptReferenceOutput reference in this.ModuleReferences.Distinct())
                {
                    string relativeReferencePath = this.DetermineRelativeReferencePath(reference.Module);
                    content.Append(string.Format("/// <reference path=\"{0}\" />", relativeReferencePath)).Append(Environment.NewLine);
                }

                foreach (TypeScriptOutput file in this.References)
                {
                    content.Append(string.Format("/// <reference path=\"{0}.ts\" />", file.FileName)).Append(Environment.NewLine);
                }

                return content.ToString();
            }
        }

        /// <summary>
        /// Builds the reference path based on the Module name
        /// </summary>
        /// <param name="root">The root directory</param>
        /// <param name="module">The module name</param>
        /// <returns>A full path with the root and module</returns>
        private static string BuildReferencePath(string root, string module)
        {
            StringBuilder path = new StringBuilder(root + Path.DirectorySeparatorChar);
            Stack<string> submodules = new Stack<string>();
            string submodule;

            if (module.Contains('.'))
            {
                while (module.Contains('.'))
                {
                    int index = module.LastIndexOf('.');
                    submodule = module.Substring(index + 1);
                    submodules.Push(submodule);
                    module = module.Substring(0, index);
                }

                submodules.Push(module);
            }
            else
            {
                submodules.Push(module);
            }

            while (submodules.Count > 0)
            {
                submodule = submodules.Pop();
                path.Append(submodule).Append(Path.DirectorySeparatorChar);
            }

            return path.ToString();
        }

        /// <summary>
        /// Determines the relative reference path between the target module and another module reference
        /// </summary>
        /// <param name="reference">The target reference</param>
        /// <returns>A relative URI</returns>
        private string DetermineRelativeReferencePath(string reference)
        {
            string root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Uri from = new Uri(BuildReferencePath(root, this.Module));
            Uri to = new Uri(BuildReferencePath(root, reference));

            return string.Format("{0}{1}.d.ts", Uri.UnescapeDataString(from.MakeRelativeUri(to).ToString().Replace('/', Path.DirectorySeparatorChar)), reference);
        }
    }
}
