namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class SkillTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("skills", true, Preprocessor.PreprocessDictionary<SkillDictionary>, Fixer.FixSkills, Preprocessor.SaveSerializedDictionary<SkillDictionary, Skill>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Skill>)(((SkillDictionary)content).Values)).ExecuteAffrows()),
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
