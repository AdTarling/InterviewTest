using FileComparisonApp.FileType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileComparisonApp
{
    public class Program
    {
        private static List<FundsLibraryFile> _fundsLibraryFiles;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the FundsLibrary File Comparison App");

            _fundsLibraryFiles = new List<FundsLibraryFile>();

            ReadFileInput(1);
            ReadFileInput(2);

            if (_fundsLibraryFiles[0].Equals(_fundsLibraryFiles[1]))
            {
                Console.WriteLine("Files are equal");
            }
            else
            {
                Console.WriteLine("Files are not equal");
            }

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        private static void ReadFileInput(int fileNumber)
        {
            Console.WriteLine(string.Format("Please enter location of file {0} to compare:", fileNumber));
            var filePath1 = Console.ReadLine();
            if (File.Exists(filePath1))
            {
                var fundsLibraryFile = new FundsLibraryFile(filePath1);
                _fundsLibraryFiles.Add(fundsLibraryFile);
                Console.WriteLine("Processed");
            }
            else
            {
                Console.WriteLine("No file found at specified location.");
                ReadFileInput(fileNumber);
            }
        }
    }
}
