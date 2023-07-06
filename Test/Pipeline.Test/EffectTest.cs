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
    public void TestParticle()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Effect Particle");

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
