using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeItem : GenericPgObject<PgRecipeItem>, IPgRecipeItem
    {
        public PgRecipeItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgRecipeItem CreateItem(byte[] data, int offset)
        {
            return new PgRecipeItem(data, offset);
        }

        public Item Item { get { return GetObject(0, ref _Item); } } private Item _Item;
        public int ItemCode { get { return RawItemCode.HasValue ? RawItemCode.Value : 0; } }
        public int? RawItemCode { get { return GetInt(4); } }
        public int StackSize { get { return RawStackSize.HasValue ? (RawStackSize.Value > 0 ? RawStackSize.Value : 1) : 0; } }
        public int? RawStackSize { get { return GetInt(8); } }
        public double PercentChance { get { return RawPercentChance.HasValue ? RawPercentChance.Value : 0; } }
        public double? RawPercentChance { get { return GetDouble(12); } }
        public List<RecipeItemKey> ItemKeyList { get { return GetEnumList(16, ref _ItemKeyList); } } private List<RecipeItemKey> _ItemKeyList;
        public ItemCollection MatchingKeyItemList { get { return GetObjectList(20, ref _MatchingKeyItemList, (byte[] data, int offset) => new PgItem(data, offset), () => new ItemCollection()); } } private ItemCollection _MatchingKeyItemList;
        public string Desc { get { return GetString(24); } }
        public double ChanceToConsume { get { return RawChanceToConsume.HasValue ? RawChanceToConsume.Value : 0; } }
        public double? RawChanceToConsume { get { return GetDouble(28); } }
        public double DurabilityConsumed { get { return RawDurabilityConsumed.HasValue ? RawDurabilityConsumed.Value : 0; } }
        public double? RawDurabilityConsumed { get { return GetDouble(32); } }
        public bool AttuneToCrafter { get { return RawAttuneToCrafter.HasValue && RawAttuneToCrafter.Value; } }
        public bool? RawAttuneToCrafter { get { return GetBool(36, 0); } }
    }
}
