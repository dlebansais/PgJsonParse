﻿namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class NpcTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("npcs", false, Preprocessor.PreprocessDictionary<NpcDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedContent<NpcDictionary>),
    };

    [Test]
    public void TestAreaName()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Npc AreaName");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
    /*
    [Test]
    public void TestDesire()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Npc No Desire");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }*/
    /*
    [Test]
    public void TestNoAreaName()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Npc No AreaName");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }*/
    /*
    [Test]
    public void TestPreferenceKeywords()
    {
        string VersionPath = TestTools.GetVersionPath("Valid Npc No Preference Keywords");

        Preprocessor Preprocessor = new();
        bool Success = Preprocessor.Preprocess(VersionPath, JsonFileList);
        Assert.That(Success, Is.True);
    }*/
}
