namespace Pipeline.Test;

using System.Collections.Generic;
using NUnit.Framework;
using Preprocessor;

[TestFixture]
public class StorageVaultTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("storagevaults",
                     true,
                     Preprocessor.PreprocessDictionary<StorageVaultDictionary>,
                     Fixer.FixStorageVaults,
                     Preprocessor.SaveSerializedDictionary<StorageVaultDictionary, StorageVault>,
                     Preprocessor.InsertStringDictionary<StorageVaultDictionary, StorageVault>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<StorageVaultDictionary, StorageVault>,
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
    public void TestArea()
    {
        string VersionPath = TestTools.GetVersionPath("Invalid Storage Vault Area");

        Preprocessor Preprocessor = new();
        Assert.Throws<PreprocessorException>(() => Preprocessor.Preprocess(VersionPath, JsonFileList, Fsql, out _));
    }
}
