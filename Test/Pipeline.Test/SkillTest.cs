namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class SkillTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("skills", true, Preprocessor.PreprocessDictionary<SkillDictionary>, Fixer.FixSkills, Preprocessor.SaveSerializedContent<SkillDictionary>),
    };

    [Test]
    public void TestRecipeIngredientKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Skill RecipeIngredientKeywords");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, out _));
    }
}
