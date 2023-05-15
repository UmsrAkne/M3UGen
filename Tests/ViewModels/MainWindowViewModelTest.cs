using System.IO;
using M3UGen.Models;
using M3UGen.ViewModels;
using NUnit.Framework;

namespace Tests.ViewModels;

[TestFixture]
public class MainWindowViewModelTest
{
    private readonly string baseDirectoryName = "testDirectory";
    private readonly string soundFilesDirectoryName = "files";

    [OneTimeSetUp]
    public void TestDirectoryCreate()
    {
        Directory.CreateDirectory(baseDirectoryName);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        var f = new FileInfo(new DirectoryInfo(baseDirectoryName).FullName + "\\" + soundFilesDirectoryName + ".m3u");
        File.Delete(f.FullName);
        File.Delete("test.m3u");
        Directory.Delete(baseDirectoryName);
    }

    [Test]
    public void ExportM3UTest()
    {
        var vm = new MainWindowViewModel();
        var directoryInfo = new DirectoryInfo(baseDirectoryName);
        vm.BaseDirectoryPath = directoryInfo.FullName;

        vm.Files.Add(new ExtendFileInfo(new FileInfo($@"{soundFilesDirectoryName}\testA.mp3")));
        vm.Files.Add(new ExtendFileInfo(new FileInfo($@"{soundFilesDirectoryName}\testB.mp3")));
        var m3UFilePath = new DirectoryInfo(baseDirectoryName).FullName + "\\" + soundFilesDirectoryName + ".m3u";

        Assert.IsFalse(File.Exists(m3UFilePath), "コマンド実行前に同名ファイルは存在していない");

        vm.ExportToM3UFileCommand.Execute();
        Assert.IsTrue(File.Exists(m3UFilePath), "コマンド実行後に想定通りにファイルが存在するか");
    }
}