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

        public double Price { get { return RawPrice.HasValue ? RawPrice.Value : 0; } }
        public double? RawPrice { get { return GetInt(0); } }
        public RecipeCurrency Currency { get { return GetEnum<RecipeCurrency>(4); } }
    }
}
