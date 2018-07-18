using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgNpcPreference : GenericPgObject<PgNpcPreference>, IPgNpcPreference
    {
        public PgNpcPreference(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgNpcPreference CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgNpcPreference CreateNew(byte[] data, ref int offset)
        {
            return new PgNpcPreference(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public List<ItemKeyword> ItemKeywordList { get { return GetEnumList(4, ref _ItemKeywordList); } } private List<ItemKeyword> _ItemKeywordList;
        public List<string> RawKeywordList { get { return GetStringList(8, ref _RawKeywordList); } } private List<string> _RawKeywordList;
        public double Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public double? RawPreference { get { return GetDouble(12); } }
        public int MinValueRequirement { get { return RawMinValueRequirement.HasValue ? RawMinValueRequirement.Value : 0; } }
        public int? RawMinValueRequirement { get { return GetInt(16); } }
        public IPgSkill SkillRequirement { get { return GetObject(20, ref _SkillRequirement, PgSkill.CreateNew); } } private IPgSkill _SkillRequirement;
        public ItemSlot SlotRequirement { get { return GetEnum<ItemSlot>(24); } }
        public RecipeItemKey RarityRequirement { get { return GetEnum<RecipeItemKey>(26); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public RecipeItemKey MinRarityRequirement { get { return GetEnum<RecipeItemKey>(32); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Keywords", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => RawKeywordList } },
            { "Pref", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawPreference } },
        }; } }

        public override string SortingName { get { return Key; } }
        public string SearchResultIconFileName { get { return "icon_" + NpcPreference.SearchResultIconId; } }
    }
}
