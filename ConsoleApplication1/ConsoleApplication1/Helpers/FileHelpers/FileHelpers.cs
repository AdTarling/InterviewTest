using System;
using System.Collections;
using System.IO;
using System.Text;
using log4net;
using log4net.Config;

namespace FundsLibraryInterviewTest.Helpers.FileHelpers
{
    public class FileHelpers
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ICollection _logConfig = BasicConfigurator.Configure();
        public static string GetByteArrayAsString(byte[] bytes)
        {

            return Encoding.ASCII.GetString(bytes);
        }

        public static bool CheckId(string bytesAsString, string iD)
        {
            return bytesAsString.Contains("/ID [" + iD + "]");
        }
        private static byte[] GetBytes(Stream fileStream)
        {
            using (BinaryReader binaryReader = new BinaryReader(fileStream))
            {
                try
                {
                    return binaryReader.ReadBytes((int) fileStream.Length);
                }
                catch(ArgumentOutOfRangeException outOfRangeException)
                {
                    LoggingHelpers.LoggingHelpers.LogError(log,log.Logger.Name,outOfRangeException.StackTrace,outOfRangeException.Message,outOfRangeException.InnerException);
                }
            }
            return new byte[0];
        }

        private static bool CheckStreamLength(Stream fileOneStream, Stream fileTwoStream)
        {
            return fileOneStream.Length == fileTwoStream.Length;
        }

        public static bool CompareFiles(Stream fileOneStream, Stream fileTwoStream, string fileOneId = "", string fileTwoId = "")
        {
            
            bool areEqualLengthStreams = CheckStreamLength(fileOneStream, fileTwoStream);
            int fileOneByte;
            int fileTwoByte;
            
            if (areEqualLengthStreams == false)
            {
                return false;
            }

            Byte[] fileStreamOneBytes = GetBytes(fileOneStream);
            Byte[] fileStreamTwoBytes = GetBytes(fileTwoStream);
            
            string fileOneBytesAsString = GetByteArrayAsString(fileStreamOneBytes);
            string fileTwoBytesAsString = GetByteArrayAsString(fileStreamTwoBytes);

            if (!String.IsNullOrEmpty(fileOneId) && !String.IsNullOrEmpty(fileTwoId))
            {
                    //Check if the ID passed in is a match -- Example: /ID [<.*>]
                    bool fileOneIdMatch = CheckId(fileOneBytesAsString, fileOneId);
                    bool fileTwoIdMatch = CheckId(fileTwoBytesAsString, fileTwoId);

                    //If we have match then we can consider the files equal and return true;
                    if (fileOneIdMatch || fileTwoIdMatch)
                    {
                        return true;
                    }
             }

            try
            {
                do
                {
                    fileOneByte = fileOneStream.ReadByte();
                    fileTwoByte = fileTwoStream.ReadByte();
                } //while the bytes are the same and we have not reached the end of the file
                while ((fileOneByte == fileTwoByte) && fileOneByte != -1);
                {
                    fileOneStream.Close();
                    fileTwoStream.Close();
                    return ((fileOneByte - fileTwoByte) == 0);
                }
            }
            catch(NotSupportedException notSupportedException)
            {
                LoggingHelpers.LoggingHelpers.LogError(log, log.Logger.Name, notSupportedException.StackTrace, notSupportedException.Message, notSupportedException.InnerException);
            }
            //Now we compare the bytes in the file
            return false;
        }
    }
}

