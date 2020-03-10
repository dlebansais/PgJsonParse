using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityAmmoCollection : List<IPgAbilityAmmo>, IPgAbilityAmmoCollection
    {
        public static IPgAbilityAmmo CreateItem(byte[] data, ref int offset)
        {
            PgAbilityAmmo Result = new PgAbilityAmmo(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
