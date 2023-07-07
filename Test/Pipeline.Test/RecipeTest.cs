﻿namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class RecipeTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("recipes", true, Preprocessor.PreprocessDictionary<RecipeDictionary>, Fixer.FixRecipes, Preprocessor.SaveSerializedContent<RecipeDictionary>),
    };

    [Test]
    public void TestAddItemTSysPower()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe AddItemTSysPower");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestAddItemTSysPowerWax()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe AddItemTSysPowerWax");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestAdjustRecipeReuseTime()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe AdjustRecipeReuseTime");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestAdjustRecipeReuseTimeSeconds()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe AdjustRecipeReuseTime Seconds");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestBrewItem()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe BrewItem");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestBrewItemParts()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe BrewItem Parts");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestConsumeItemUses()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe ConsumeItemUses");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestCraftingEnhanceItem()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe CraftingEnhanceItem");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestExtractTSysPower()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe ExtractTSysPower");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestParticle()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe Particle");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestParticleColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe Particle Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestRepairItemDurability()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe RepairItemDurability");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestTeleport()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe Teleport");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestTeleportArea()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe Teleport Area");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestTeleportDestination()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe Teleport Destination");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestTSysCraftedEquipment()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe TSysCraftedEquipment");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestTSysCraftedEquipmentEmpty()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe TSysCraftedEquipment Empty");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }

    [Test]
    public void TestParticlePrimaryColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Recipe Particle Primary Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList));
    }
}
