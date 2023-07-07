namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class EffectTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("effects", true, Preprocessor.PreprocessDictionary<EffectDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<EffectDictionary>),
    };

    [Test]
    public void TestDescription()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Effect No Description");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestDoeEyesEmptyKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Effect Doe Eyes Empty Keywords");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestDoeEyesNoKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Effect Doe Eyes No Keywords");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }

    [Test]
    public void TestParticle()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestParticleNotAoeColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle Not Aoe Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestParticleTooManyColors()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle Too Many Colors");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
