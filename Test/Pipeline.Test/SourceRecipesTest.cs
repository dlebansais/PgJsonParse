namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class SourceRecipesTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("sources_recipes", true, Preprocessor.PreprocessDictionary<SourceRecipeDictionary>, Fixer.FixSourceRecipes, Preprocessor.SaveSerializedDictionary<SourceRecipeDictionary, SourceRecipe>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<SourceRecipe>)(((SourceRecipeDictionary)content).Values)).ExecuteAffrows()),
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
        string VersionPath = TestTools.GetVersionPath("Invalid Source Recipes");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
