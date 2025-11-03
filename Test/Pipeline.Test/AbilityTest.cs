namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class AbilityTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("abilities", true, Preprocessor.PreprocessDictionary<AbilityDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<AbilityDictionary>),
    };

    [Test]
    public void TestTargetTypeTagReq()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Ability TargetTypeTagReq");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, out _));
    }

    [Test]
    public void TestItemKeywordReqs()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Ability ItemKeywordReqs");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, out _));
    }

    [Test]
    public void TestTooManyParticles()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Ability Too Many Particles");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, out _));
    }

    [Test]
    public void TestTooManyColors()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Ability Too Many Colors");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, out _));
    }

    [Test]
    public void TestNoPvE()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Ability No PvE");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList, out _);
        Assert.That(Success, Is.True);
    }
    /*
    [Test]
    public void TestPvESelfPreEffect()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Ability PvE SelfPreEffect");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }*/
    /*
    [Test]
    public void TestPvESelfPreEffectGalvanize()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Ability PvE SelfPreEffect Galvanize");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }*/
}
