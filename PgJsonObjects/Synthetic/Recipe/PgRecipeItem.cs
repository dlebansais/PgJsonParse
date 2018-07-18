using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeItem : GenericPgObject<PgRecipeItem>, IPgRecipeItem
    {
        public PgRecipeItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgRecipeItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipeItem CreateNew(byte[] data, ref int offset)
        {
            return new PgRecipeItem(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgItem Item { get { return GetObject(4, ref _Item, PgItem.CreateNew); } } private IPgItem _Item;
        public int ItemCode { get { return RawItemCode.HasValue ? RawItemCode.Value : 0; } }
        public int? RawItemCode { get { return GetInt(8); } }
        public int StackSize { get { return RawStackSize.HasValue ? (RawStackSize.Value > 0 ? RawStackSize.Value : 1) : 0; } }
        public int? RawStackSize { get { return GetInt(12); } }
        public double PercentChance { get { return RawPercentChance.HasValue ? RawPercentChance.Value : 0; } }
        public double? RawPercentChance { get { return GetDouble(16); } }
        public List<RecipeItemKey> ItemKeyList { get { return GetEnumList(20, ref _ItemKeyList); } } private List<RecipeItemKey> _ItemKeyList;
        public IPgItemCollection MatchingKeyItemList { get { return GetObjectList(24, ref _MatchingKeyItemList, PgItemCollection.CreateItem, () => new PgItemCollection()); } } private IPgItemCollection _MatchingKeyItemList;
        public string Desc { get { return GetString(28); } }
        public double ChanceToConsume { get { return RawChanceToConsume.HasValue ? RawChanceToConsume.Value : 0; } }
        public double? RawChanceToConsume { get { return GetDouble(32); } }
        public double DurabilityConsumed { get { return RawDurabilityConsumed.HasValue ? RawDurabilityConsumed.Value : 0; } }
        public double? RawDurabilityConsumed { get { return GetDouble(36); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(40, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool AttuneToCrafter { get { return RawAttuneToCrafter.HasValue && RawAttuneToCrafter.Value; } }
        public bool? RawAttuneToCrafter { get { return GetBool(44, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "ItemCode", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawItemCode } },
            { "StackSize", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawStackSize } },
            { "PercentChance", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawPercentChance } },
            { "ItemKeys", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<RecipeItemKey>.ToStringList(ItemKeyList, TextMaps.RecipeItemKeyStringMap) } },
            { "Desc", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Desc } },
            { "ChanceToConsume", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawChanceToConsume } },
            { "DurabilityConsumed", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawDurabilityConsumed } },
            { "AttuneToCrafter", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawAttuneToCrafter } },
        }; } }

        public override string SortingName { get { return null; } }
    }
}
