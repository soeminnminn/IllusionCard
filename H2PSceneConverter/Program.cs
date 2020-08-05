using System;
using System.IO;
using System.Reflection;
using CommandLine;

namespace H2PConverter
{
    public class Options
    {
        #region Properties
        [Value(0, Required = true, HelpText = "Set HS Scene card to convert.")]
        public string FilePath { get; set; }

        [Option('o', "output", HelpText = "Set output folder path.")]
        public string OutputPath { get; set; }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(Run);

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Run(Options options)
        {
            string filePath = options.FilePath.Replace("\"", "").Trim();
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error >> File not specified.");
                return;
            }

            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                Console.WriteLine("Error >> File not found.");
                return;
            }

            DirectoryInfo outDirectory = null;
            if (!string.IsNullOrEmpty(options.OutputPath))
            {
                var dir = new DirectoryInfo(options.OutputPath);
                try
                {
                    if (!dir.Exists) dir.Create();
                    outDirectory = dir;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error >> {e.Message}");
                    return;
                }
            }

            if (outDirectory == null)
            {
                var baseDir = AssemblyDirectory(Assembly.GetExecutingAssembly());
                var dir = new DirectoryInfo(Path.Combine(baseDir, "output"));

                try
                {
                    if (!dir.Exists) dir.Create();
                    outDirectory = dir;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error >> {e.Message}");
                    return;
                }
            }

            Converter.convert(file.FullName, outDirectory.FullName);
        }

        private static string AssemblyDirectory(Assembly assembly)
        {
#if CORE
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
#else
            return Path.GetDirectoryName(assembly.Location);
#endif
        }
    }
}
