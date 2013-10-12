// <copyright file="TypeScriptGenerator.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Roslyn.Compilers.CSharp;
    using TypeSharpParser.Generator;
    using TypeSharpParser.Types;

    /// <summary>
    /// The TypeScriptGenerator converts CSharp source files into TypeScript
    /// </summary>
    public class TypeScriptGenerator
    {
        /// <summary>
        /// Container for parsed types that appear in TypeSharp target files
        /// </summary>
        private readonly TypeAggregator parsedTypes = new TypeAggregator();

        /// <summary>
        /// The interface generator
        /// </summary>
        private readonly InterfaceGenerator interfaceBuilder = new InterfaceGenerator();

        /// <summary>
        /// The class generator
        /// </summary>
        private readonly ClassGenerator classBuilder = new ClassGenerator();

        /// <summary>
        /// Initializes a new instance of the TypeScriptGenerator class by accepting a sequence of input files
        /// </summary>
        /// <param name="paths">
        /// The source input files
        /// </param>
        public TypeScriptGenerator(IEnumerable<string> paths)
        {
            this.parsedTypes.ParseInputSources(paths);
        }

        /// <summary>
        /// Generates a list of TypeScript output files
        /// </summary>
        /// <returns>A list of TypeScriptOutput instances</returns>
        public List<TypeScriptOutput> GenerateOutputFiles()
        {
            Dictionary<string, TypeScriptOutput> generatedTargets = new Dictionary<string, TypeScriptOutput>(); 

            foreach (string module in this.parsedTypes.Modules)
            {
                foreach (ModuleClassSyntax unit in this.parsedTypes.GetModuleClasses(module))
                {
                    if (!generatedTargets.ContainsKey(unit.SourceFile))
                    {
                        generatedTargets.Add(unit.SourceFile, new TypeScriptOutput(unit.SourceFile, module) { IsClass = true });
                    }

                    generatedTargets[unit.SourceFile].Content.Append(this.GenerateClassSyntax(unit.Syntax, module));
                }

                foreach (ModuleInterfaceSyntax unit in this.parsedTypes.GetModuleInterfaces(module))
                {
                    if (!generatedTargets.ContainsKey(unit.SourceFile))
                    {
                        generatedTargets.Add(unit.SourceFile, new TypeScriptOutput(unit.SourceFile, module) { IsClass = false });
                    }

                    generatedTargets[unit.SourceFile].Content.Append(this.GenerateInterfaceSyntax(unit.Syntax, module));
                }
            }

            return generatedTargets.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Generates a lookup for references based on the module as a key
        /// </summary>
        /// <param name="output">
        /// The list out TypeScriptOutput instances
        /// </param>
        /// <returns>
        /// A dictionary of TypeScriptReferenceOutput instances mapped by module
        /// </returns>
        public Dictionary<string, TypeScriptReferenceOutput> GenerateReferenceOutputFiles(List<TypeScriptOutput> output)
        {
            Dictionary<string, TypeScriptReferenceOutput> result = new Dictionary<string, TypeScriptReferenceOutput>();

            // Resolve local references
            foreach (string module in output.Select(x => x.Module).Distinct())
            {
                TypeScriptReferenceOutput reference = new TypeScriptReferenceOutput(module);

                foreach (TypeScriptOutput file in output.Where(x => x.Module == module))
                {
                    reference.References.Add(file);
                }

                result.Add(module, reference);
            }

            foreach (string module in output.Select(x => x.Module).Distinct())
            {
                if (this.parsedTypes.References[module] != null)
                {
                    foreach (string reference in this.parsedTypes.References[module])
                    {
                        result[module].ModuleReferences.Add(result[reference]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a Roslyn InterfaceDeclarationSyntax into a TypeScript interface
        /// </summary>
        /// <param name="syntax">
        /// The Roslyn InterfaceDeclarationSyntax
        /// </param>
        /// <param name="module">
        /// The namespace of the file
        /// </param>
        /// <returns>
        /// A TypeScript interface declaration inside specified module
        /// </returns>
        private string GenerateInterfaceSyntax(InterfaceDeclarationSyntax syntax, string module)
        {
            return this.interfaceBuilder.Generate(syntax, module, this.parsedTypes);
        }

        /// <summary>
        /// Converts a Roslyn ClassDeclarationSyntax into a TypeScript class
        /// </summary>
        /// <param name="syntax">
        /// The Roslyn ClassDeclarationSyntax
        /// </param>
        /// <param name="module">
        /// The namespace of the file
        /// </param>
        /// <returns>
        /// A TypeScript class declaration inside specified module
        /// </returns>
        private string GenerateClassSyntax(ClassDeclarationSyntax syntax, string module)
        {
            return this.classBuilder.Generate(syntax, module, this.parsedTypes);
        }
    }
}
