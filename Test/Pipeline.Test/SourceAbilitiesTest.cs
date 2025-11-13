namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class SourceAbilitiesTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("sources_abilities",
                     true,
                     Preprocessor.PreprocessDictionary<SourceAbilityDictionary>,
                     Fixer.FixSourceAbilities,
                     Preprocessor.SaveSerializedDictionary<SourceAbilityDictionary, SourceAbility>,
                     Preprocessor.InsertIntDictionary<SourceAbilityDictionary, SourceAbility>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<SourceAbilityDictionary, SourceAbility>,
                     new()
                     {
                     }),
    };

    private static IFreeSql Fsql = TestTools.CreateTestDatabase();

    [OneTimeTearDown]
    public void TearDown()
    {
        Fsql.Dispose();
    }

    [Test]
    public void Test()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Source Abilities");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
