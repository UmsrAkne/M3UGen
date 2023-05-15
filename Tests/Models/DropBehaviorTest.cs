using System.Collections.Generic;
using System.IO;
using System.Linq;
using M3UGen.Models;
using NUnit.Framework;

namespace Tests.Models;

public class DropBehaviorTest
{
    private string testDirectoryName = "testDirectoryName";

    [SetUp]
    public void TestSetUp()
    {
        Directory.CreateDirectory(testDirectoryName);
        File.Create($@"{testDirectoryName}\a.mp3").Close();
        File.Create($@"{testDirectoryName}\b.mp3").Close();
        File.Create($@"{testDirectoryName}\c.mp3").Close();
        File.Create($@"{testDirectoryName}\d.jpg").Close();
    }

    [TearDown]
    public void TestTearDown()
    {
        File.Delete($@"{testDirectoryName}\a.mp3");
        File.Delete($@"{testDirectoryName}\b.mp3");
        File.Delete($@"{testDirectoryName}\c.mp3");
        File.Delete($@"{testDirectoryName}\d.jpg");
        Directory.Delete(testDirectoryName);
    }

    [Test]
    public void GetSoundFilesTest()
    {
        var d = new DropBehavior();

        var files = d.GetSoundFiles(new[] { testDirectoryName }).Select(p => p.ToString()).ToList();
        var expectedPaths = new List<string>
        {
            $@"{testDirectoryName}\a.mp3",
            $@"{testDirectoryName}\b.mp3",
            $@"{testDirectoryName}\c.mp3",
        };

        CollectionAssert.AreEqual(expectedPaths, files);
    }

    [Test]
    public void IsSoundFileTest()
    {
        var d = new DropBehavior();

        Assert.IsTrue(d.IsSoundFile("a.mp3"));
        Assert.IsTrue(d.IsSoundFile("a.wav"));
        Assert.IsTrue(d.IsSoundFile("a.ogg"));

        Assert.IsTrue(d.IsSoundFile("a.Mp3"));
        Assert.IsTrue(d.IsSoundFile("a.Wav"));
        Assert.IsTrue(d.IsSoundFile("a.Ogg"));

        Assert.IsTrue(d.IsSoundFile("a.MP3"));
        Assert.IsTrue(d.IsSoundFile("a.WAV"));
        Assert.IsTrue(d.IsSoundFile("a.OGG"));

        Assert.IsTrue(d.IsSoundFile(@"C:\a.mp3"));
        Assert.IsTrue(d.IsSoundFile(@"C:\a.wav"));
        Assert.IsTrue(d.IsSoundFile(@"C:\a.ogg"));

        Assert.IsTrue(d.IsSoundFile(@"C:\a.Mp3"));
        Assert.IsTrue(d.IsSoundFile(@"C:\a.Wav"));
        Assert.IsTrue(d.IsSoundFile(@"C:\a.Ogg"));

        Assert.IsTrue(d.IsSoundFile(@"C:\a.MP3"));
        Assert.IsTrue(d.IsSoundFile(@"C:\a.WAV"));
        Assert.IsTrue(d.IsSoundFile(@"C:\a.OGG"));

        Assert.IsFalse(d.IsSoundFile(@"a.jpg"));
        Assert.IsFalse(d.IsSoundFile(@"C:\a.jpg"));
    }
}