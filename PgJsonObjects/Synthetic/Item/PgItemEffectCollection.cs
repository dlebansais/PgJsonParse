using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemEffectCollection : List<IPgItemEffect>, IPgItemEffectCollection
    {
        public static IPgItemEffect CreateItem(byte[] data, ref int offset)
        {
            IPgItemEffect Result = CreateNew(data, ref offset);
            return Result;
        }

        public static IPgItemEffect CreateNew(byte[] data, ref int offset)
        {
            int Type = BitConverter.ToInt32(data, offset);
            bool IsSimple = (Type == 0);

            if (IsSimple)
            {
                PgItemSimpleEffect Result = PgItemSimpleEffect.CreateNew(data, ref offset);
                Result.Init();
                return Result;
            }
            else
            {
                PgItemAttributeLink Result = PgItemAttributeLink.CreateNew(data, ref offset);
                Result.Init();
                return Result;
            }
        }
    }
}
