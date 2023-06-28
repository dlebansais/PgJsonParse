namespace Preprocessor;

using System.Collections.Generic;

internal class StorageVaultDictionary : Dictionary<string, StorageVault>, IDictionaryValueBuilder<StorageVault, RawStorageVault>
{
    public StorageVault FromRaw(RawStorageVault rawStorageVault) => new StorageVault(rawStorageVault);
    public RawStorageVault ToRaw(StorageVault storageVault) => storageVault.ToRawStorageVault();
}
