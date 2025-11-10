namespace Preprocessor;

using System.Collections.Generic;

public class StorageVaultDictionary : Dictionary<string, StorageVault>, IDictionaryValueBuilderString<StorageVault, RawStorageVault>
{
    public StorageVault FromRaw(string key, RawStorageVault rawStorageVault) => new(key, rawStorageVault);
    public RawStorageVault ToRaw(StorageVault storageVault) => storageVault.ToRawStorageVault();
}
