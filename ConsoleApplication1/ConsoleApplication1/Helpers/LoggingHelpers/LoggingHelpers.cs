using System;
using log4net;
using log4net.Config;

namespace FundsLibraryInterviewTest.Helpers.LoggingHelpers
{
    public class LoggingHelpers
    {
        public static void LogError(ILog log, string loggerName, string stackTrace, string message, Exception innerException)
        {
            BasicConfigurator.Configure();
            log.Error("An error was thrown from" + " " + loggerName + " " +
                      "The error was" + " " + message + " " + "the stack trace was" + stackTrace + " " + "The inner exception was" + innerException);
        }
    }
}
