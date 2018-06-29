using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemUses : MainPgObject<PgItemUses>, IPgItemUses
    {
        public PgItemUses(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 4;
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
    }
}
