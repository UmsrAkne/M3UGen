using System.IO;
using M3UGen.Models;
using NUnit.Framework;

namespace Tests.Models
{
    [TestFixture]
    public class ExtendFileInfoTest
    {
        [Test]
        public void RelativePathTest()
        {
            var f = new ExtendFileInfo(new FileInfo(@"C:\test\file01"));
            f.BaseOfRelativePath = @"C:\test\";
            Assert.AreEqual(@"file01", f.RelativePath, "同じディレクトリ");

            f.BaseOfRelativePath = @"C:\other\";
            Assert.AreEqual(@"..\test\file01", f.RelativePath, "同階層に設置された別のディレクトリ");

            f.BaseOfRelativePath = @"C:\other\other2\";
            Assert.AreEqual(@"..\..\test\file01", f.RelativePath, "一つ深い階層に設置された別のディレクトリ");
        }
    }
}