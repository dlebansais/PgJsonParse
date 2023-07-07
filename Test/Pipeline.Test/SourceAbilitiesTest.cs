namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class SourceAbilitiesTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("sources_abilities", true, Preprocessor.PreprocessDictionary<SourceAbilityDictionary>, Fixer.FixSourceAbilities, Preprocessor.SaveSerializedContent<SourceAbilityDictionary>),
    };

    [Test]
    public void Test()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Source Abilities");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
