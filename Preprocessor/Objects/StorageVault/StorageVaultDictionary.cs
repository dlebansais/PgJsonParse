namespace Preprocessor;

using System.Collections.Generic;

internal class StorageVaultDictionary : Dictionary<string, StorageVault>, IDictionaryValueBuilder<StorageVault, StorageVault>
{
    public StorageVault FromRaw(StorageVault storageVault) => storageVault;
    public StorageVault ToRaw(StorageVault storageVault) => storageVault;
}
