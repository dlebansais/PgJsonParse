using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityAmmo : MainPgObject<PgAbilityAmmo>, IPgAbilityAmmo
    {
        public PgAbilityAmmo(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 10;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAbilityAmmo CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityAmmo CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityAmmo(data, ref offset);
        }

        public override string Key { get { return null; } }
        public int Count { get { return RawCount.HasValue ? RawCount.Value : 0; } }
        public int? RawCount { get { return GetInt(0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(4, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public ItemKeyword Keyword { get { return GetEnum<ItemKeyword>(8); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "ItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => StringToEnumConversion<ItemKeyword>.ToString(Keyword) } },
            { "Count", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawCount } },
        }; } }

        public override string SortingName { get { return null; } }
    }
}
