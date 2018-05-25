﻿using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        protected override string SortingName { get { return Key; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Keywords", new FieldParser() { Type = FieldType.StringArray, ParseStringArray = ParseKeywords } },
            { "Pref", new FieldParser() { Type = FieldType.Float, ParseFloat = (float value, ParseErrorInfo errorInfo) => { RawPreference = value; }} },
        }; } }

        private bool ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
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

        private bool ParseKeywordAsMinValue(string MinValueString, ParseErrorInfo ErrorInfo)
        {
            int MinValueRequirement;
            if (int.TryParse(MinValueString, out MinValueRequirement))
            {
                this.MinValueRequirement = MinValueRequirement;
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            if (ItemKeywordList.Count == 0)
                ItemKeywordList.Add(ItemKeyword.Any);

            SkillRequirement = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkillRequirement, SkillRequirement, ref IsSkillParsed, ref IsConnected, Parent as GameNpc);

            List<Item> ItemList = new List<Item>();
            foreach (ItemKeyword Keyword in ItemKeywordList)
            {
                if (Keyword == ItemKeyword.Internal_None)
                    continue;

                ItemList.Clear();

                if (Keyword == ItemKeyword.Any)
                {
                    if (MinValueRequirement.HasValue)
                    {
                        foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
                        {
                            Item ItemValue = Entry.Value as Item;
                            if (ItemValue.Value >= MinValueRequirement.Value)
                                ItemList.Add(ItemValue);
                        }
                    }
                    else if (SlotRequirement != ItemSlot.Internal_None)
                    {
                        foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
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
                    if (MinValueRequirement.HasValue && Item.Value < MinValueRequirement.Value)
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
    }
}
