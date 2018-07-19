using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public void InitFavorList(Dictionary<string, IJsonKey> ItemTable)
        {
            InitNpcFavorList(ItemTable, this);
        }

        public static void InitNpcFavorList(Dictionary<string, IJsonKey> ItemTable, IPgNpcPreference NpcPreference)
        {
             //= ObjectList.Definitions[typeof(Item)].ObjectTable;

            foreach (ItemKeyword Keyword in NpcPreference.ItemKeywordList)
            {
                if (Keyword == ItemKeyword.Internal_None)
                    continue;

                ItemCollection ItemList = new ItemCollection();

                if (Keyword == ItemKeyword.Any)
                {
                    if (NpcPreference.RawMinValueRequirement.HasValue)
                    {
                        foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
                        {
                            IPgItem ItemValue = Entry.Value as IPgItem;
                            if (ItemValue.Value >= NpcPreference.RawMinValueRequirement.Value)
                                ItemList.Add(ItemValue);
                        }
                    }
                    else if (NpcPreference.SlotRequirement != ItemSlot.Internal_None)
                    {
                        foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
                        {
                            IPgItem ItemValue = Entry.Value as IPgItem;
                            if (ItemValue.EquipSlot == NpcPreference.SlotRequirement)
                                ItemList.Add(ItemValue);
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, IJsonKey> ItemEntry in ItemTable)
                    {
                        IPgItem ItemValue = ItemEntry.Value as IPgItem;
                        foreach (KeyValuePair<ItemKeyword, List<float>> KeywordEntry in ItemValue.KeywordTable)
                            if (KeywordEntry.Key == Keyword)
                                ItemList.Add(ItemValue);
                    }
                }

                foreach (IPgItem Item in ItemList)
                {
                    if (NpcPreference.RawMinValueRequirement.HasValue && Item.Value < NpcPreference.RawMinValueRequirement.Value)
                        continue;

                    if (NpcPreference.SlotRequirement != ItemSlot.Internal_None && Item.EquipSlot != NpcPreference.SlotRequirement)
                        continue;

                    double Value = NpcPreference.Preference * Item.Value;
                    NpcPreference.ItemFavorList.Add(new Gift(Keyword, Item, Value));
                }
            }
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

        public string PreferenceType
        {
            get
            {
                if (Preference <= -2)
                    return "Hates";
                else if (Preference < 0)
                    return "Dislikes";
                else if (Preference > 2)
                    return "Loves";
                else if (Preference > 0)
                    return "Likes";
                else
                    return "";
            }
        }

        public ICollection<Gift> ItemFavorList { get; } = new ObservableCollection<Gift>();

        public override string SortingName { get { return Key; } }
        public string SearchResultIconFileName { get { return "icon_" + NpcPreference.SearchResultIconId; } }
    }
}
