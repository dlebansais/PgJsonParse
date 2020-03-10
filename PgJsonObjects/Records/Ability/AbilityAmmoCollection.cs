using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityAmmoCollection : List<IPgAbilityAmmo>, IPgAbilityAmmoCollection, ISerializableJsonObjectCollection
    {
    }
}
