using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeCost : GenericPgObject<PgRecipeCost>, IPgRecipeCost
    {
        public PgRecipeCost(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgRecipeCost CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipeCost CreateNew(byte[] data, ref int offset)
        {
            return new PgRecipeCost(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public double Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        public double? RawPrice { get { return GetDouble(4); } }
        public RecipeCurrency Currency { get { return GetEnum<RecipeCurrency>(8); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
