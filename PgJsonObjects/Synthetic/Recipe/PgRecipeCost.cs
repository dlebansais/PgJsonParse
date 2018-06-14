using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeCost : GenericPgObject, IPgRecipeCost
    {
        public PgRecipeCost(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public double Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        public double? RawPrice { get { return GetInt(0); } }
        public RecipeCurrency Currency { get { return GetEnum<RecipeCurrency>(4); } }
    }
}
