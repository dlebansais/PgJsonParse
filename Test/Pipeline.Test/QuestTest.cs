namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class QuestTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("quests", true, Preprocessor.PreprocessDictionary<QuestDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<QuestDictionary>),
    };

    [Test]
    public void TestPreGiveEffect()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest PreGiveEffect");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestPreGiveEffectWarCacheMap()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest PreGiveEffect War Cache Map");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestRewardEffectFavor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest Reward Effect Favor");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestRewardEffectScripted()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest Reward Effect Scripted");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestRewardEffectSkill()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest Reward Effect Skill");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestRewardEffectXP()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest Reward Effect XP");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestObjectiveTargetArea()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest Objective Target Area");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestObjectiveTargetTooMany()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Quest Objective Target Too Many");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
