using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSharpCommandLine
{
    using System.Diagnostics;
    using System.IO;

    using TypeSharpParser;

    public class Program
    {
        public static void Main(string[] args)
        {
            var files = Directory.EnumerateFiles(
                "C:/DEVGIT/TypeSharp/TestCompileProject",
                "*.cs",
                SearchOption.AllDirectories);

            var output = new TypeScriptGenerator(files).GenerateOutputFiles();

            Directory.Delete("C:/DEVGIT/TypeScript/TestCompileProject/bin", true);


            foreach (var file in output)
            {
                File.WriteAllText(string.Format("{0}{1}{2}", file.FilePath, Path.DirectorySeparatorChar, file.FileName), file.Syntax);
            }

            Environment.Exit(0);
        }
    }
}
