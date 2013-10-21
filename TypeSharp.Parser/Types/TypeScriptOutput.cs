// <copyright file="TypeScriptOutput.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.Parser.Types
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// TypeScriptOutput manages TypeScript output
    /// </summary>
    public class TypeScriptOutput
    {
        /// <summary>
        /// The content of the TypeScript file
        /// </summary>
        private readonly StringBuilder content = new StringBuilder();

        /// <summary>
        /// Initializes a new instance of the TypeScriptOutput class
        /// </summary>
        /// <param name="fileName">The filename including path</param>
        /// <param name="module">The module namespace</param>
        public TypeScriptOutput(string fileName, string module)
        {
            this.FilePath = fileName;
            this.Module = module;
        }

        /// <summary>
        /// Gets or sets the file path
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the module
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the instance is a a class
        /// </summary>
        public bool IsClass { get; set; }

        /// <summary>
        /// Gets the filename without extension
        /// </summary>
        public string FileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.FilePath);
            }
        }

        /// <summary>
        /// Gets the TypeScript content with references
        /// </summary>
        public StringBuilder Content
        {
            get
            {
                return this.content;
            }
        }
        
        /// <summary>
        /// Gets the TypeScript contents including external references
        /// </summary>
        public string Syntax
        {
            get
            {
                StringBuilder output = new StringBuilder();

                output.Append(string.Format("/// <reference path=\"{0}.d.ts\" />", this.Module)).Append(Environment.NewLine).Append(Environment.NewLine);
                output.Append(string.Format("module {0} {{", this.Module)).Append(Environment.NewLine).Append(Environment.NewLine);

                foreach (string line in this.content.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    output.Append('\t').Append(line).Append(Environment.NewLine);
                }

                output.Append("}");

                return output.ToString();
            }
        }

        /// <summary>
        /// Gets the TypeScript contents of the TestClass including external references
        /// </summary>
        public string TestSyntax
        {
            get
            {
                StringBuilder testClass = new StringBuilder();
                testClass.Append(string.Format("/// <reference path=\"{0}{1}.ts\" />", this.DetermineRelativeReferencePath(this.Module), this.FileName)).Append(Environment.NewLine);
                testClass.Append(string.Format("/// <reference path=\"{0}tsUnit.ts\" />", this.DetermineRelativeReferencePath(string.Empty))).Append(Environment.NewLine).Append(Environment.NewLine);
                testClass.Append(string.Format("module {0} {{", BuildTestModulePath(this.Module).Replace('\\', '.').Trim('.'))).Append(Environment.NewLine).Append(Environment.NewLine);
                testClass.Append('\t').Append(string.Format("export class {0}Tests extends tsUnit.TestClass {{", this.FileName)).Append(Environment.NewLine).Append(Environment.NewLine);
                testClass.Append('\t').Append("}");
                testClass.Append(Environment.NewLine).Append(Environment.NewLine);
                testClass.Append("}");

                return testClass.ToString();
            }
        }

        /// <summary>
        /// Builds the reference path to a target module
        /// </summary>
        /// <param name="root">The root directory</param>
        /// <param name="module">The target module</param>
        /// <returns>A full path including module</returns>
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
        /// Builds the test module path based on the actual implementation module
        /// </summary>
        /// <param name="module">The target module</param>
        /// <returns>A path for the test module</returns>
        private static string BuildTestModulePath(string module)
        {
            StringBuilder path = new StringBuilder();
            Stack<string> submodules = new Stack<string>();
            string submodule;

            if (module.Contains('.'))
            {
                while (module.Contains('.'))
                {
                    int index = module.LastIndexOf('.');
                    submodule = module.Substring(index + 1);
                    submodules.Push(string.Format("{0}.Tests", submodule));
                    module = module.Substring(0, index);
                }

                submodules.Push(string.Format("{0}.Tests", module));
            }
            else
            {
                submodules.Push(string.Format("{0}.Tests", module));
            }

            while (submodules.Count > 0)
            {
                submodule = submodules.Pop();
                path.Append(submodule).Append(Path.DirectorySeparatorChar);
            }

            return path.ToString();
        }

        /// <summary>
        /// Builds a relative reference path between two files
        /// </summary>
        /// <param name="reference">The target reference</param>
        /// <returns>A relative reference path</returns>
        private string DetermineRelativeReferencePath(string reference)
        {
            string root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Uri from = new Uri(string.Format("{0}{1}{2}", root, Path.DirectorySeparatorChar, BuildTestModulePath(this.Module)));

            Uri to = !string.IsNullOrEmpty(reference) ? new Uri(BuildReferencePath(root, reference)) : new Uri(string.Format("{0}{1}", root, Path.DirectorySeparatorChar));

            return Uri.UnescapeDataString(from.MakeRelativeUri(to).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
