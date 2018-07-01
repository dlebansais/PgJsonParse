using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemUses : MainPgObject<PgItemUses>, IPgItemUses
    {
        public PgItemUses(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 12;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgItemUses CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItemUses CreateNew(byte[] data, ref int offset)
        {
            return new PgItemUses(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public List<int> RecipesThatUseItemList { get { return GetIntList(4, ref _RecipesThatUseItemList); } } private List<int> _RecipesThatUseItemList;
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "RecipesThatUseItem", new FieldParser() {
                Type = FieldType.SimpleIntegerArray,
                GetIntegerArray = () => RecipesThatUseItemList } },
        }; } }
    }
}
