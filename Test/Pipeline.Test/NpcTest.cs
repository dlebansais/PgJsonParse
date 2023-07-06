namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class NpcTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("npcs", false, Preprocessor.PreprocessDictionary<NpcDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<NpcDictionary>),
    };

    [Test]
    public void TestAreaName()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Npc AreaName");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
