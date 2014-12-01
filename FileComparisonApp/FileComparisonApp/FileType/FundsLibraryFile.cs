using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileComparisonApp.FileType
{
    public class FundsLibraryFile
    {
        private string _fileString { get; set; }

        public FundsLibraryFile(string filePath)
        {
            if(File.Exists(filePath)){
                CreateFileString(filePath);
            }
        }

        private void CreateFileString(string filePath)
        {
            var fileLineStringArray = File.ReadAllLines(filePath);

            _fileString = string.Join("", fileLineStringArray);
        }

        public override bool Equals(object obj)
        {
            var fundsLibraryFileIncoming = obj as FundsLibraryFile;
            if ((object)fundsLibraryFileIncoming == null)
            {
                return false;
            }

            if (fundsLibraryFileIncoming.ContainsEqualityRegex() && this.ContainsEqualityRegex())
            {
                return true;
            }

            return false;
        }

        public bool ContainsEqualityRegex()
        {
            var equalityRegex = new Regex(@"ID \[<.*>\]");
            return equalityRegex.IsMatch(_fileString);
        }
    }
}
