using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityCollection : List<IPgAbility>, IPgAbilityCollection
    {
        public static PgAbility CreateItem(byte[] data, ref int offset)
        {
            PgAbility Result = PgAbility.CreateNew(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
