// <copyright file="TypeAggregator.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.Parser.Generator
{
    using System.Collections.Generic;
    using System.Linq;

    using Roslyn.Compilers.CSharp;

    using TypeSharp.Parser.Types;

    using TypeSharp.Parser.Types;

    /// <summary>
    /// TypeAggregator manages the parsed types and compiler symbols for a set of input files
    /// </summary>
    public class TypeAggregator
    {
        /// <summary>
        /// The reference aggregator
        /// </summary>
        private readonly ReferenceAggregator referenceAggregator = new ReferenceAggregator();

        /// <summary>
        /// The namespace and list of filename/syntax tree mappings for sources in that namespace
        /// </summary>
        private readonly Dictionary<string, List<KeyValuePair<string, SyntaxTree>>> namespaceSyntaxTrees = new Dictionary<string, List<KeyValuePair<string, SyntaxTree>>>();

        /// <summary>
        /// The namespace and interfaces contained in that namespace
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, ModuleInterfaceSyntax>> namespaceInterfaces = new Dictionary<string, Dictionary<string, ModuleInterfaceSyntax>>();

        /// <summary>
        /// The namespace and classes contained in that namespace
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, ModuleClassSyntax>> namespaceClasses = new Dictionary<string, Dictionary<string, ModuleClassSyntax>>();

        /// <summary>
        /// Gets the list of modules that have been parsed
        /// </summary>
        public List<string> Modules
        {
            get
            {
                IEnumerable<string> interfaceFiles = this.namespaceInterfaces.Select(x => x.Key);
                IEnumerable<string> classFiles = this.namespaceClasses.Select(x => x.Key);

                return interfaceFiles.Union(classFiles).Distinct().ToList();
            }
        }

        /// <summary>
        /// Gets the reference aggregator
        /// </summary>
        public ReferenceAggregator References
        {
            get
            {
                return this.referenceAggregator;
            }
        }

        /// <summary>
        /// Indexes the module and type to a simple type identifier
        /// </summary>
        /// <param name="module">The module</param>
        /// <param name="type">The type</param>
        /// <returns>type if namespace or class exists in module</returns>
        public string this[string module, string type]
        {
            get
            {
                if (this.namespaceInterfaces.ContainsKey(module) && this.namespaceInterfaces[module].Select(x => x.Value).Any(x => x.Identifier == type))
                {
                    return type;
                }

                if (this.namespaceClasses.ContainsKey(module) && this.namespaceClasses[module].Select(x => x.Value).Any(x => x.Identifier == type))
                {
                    return type;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Indexes a type to the module and type full name
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The fully qualified type name</returns>
        public string this[string type]
        {
            get
            {
                foreach (string module in this.namespaceInterfaces.Keys)
                {
                    if (this.namespaceInterfaces[module].Select(x => x.Value).Any(x => x.Identifier == type))
                    {
                        return string.Format("{0}.{1}", module, type);
                    }
                }

                foreach (string module in this.namespaceClasses.Keys)
                {
                    if (this.namespaceClasses[module].Select(x => x.Value).Any(x => x.Identifier == type))
                    {
                        return string.Format("{0}.{1}", module, type);
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the list of interfaces inside a module
        /// </summary>
        /// <param name="module">The target module</param>
        /// <returns>A list of ModuleInterfaceSyntax instances</returns>
        public List<ModuleInterfaceSyntax> GetModuleInterfaces(string module)
        {
            IEnumerable<ModuleInterfaceSyntax> interfaces = Enumerable.Empty<ModuleInterfaceSyntax>();

            if (this.namespaceInterfaces.ContainsKey(module))
            {
                interfaces = this.namespaceInterfaces[module].Select(x => x.Value);
            }

            return interfaces.ToList();
        }

        /// <summary>
        /// Gets a list of ModuleClassSyntax instances for a module
        /// </summary>
        /// <param name="module">The target module</param>
        /// <returns>A list of ModuleClassSyntax instances for a module</returns>
        public List<ModuleClassSyntax> GetModuleClasses(string module)
        {
            IEnumerable<ModuleClassSyntax> classes = Enumerable.Empty<ModuleClassSyntax>();

            if (this.namespaceClasses.ContainsKey(module))
            {
                classes = this.namespaceClasses[module].Select(x => x.Value);
            }

            return classes.ToList();
        }

        /// <summary>
        /// Parse input of source files creating source trees per namespace
        /// </summary>
        /// <param name="paths">The input files</param>
        public void ParseInputSources(IEnumerable<string> paths)
        {
            Dictionary<string, SyntaxTree> sourceSyntaxTrees = new Dictionary<string, SyntaxTree>();

            foreach (string path in paths)
            {
                if (!sourceSyntaxTrees.ContainsKey(path))
                {
                    SyntaxTree tree = SyntaxTree.ParseFile(path);
                    sourceSyntaxTrees.Add(path, tree);

                    CompilationUnitSyntax root = tree.GetRoot() as CompilationUnitSyntax;

                    foreach (NamespaceDeclarationSyntax module in root.DescendantNodes().OfType<NamespaceDeclarationSyntax>())
                    {
                        string name = module.Name.ToString();

                        if (!this.namespaceSyntaxTrees.ContainsKey(name))
                        {
                            this.namespaceSyntaxTrees.Add(name, new List<KeyValuePair<string, SyntaxTree>>());
                        }

                        this.namespaceSyntaxTrees[name].Add(new KeyValuePair<string, SyntaxTree>(path, tree));
                    }
                }
            }

            this.ParseInterfaceTypes();
            this.ParseClassTypes();
        }

        /// <summary>
        /// Extracts the InterfaceDeclarationSyntax nodes from the SyntaxTree and maps them by namespace
        /// </summary>
        private void ParseInterfaceTypes()
        {
            foreach (KeyValuePair<string, List<KeyValuePair<string, SyntaxTree>>> module in this.namespaceSyntaxTrees)
            {
                foreach (KeyValuePair<string, SyntaxTree> file in module.Value)
                {
                    CompilationUnitSyntax root = file.Value.GetRoot() as CompilationUnitSyntax;

                    foreach (InterfaceDeclarationSyntax unit in root.DescendantNodes().OfType<InterfaceDeclarationSyntax>())
                    {
                        if (unit.AttributeLists != null && unit.AttributeLists[0].Attributes.Any(x => x.Name.ToString().Contains("TypeSharpCompile")))
                        {
                            if (!this.namespaceInterfaces.ContainsKey(module.Key))
                            {
                                this.namespaceInterfaces.Add(module.Key, new Dictionary<string, ModuleInterfaceSyntax>());
                            }

                            this.namespaceInterfaces[module.Key].Add(file.Key, new ModuleInterfaceSyntax() { SourceFile = file.Key, Identifier = unit.Identifier.Value.ToString(), Syntax = unit });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the ClassDeclarationSyntax nodes from the SyntaxTree and maps them by namespace
        /// </summary>
        private void ParseClassTypes()
        {
            foreach (KeyValuePair<string, List<KeyValuePair<string, SyntaxTree>>> module in this.namespaceSyntaxTrees)
            {
                foreach (KeyValuePair<string, SyntaxTree> file in module.Value)
                {
                    CompilationUnitSyntax root = file.Value.GetRoot() as CompilationUnitSyntax;

                    foreach (ClassDeclarationSyntax unit in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
                    {
                        if (unit.AttributeLists != null && unit.AttributeLists[0].Attributes.Any(x => x.Name.ToString().Contains("TypeSharpCompile")))
                        {
                            if (!this.namespaceClasses.ContainsKey(module.Key))
                            {
                                this.namespaceClasses.Add(module.Key, new Dictionary<string, ModuleClassSyntax>());
                            }

                            this.namespaceClasses[module.Key].Add(file.Key, new ModuleClassSyntax() { SourceFile = file.Key, Identifier = unit.Identifier.Value.ToString(), Syntax = unit });
                        }
                    }
                }
            }
        }
    }
}
