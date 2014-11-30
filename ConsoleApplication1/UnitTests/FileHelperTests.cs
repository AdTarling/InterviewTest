using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundsLibraryInterviewTest.Helpers.FileHelpers;
using Microsoft.SqlServer.Server;
using NUnit.Framework;

namespace UnitTests
{   
  [TestFixture]
    public class FileHelperTests
  {

      private static byte[] fileOneBytes;
      private static FileStream fileStreamOne;
      private static FileStream fileStreamTwo;
      private const string fileOnePath = "C:\\Projects\\FundsLibrary\\InterviewTest\\file_0.bin";
      private const string fileTwoPath = "C:\\Projects\\FundsLibrary\\InterviewTest\\file_1.bin";
      [TestFixtureSetUp]
      public static void Startup()
      {
          fileOneBytes = File.ReadAllBytes(fileOnePath);   
      }

      [Test]
      public void TestWhenIdIsNotAMatchFilesAreNotEqual()
      {
          //ARRANGE
                        fileStreamOne = new FileStream(fileOnePath, FileMode.Open);
                        fileStreamTwo = new FileStream(fileTwoPath, FileMode.Open);
          string        bytesAsString = FileHelpers.GetByteArrayAsString(fileOneBytes);
          const string  iD = "/ID [<.*>]";
          const bool    expected = false;

          //ACT
          bool          actual = FileHelpers.CheckId(bytesAsString, iD);
          
          //ASSERT
          Assert.AreEqual(actual,expected);
          
          //CLOSE STREAMS
          fileStreamOne.Close();
          fileStreamTwo.Close();
      }

      [Test]
      public void TestWhenIdIsAMatchFileAreEqual()
      {
          //ARRANGE
          fileStreamOne = new FileStream(fileOnePath, FileMode.Open);
          fileStreamTwo = new FileStream(fileTwoPath, FileMode.Open);
          string bytesAsString = FileHelpers.GetByteArrayAsString(fileOneBytes);
          const string iD = "<41667C9D25F55FF22D2A7A93B949D904>\n<36E081B23E919687F6F7198B39158ED0>";
          const bool expected = true;
          
          //ACT
          bool actual = FileHelpers.CheckId(bytesAsString, iD);
          
          //ASSERT
          Assert.AreEqual(actual, expected);
          
          //CLOSE STREAMS
          fileStreamOne.Close();
          fileStreamTwo.Close();
          
      }

      [Test]
      public void TestCompareFilesMethodReturnFalseWhenTwoDiffrentFilesArePassedIn()
      {

          //ARRANGE
          fileStreamOne = new FileStream(fileOnePath, FileMode.Open);
          fileStreamTwo = new FileStream(fileTwoPath, FileMode.Open);
          const bool expected = true;
          
          //ACT
          bool actual = FileHelpers.CompareFiles(fileStreamOne, fileStreamTwo,
              "<41667C9D25F55FF22D2A7A93B949D904>\n<36E081B23E919687F6F7198B39158ED>",
              "<41667C9D25F55FF22D2A7A93B949D904>\n<36E081B23E919687F6F7198B39158ED>");
          
          //ASSERT
          Assert.AreEqual(actual, expected);
          
          //CLOSE STREAMS
          fileStreamOne.Close();
          fileStreamTwo.Close();
      }
       
    }
}
