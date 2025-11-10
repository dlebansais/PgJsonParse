namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class ItemTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("items", true, Preprocessor.PreprocessDictionary<ItemDictionary>, Fixer.NoFix, Preprocessor.SaveSerializedDictionary<ItemDictionary, Item>, (IFreeSql fsql, object content) => fsql.Insert((IEnumerable<Item>)(((ItemDictionary)content).Values)).ExecuteAffrows()),
    };

    private static IFreeSql Fsql = TestTools.CreateTestDatabase();

    [OneTimeTearDown]
    public void TearDown()
    {
        Fsql.Dispose();
    }

    [Test]
    public void TestDroppedAppearance()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item DroppedAppearance");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestDroppedAppearanceSkin()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item DroppedAppearance Skin");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestDroppedAppearanceSkinEmpty()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item DroppedAppearance Skin Empty");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestDroppedAppearanceOther()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item DroppedAppearance Other");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionCloseBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Close Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionOpenBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Open Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionAttributeEmptyName()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Attribute Empty Name");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionAttributeEmptyValue()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Attribute Empty Value");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionAttributeNoClosingBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Attribute No Closing Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionAttributeNoValue()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Attribute No Value");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestEffectDescriptionAttributeTooManyClosingBrace()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item EffectDescription Attribute Too Many Closing Brace");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestKeywordTooManyValues()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item Keyword Too Many Values");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestStockDyeGlow()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item StockDye Glow");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestStockDyeNotAColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item StockDye Not A Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }

    [Test]
    public void TestStockDyeNotEnoughColor()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Item StockDye Not Enough Color");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
