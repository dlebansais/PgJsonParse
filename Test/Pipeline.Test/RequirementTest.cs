namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class RequirementTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("quests",
                     true,
                     Preprocessor.PreprocessDictionary<QuestDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<QuestDictionary, Quest>,
                     Preprocessor.InsertIntDictionary<QuestDictionary, Quest>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<QuestDictionary, Quest>,
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
    public void TestArea()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Requirement Area");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestAreaHeader()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Requirement Area Header");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
