namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class PowerTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("tsysclientinfo", true, Preprocessor.PreprocessDictionary<PowerDictionary>, Fixer.FixPowers, Preprocessor.SaveSerializedContent<PowerDictionary>),
    };

    [Test]
    public void TestEffectDescriptionCloseBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription Close Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionEmptyName()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription Empty Name");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionEmptyValue()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription Empty Value");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionNoClosingBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription No Closing Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionNoClosingBraceSkill()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription No Closing Brace Skill");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionNoValue()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription No Value");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionOpenBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription Open Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestEffectDescriptionTooManyClosingBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power EffectDescription Too Many Closing Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestNoTiers()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Power No Tiers");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestNoEffectDescription()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Power No EffectDescription");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }
}
