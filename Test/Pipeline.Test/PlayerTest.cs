namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class PlayerTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("playertitles", true, Preprocessor.PreprocessDictionary<PlayerTitleDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<PlayerTitleDictionary>),
    };

    [Test]
    public void Test()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Player Title");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestNoColor()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Player Title No Color");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Player Title Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
