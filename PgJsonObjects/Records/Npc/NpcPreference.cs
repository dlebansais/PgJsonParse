using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PgJsonObjects
{
    public class NpcPreference : GenericJsonObject<NpcPreference>, IPgNpcPreference
    {
        #region Direct Properties
        public List<ItemKeyword> ItemKeywordList { get; private set; } = new List<ItemKeyword>();
        public List<string> RawKeywordList { get; private set; } = new List<string>();
        public double Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public double? RawPreference { get; private set; }
        public int MinValueRequirement { get { return RawMinValueRequirement.HasValue ? RawMinValueRequirement.Value : 0; } }
        public int? RawMinValueRequirement { get; private set; }
        public IPgSkill SkillRequirement { get; private set; }
        public ItemSlot SlotRequirement { get; private set; }
        public RecipeItemKey RarityRequirement { get; private set; }
        public RecipeItemKey MinRarityRequirement { get; private set; }
        private PowerSkill RawSkillRequirement;
        private bool IsSkillParsed;
        #endregion

        #region Indirect Properties
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

        private bool IsItemFavorListParsed;
        private bool IsSortIncreasing;
        public ObservableCollection<Gift> ItemFavorList { get; private set; } = new ObservableCollection<Gift>();

        public void SortByValue()
        {
            IsSortIncreasing = !IsSortIncreasing;

            List<Gift> CurrentList = new List<Gift>(ItemFavorList);

            if (IsSortIncreasing)
                CurrentList.Sort(SortGiftByIncreasingValue);
            else
                CurrentList.Sort(SortGiftByDecreasingValue);

            ItemFavorList.Clear();
            foreach (Gift Item in CurrentList)
                ItemFavorList.Add(Item);
        }

        private static int SortGiftByIncreasingValue(Gift g1, Gift g2)
        {
            if (g1.Value > g2.Value)
                return 1;
            else if (g1.Value < g2.Value)
                return -1;
            else
                return 0;
        }

        private static int SortGiftByDecreasingValue(Gift g1, Gift g2)
        {
            return -SortGiftByIncreasingValue(g1, g2);
        }

        public void SortByName()
        {
            IsSortIncreasing = !IsSortIncreasing;

            List<Gift> CurrentList = new List<Gift>(ItemFavorList);

            if (IsSortIncreasing)
                CurrentList.Sort(SortGiftByIncreasingName);
            else
                CurrentList.Sort(SortGiftByDecreasingName);

            ItemFavorList.Clear();
            foreach (Gift Item in CurrentList)
                ItemFavorList.Add(Item);
        }

        private static int SortGiftByIncreasingName(Gift g1, Gift g2)
        {
            return string.Compare(g1.Item.Name, g2.Item.Name);
        }

        private static int SortGiftByDecreasingName(Gift g1, Gift g2)
        {
            return -SortGiftByIncreasingName(g1, g2);
        }

        public override string SortingName { get { return Key; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + NpcPreference.SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Keywords", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseKeywords,
                GetStringArray = () => RawKeywordList } },
            { "Pref", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawPreference = value,
                GetFloat = () => RawPreference } },
        }; } }

        private bool ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            RawKeywordList.Add(RawKeyword);

            if (RawKeyword.StartsWith("MinValue:"))
                return ParseKeywordAsMinValue(RawKeyword.Substring(9), ErrorInfo);
            else if (RawKeyword.StartsWith("SkillPrereq:"))
                return ParseKeywordAsSkillRequirement(RawKeyword.Substring(12), ErrorInfo);
            else if (RawKeyword.StartsWith("EquipmentSlot:"))
                return ParseKeywordAsEquipmentSlot(RawKeyword.Substring(14), ErrorInfo);
            else if (RawKeyword.StartsWith("MinRarity:"))
                return ParseKeywordAsMinRarity(RawKeyword.Substring(10), ErrorInfo);
            else if (RawKeyword.StartsWith("Rarity:"))
                return ParseKeywordAsRarity(RawKeyword.Substring(7), ErrorInfo);
            else
            {
                ItemKeyword ParsedItemKeyword;
                if (StringToEnumConversion<ItemKeyword>.TryParse(RawKeyword, out ParsedItemKeyword, ErrorInfo))
                {
                    ItemKeywordList.Add(ParsedItemKeyword);
                    return true;
                }
                else
                    return false;
            }
        }

        private bool ParseKeywordAsMinValue(string value, ParseErrorInfo ErrorInfo)
        {
            int ParsedMinValueRequirement;
            if (int.TryParse(value, out ParsedMinValueRequirement))
            {
                RawMinValueRequirement = ParsedMinValueRequirement;
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(MinValue)");
                return false;
            }
        }

        private bool ParseKeywordAsSkillRequirement(string SkillRequirementString, ParseErrorInfo ErrorInfo)
        {
            if (RawSkillRequirement == PowerSkill.Internal_None)
            {
                PowerSkill ParsedSkill;
                StringToEnumConversion<PowerSkill>.TryParse(SkillRequirementString, out ParsedSkill, ErrorInfo);
                RawSkillRequirement = ParsedSkill;
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(SkillPrereq)");
                return false;
            }
        }

        private bool ParseKeywordAsEquipmentSlot(string EquipmentSlotString, ParseErrorInfo ErrorInfo)
        {
            if (SlotRequirement == ItemSlot.Internal_None)
            {
                ItemSlot ParsedSlot;
                StringToEnumConversion<ItemSlot>.TryParse(EquipmentSlotString, out ParsedSlot, ErrorInfo);
                SlotRequirement = ParsedSlot;
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(EquipmentSlot)");
                return false;
            }
        }

        private bool ParseKeywordAsMinRarity(string MinRarityString, ParseErrorInfo ErrorInfo)
        {
            if (RarityRequirement == RecipeItemKey.Internal_None)
            {
                if (MinRarityString == "Uncommon")
                    MinRarityRequirement = RecipeItemKey.MinRarity_Uncommon;
                else if (MinRarityString == "Rare")
                    MinRarityRequirement = RecipeItemKey.MinRarity_Rare;
                else
                {
                    ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(MinRarity)");
                    return false;
                }

                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(MinRarity)");
                return false;
            }
        }

        private bool ParseKeywordAsRarity(string RarityString, ParseErrorInfo ErrorInfo)
        {
            if (RarityRequirement == RecipeItemKey.Internal_None)
            {
                if (RarityString == "Common")
                    RarityRequirement = RecipeItemKey.Rarity_Common;
                else if (RarityString == "Uncommon")
                    RarityRequirement = RecipeItemKey.Rarity_Uncommon;
                else
                {
                    ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(Rarity)");
                    return false;
                }

                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(Rarity)");
                return false;
            }
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, PreferenceType);

                foreach (ItemKeyword Keyword in ItemKeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[Keyword]);

                if (SkillRequirement != null)
                    AddWithFieldSeparator(ref Result, SkillRequirement.Name);

                if (SlotRequirement != ItemSlot.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemSlotTextMap[SlotRequirement]);
                if (RarityRequirement != RecipeItemKey.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeItemKeyTextMap[RarityRequirement]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            if (ItemKeywordList.Count == 0)
                ItemKeywordList.Add(ItemKeyword.Any);

            SkillRequirement = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkillRequirement, SkillRequirement, ref IsSkillParsed, ref IsConnected, Parent as GameNpc);

            IPgItemCollection ItemList = new ItemCollection();
            foreach (ItemKeyword Keyword in ItemKeywordList)
            {
                if (Keyword == ItemKeyword.Internal_None)
                    continue;

                (ItemList as IPgCollection).Clear();

                if (Keyword == ItemKeyword.Any)
                {
                    if (RawMinValueRequirement.HasValue)
                    {
                        foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
                        {
                            Item ItemValue = Entry.Value as Item;
                            if (ItemValue.Value >= RawMinValueRequirement.Value)
                                ItemList.Add(ItemValue);
                        }
                    }
                    else if (SlotRequirement != ItemSlot.Internal_None)
                    {
                        foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
                        {
                            Item ItemValue = Entry.Value as Item;
                            if (ItemValue.EquipSlot == SlotRequirement)
                                ItemList.Add(ItemValue);
                        }
                    }
                }
                else
                    ItemList = Item.ConnectByKeyword(ErrorInfo, ItemTable, Keyword, ItemList, ref IsItemFavorListParsed, ref IsConnected, Parent as GameNpc);

                foreach (Item Item in ItemList)
                {
                    if (RawMinValueRequirement.HasValue && Item.Value < RawMinValueRequirement.Value)
                        continue;

                    if (SlotRequirement != ItemSlot.Internal_None && Item.EquipSlot != SlotRequirement)
                        continue;

                    double Value = Preference * Item.Value;
                    ItemFavorList.Add(new Gift(Keyword, Item, Value));
                }
            }

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "NpcPreferences"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddEnumList(ItemKeywordList, data, ref offset, BaseOffset, 4, StoredEnumListTable);
            AddStringList(RawKeywordList, data, ref offset, BaseOffset, 8, StoredStringListTable);
            AddDouble(RawPreference, data, ref offset, BaseOffset, 12);
            AddInt(RawMinValueRequirement, data, ref offset, BaseOffset, 16);
            AddObject(SkillRequirement as ISerializableJsonObject, data, ref offset, BaseOffset, 20, StoredObjectTable);
            AddEnum(SlotRequirement, data, ref offset, BaseOffset, 24);
            AddEnum(RarityRequirement, data, ref offset, BaseOffset, 26);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 28, StoredStringListTable);
            AddEnum(MinRarityRequirement, data, ref offset, BaseOffset, 32);

            FinishSerializing(data, ref offset, BaseOffset, 34, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
