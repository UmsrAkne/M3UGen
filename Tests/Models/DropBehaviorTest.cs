using M3UGen.Models;
using NUnit.Framework;

namespace Tests.Models;

public class DropBehaviorTest
{
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