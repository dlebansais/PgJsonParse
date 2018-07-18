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
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public RecipeCurrency Currency { get { return GetEnum<RecipeCurrency>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Currency", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeCurrency>.ToString(Currency, null, RecipeCurrency.Internal_None) } },
            { "Price", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawPrice } },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}
