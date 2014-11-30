using System;
using System.Collections;
using System.IO;
using FundsLibraryInterviewTest.Helpers;
using FundsLibraryInterviewTest.Helpers.FileHelpers;
using FundsLibraryInterviewTest.Helpers.LoggingHelpers;
using log4net;
using log4net.Config;

namespace FundsLibraryInterviewTest
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ICollection _logConfig = BasicConfigurator.Configure() ;
        private static void Main(string[] args)
        {
            string fileOneId = String.Empty;
            string fileTwoId = String.Empty;

            if (args.Length < 1)
            {
                Console.WriteLine("Please enter a file path of two files you wish to compare");
            }
            if (args.Length == 3)
            {
                fileOneId = args[2];
                fileTwoId = args[3];
            }
            else
            {
                try
                {

                    Stream fileOneStream = new FileStream(args[0], FileMode.Open);
                    Stream fileTwoStream = new FileStream(args[1], FileMode.Open);
                    
                    bool equal = FileHelpers.CompareFiles(fileOneStream, fileTwoStream, fileOneId, fileTwoId);
                    Console.WriteLine(equal);
                }
                catch (FileNotFoundException exception)
                {
                    LoggingHelpers.LogError(log, log.Logger.Name, exception.StackTrace, exception.Message, exception.InnerException);

                }
                catch (PathTooLongException exception)
                {
                    
                    LoggingHelpers.LogError(log, log.Logger.Name, exception.StackTrace, exception.Message, exception.InnerException);
                }

                Console.WriteLine("Please press any key to continue");
                Console.ReadKey(true);
            }

        }
    }
}
    
