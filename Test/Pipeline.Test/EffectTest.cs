namespace Pipeline.Test;

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class EffectTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("effects",
                     true,
                     Preprocessor.PreprocessDictionary<EffectDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<EffectDictionary, Effect>,
                     Preprocessor.InsertIntDictionary<EffectDictionary, Effect>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<EffectDictionary, Effect>,
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
    public void TestDescription()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Effect No Description");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestDoeEyesEmptyKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Effect Doe Eyes Empty Keywords");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestDoeEyesNoKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Effect Doe Eyes No Keywords");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestParticle()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestParticleNotAoeColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle Not Aoe Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestParticleTooManyColors()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle Too Many Colors");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestDuration()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Duration");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
