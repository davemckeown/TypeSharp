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

            if (Directory.Exists("C:/DEVGIT/TypeSharp/TestCompileProject/typesharp_bin"))
            {
                Directory.Delete("C:/DEVGIT/TypeSharp/TestCompileProject/typesharp_bin", true);
            }

            Directory.CreateDirectory("C:/DEVGIT/TypeSharp/TestCompileProject/typesharp_bin");

            foreach (var file in output)
            {
                File.WriteAllText(string.Format("C:/DEVGIT/TypeSharp/TestCompileProject/typesharp_bin/{0}.ts", file.FileName), file.Syntax);
            }

            Environment.Exit(0);
        }
    }
}
