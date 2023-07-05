namespace Preprocessor;

using System.Collections.Generic;

public class StorageVaultDictionary : Dictionary<string, StorageVault>, IDictionaryValueBuilder<StorageVault, RawStorageVault>
{
    public StorageVault FromRaw(RawStorageVault rawStorageVault) => new StorageVault(rawStorageVault);
    public RawStorageVault ToRaw(StorageVault storageVault) => storageVault.ToRawStorageVault();
}
