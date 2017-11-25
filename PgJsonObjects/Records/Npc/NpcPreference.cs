using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class NpcPreference : GenericJsonObject<NpcPreference>
    {
        #region Direct Properties
        public List<ItemKeyword> ItemKeywordList { get; private set; } = new List<ItemKeyword>();
        public double Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public double? RawPreference { get; private set; }
        public int? MinValueRequirement { get; private set; }
        public Skill SkillRequirement { get; private set; }
        public PowerSkill RawSkillRequirement { get; private set; }
        private bool IsSkillParsed;
        public ItemSlot SlotRequirement { get; private set; }
        public RecipeItemKey RarityRequirement { get; private set; }
        public RecipeItemKey MinRarityRequirement { get; private set; }
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
        protected override string SortingName { get { return Key; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Keywords", ParseFieldKeywords },
            { "Pref", ParseFieldPref },
        };

        private static void ParseFieldKeywords(NpcPreference This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawKeywords;
            if ((RawKeywords = Value as ArrayList) != null)
                This.ParseKeywords(RawKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Keywords");
        }

        private void ParseKeywords(ArrayList RawKeywords, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawKeyword in RawKeywords)
            {
                string AsString;
                if ((AsString = RawKeyword as string) != null)
                {
                    if (AsString.StartsWith("MinValue:"))
                        ParseKeywordAsMinValue(AsString.Substring(9), ErrorInfo);
                    else if (AsString.StartsWith("SkillPrereq:"))
                        ParseKeywordAsSkillRequirement(AsString.Substring(12), ErrorInfo);
                    else if (AsString.StartsWith("EquipmentSlot:"))
                        ParseKeywordAsEquipmentSlot(AsString.Substring(14), ErrorInfo);
                    else if (AsString.StartsWith("MinRarity:"))
                        ParseKeywordAsMinRarity(AsString.Substring(10), ErrorInfo);
                    else if (AsString.StartsWith("Rarity:"))
                        ParseKeywordAsRarity(AsString.Substring(7), ErrorInfo);
                    else
                    {
                        ItemKeyword ParsedItemKeyword;
                        if (StringToEnumConversion<ItemKeyword>.TryParse(AsString, out ParsedItemKeyword, ErrorInfo))
                            ItemKeywordList.Add(ParsedItemKeyword);
                    }
                }
            }
        }

        private static void ParseFieldPref(NpcPreference This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParsePref((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParsePref(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref");
        }

        private void ParsePref(double RawPref, ParseErrorInfo ErrorInfo)
        {
            RawPreference = RawPref;
        }

        private void ParseKeywordAsMinValue(string MinValueString, ParseErrorInfo ErrorInfo)
        {
            int MinValueRequirement;
            if (int.TryParse(MinValueString, out MinValueRequirement))
                this.MinValueRequirement = MinValueRequirement;
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(MinValue)");
        }

        private void ParseKeywordAsSkillRequirement(string SkillRequirementString, ParseErrorInfo ErrorInfo)
        {
            if (RawSkillRequirement == PowerSkill.Internal_None)
            {
                PowerSkill ParsedSkill;
                StringToEnumConversion<PowerSkill>.TryParse(SkillRequirementString, out ParsedSkill, ErrorInfo);
                RawSkillRequirement = ParsedSkill;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(SkillPrereq)");
        }

        private void ParseKeywordAsEquipmentSlot(string EquipmentSlotString, ParseErrorInfo ErrorInfo)
        {
            if (SlotRequirement == ItemSlot.Internal_None)
            {
                ItemSlot ParsedSlot;
                StringToEnumConversion<ItemSlot>.TryParse(EquipmentSlotString, out ParsedSlot, ErrorInfo);
                SlotRequirement = ParsedSlot;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(EquipmentSlot)");
        }

        private void ParseKeywordAsMinRarity(string MinRarityString, ParseErrorInfo ErrorInfo)
        {
            if (RarityRequirement == RecipeItemKey.Internal_None)
            {
                if (MinRarityString == "Uncommon")
                    MinRarityRequirement = RecipeItemKey.MinRarity_Uncommon;
                else if (MinRarityString == "Rare")
                    MinRarityRequirement = RecipeItemKey.MinRarity_Rare;
                else
                    ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(MinRarity)");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(MinRarity)");
        }

        private void ParseKeywordAsRarity(string RarityString, ParseErrorInfo ErrorInfo)
        {
            if (RarityRequirement == RecipeItemKey.Internal_None)
            {
                if (RarityString == "Common")
                    RarityRequirement = RecipeItemKey.Rarity_Common;
                else
                    ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(Rarity)");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("NpcPreference Pref(Rarity)");
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, PreferenceType);

                foreach (ItemKeyword Item in ItemKeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[Item]);

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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = false;

            if (ItemKeywordList.Count == 0)
                ItemKeywordList.Add(ItemKeyword.Any);

            SkillRequirement = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkillRequirement, SkillRequirement, ref IsSkillParsed, ref IsConnected, Parent as GameNpc);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "NpcPreferences"; } }
        #endregion
    }
}
