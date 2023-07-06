namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class AITest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("ai", true, Preprocessor.PreprocessDictionary<AIDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AIDictionary>),
    };

    [Test]
    public void Test()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid AI Ability");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
