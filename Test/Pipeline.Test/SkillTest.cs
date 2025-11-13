namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class SkillTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("skills",
                     true,
                     Preprocessor.PreprocessDictionary<SkillDictionary>,
                     Fixer.FixSkills,
                     Preprocessor.SaveSerializedDictionary<SkillDictionary, Skill>,
                     Preprocessor.InsertStringDictionary<SkillDictionary, Skill>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<SkillDictionary, Skill>,
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
    public void TestRecipeIngredientKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Skill RecipeIngredientKeywords");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
