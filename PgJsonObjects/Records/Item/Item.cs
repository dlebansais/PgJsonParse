﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PgJsonObjects
{
    public class Item : GenericJsonObject<Item>
    {
        #region Direct Properties
        public Dictionary<string, Recipe> BestowRecipeTable { get; } = new Dictionary<string, Recipe>();
        public Ability BestowAbility { get; private set; }
        private string RawBestowAbility;
        private bool IsRawBestowAbilityParsed;
        public Quest BestowQuest { get; private set; }
        private string RawBestowQuest;
        private bool IsRawBestowQuestParsed;
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        public bool? RawAllowPrefix { get; private set; }
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        public bool? RawAllowSuffix { get; private set; }
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get; private set; }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get; private set; }
        public string Description { get; private set; }
        public ItemDroppedAppearance DroppedAppearance { get; private set; }
        public ObservableCollection<ItemEffect> EffectDescriptionList { get; } = new ObservableCollection<ItemEffect>();
        public bool IsEffectDescriptionEmpty { get; private set; }
        public uint? DyeColor { get; private set; }
        public string EquipAppearance { get; private set; }
        public ItemSlot EquipSlot { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        private int? RawIconId;
        public string InternalName { get; private set; }
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        public bool? RawIsTemporary { get; private set; }
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        public bool? RawIsCrafted { get; private set; }
        public Dictionary<ItemKeyword, List<float>> KeywordTable { get; } = new Dictionary<ItemKeyword, List<float>>();
        public List<RecipeItemKey> ItemKeyList { get; } = new List<RecipeItemKey>();
        public List<ItemKeyword> EmptyKeywordList { get; } = new List<ItemKeyword>();
        public List<ItemKeyword> RepeatedKeywordList { get; } = new List<ItemKeyword>();
        public Quest MacGuffinQuestName { get; private set; }
        private string RawMacGuffinQuestName;
        private bool IsRawMacGuffinQuestNameParsed;
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        public int? RawMaxCarryable { get; private set; }
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        public int? RawMaxOnVendor { get; private set; }
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        public int? RawMaxStackSize { get; private set; }
        public string Name { get; private set; }
        public Appearance RequiredAppearance { get; private set; }
        public List<ItemSkillLink> SkillRequirementList { get; } = new List<ItemSkillLink>();
        public List<uint> StockDye { get; private set; }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get; private set; }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get; private set; }
        public bool DestroyWhenUsedUp { get { return RawDestroyWhenUsedUp.HasValue && RawDestroyWhenUsedUp.Value; } }
        public bool? RawDestroyWhenUsedUp { get; private set; }
        public List<ItemBehavior> BehaviorList { get; } = new List<ItemBehavior>();
        public string DynamicCraftingSummary { get; private set; }
        public bool IsSkillReqsDefaults { get { return RawIsSkillReqsDefaults.HasValue && RawIsSkillReqsDefaults.Value; } }
        public bool? RawIsSkillReqsDefaults { get; private set; }
        public int BestowTitle { get { return RawBestowTitle.HasValue ? RawBestowTitle.Value : 0; } }
        public int? RawBestowTitle { get; private set; }
        public int BestowLoreBook { get { return RawBestowLoreBook.HasValue ? RawBestowLoreBook.Value : 0; } }
        public int? RawBestowLoreBook { get; private set; }
        public LoreBook ConnectedLoreBook { get; private set; }
        private bool IsLoreBookParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "BestowRecipes", ParseFieldBestowRecipes },
            { "BestowAbility", ParseFieldBestowAbility },
            { "BestowQuest", ParseFieldBestowQuest },
            { "AllowPrefix", ParseFieldAllowPrefix },
            { "AllowSuffix", ParseFieldAllowSuffix },
            { "CraftPoints", ParseFieldCraftPoints },
            { "CraftingTargetLevel", ParseFieldCraftingTargetLevel },
            { "Description", ParseFieldDescription },
            { "DroppedAppearance", ParseFieldDroppedAppearance },
            { "EffectDescs", ParseFieldEffectDescs },
            { "DyeColor", ParseFieldDyeColor },
            { "EquipAppearance", ParseFieldEquipAppearance },
            { "EquipSlot", ParseFieldEquipSlot },
            { "IconId", ParseFieldIconId },
            { "InternalName", ParseFieldInternalName },
            { "IsTemporary", ParseFieldIsTemporary },
            { "IsCrafted", ParseFieldIsCrafted },
            { "Keywords", ParseFieldKeywords },
            { "MacGuffinQuestName", ParseFieldMacGuffinQuestName },
            { "MaxCarryable", ParseFieldMaxCarryable },
            { "MaxOnVendor", ParseFieldMaxOnVendor },
            { "MaxStackSize", ParseFieldMaxStackSize },
            { "Name", ParseFieldName },
            { "RequiredAppearance", ParseFieldRequiredAppearance },
            { "SkillReqs", ParseFieldSkillReqs },
            { "StockDye", ParseFieldStockDye },
            { "Value", ParseFieldValue },
            { "NumUses", ParseFieldNumUses },
            { "DestroyWhenUsedUp", ParseFieldDestroyWhenUsedUp },
            { "Behaviors", ParseFieldBehaviors},
            { "DynamicCraftingSummary", ParseFieldDynamicCraftingSummary},
            { "IsSkillReqsDefaults", ParseFieldIsSkillReqsDefaults },
            { "BestowTitle", ParseFieldBestowTitle },
            { "BestowLoreBook", ParseFieldBestowLoreBook },
        };

        private static void ParseFieldBestowRecipes(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawBestowRecipes;
            if ((RawBestowRecipes = Value as ArrayList) != null)
                This.ParseBestowRecipes(RawBestowRecipes, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item BestowRecipes");
        }

        private void ParseBestowRecipes(ArrayList RawBestowRecipes, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawBestowRecipes, RawBestowRecipesList, "BestowRecipes", ErrorInfo, out RawBestowRecipesListIsEmpty);
        }

        private static void ParseFieldBestowAbility(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawBestowAbility;
            if ((RawBestowAbility = Value as string) != null)
                This.ParseBestowAbility(RawBestowAbility, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item BestowAbility");
        }

        private void ParseBestowAbility(string RawBestowAbility, ParseErrorInfo ErrorInfo)
        {
            this.RawBestowAbility = RawBestowAbility;
            BestowAbility = null;
            IsRawBestowAbilityParsed = false;
        }

        private static void ParseFieldBestowQuest(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawBestowQuest;
            if ((RawBestowQuest = Value as string) != null)
                This.ParseBestowQuest(RawBestowQuest, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item BestowQuest");
        }

        private void ParseBestowQuest(string RawBestowQuest, ParseErrorInfo ErrorInfo)
        {
            this.RawBestowQuest = RawBestowQuest;
            BestowQuest = null;
            IsRawBestowQuestParsed = false;
        }

        private static void ParseFieldAllowPrefix(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseAllowPrefix((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item AllowPrefix");
        }

        private void ParseAllowPrefix(bool RawAllowPrefix, ParseErrorInfo ErrorInfo)
        {
            this.RawAllowPrefix = RawAllowPrefix;
        }

        private static void ParseFieldAllowSuffix(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseAllowSuffix((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item AllowSuffix");
        }

        private void ParseAllowSuffix(bool RawAllowSuffix, ParseErrorInfo ErrorInfo)
        {
            this.RawAllowSuffix = RawAllowSuffix;
        }

        private static void ParseFieldCraftPoints(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseCraftPoints((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item CraftPoints");
        }

        private void ParseCraftPoints(int RawCraftPoints, ParseErrorInfo ErrorInfo)
        {
            this.RawCraftPoints = RawCraftPoints;
        }

        private static void ParseFieldCraftingTargetLevel(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseCraftingTargetLevel((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item CraftingTargetLevel");
        }

        private void ParseCraftingTargetLevel(int RawCraftingTargetLevel, ParseErrorInfo ErrorInfo)
        {
            this.RawCraftingTargetLevel = RawCraftingTargetLevel;
        }

        private static void ParseFieldDescription(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
        }

        private static void ParseFieldDroppedAppearance(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDroppedAppearance;
            if ((RawDroppedAppearance = Value as string) != null)
                This.ParseDroppedAppearance(RawDroppedAppearance, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item DroppedAppearance");
        }

        private void ParseDroppedAppearance(string RawDroppedAppearance, ParseErrorInfo ErrorInfo)
        {
            int index = RawDroppedAppearance.IndexOf('(');
            if (index > 0)
                RawDroppedAppearance = RawDroppedAppearance.Substring(0, index);

            ItemDroppedAppearance ParsedDroppedAppearance;
            StringToEnumConversion<ItemDroppedAppearance>.TryParse(RawDroppedAppearance, out ParsedDroppedAppearance, ErrorInfo);
            DroppedAppearance = ParsedDroppedAppearance;
        }

        private static void ParseFieldEffectDescs(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawEffectDescs;
            if ((RawEffectDescs = Value as ArrayList) != null)
                This.ParseEffectDescs(RawEffectDescs, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item EffectDescs");
        }

        private void ParseEffectDescs(ArrayList RawEffectDescs, ParseErrorInfo ErrorInfo)
        {
            List<ItemEffect> EffectDescsList = new List<ItemEffect>();

            foreach (object Item in RawEffectDescs)
            {
                string RawEffectDesc;
                if ((RawEffectDesc = Item as string) != null)
                {
                    ItemEffect ItemEffect;
                    if (ItemEffect.TryParse(RawEffectDesc, out ItemEffect))
                        EffectDescriptionList.Add(ItemEffect);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Item EffectDescs");
                        break;
                    }
                }
                else
                {
                    ErrorInfo.AddInvalidObjectFormat("Item EffectDescs");
                    break;
                }
            }

            IsEffectDescriptionEmpty = (EffectDescriptionList.Count == 0);
        }

        private static void ParseFieldDyeColor(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDyeColor;
            if ((RawDyeColor = Value as string) != null)
                This.ParseDyeColor(RawDyeColor, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item DyeColor");
        }

        private void ParseDyeColor(string RawDyeColor, ParseErrorInfo ErrorInfo)
        {
            uint NewColor;
            if (Tools.TryParseColor(RawDyeColor, out NewColor))
                DyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item DyeColor", RawDyeColor);
        }

        private static void ParseFieldEquipAppearance(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawEquipAppearance;
            if ((RawEquipAppearance = Value as string) != null)
                This.ParseEquipAppearance(RawEquipAppearance, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item EquipAppearance");
        }

        private void ParseEquipAppearance(string RawEquipAppearance, ParseErrorInfo ErrorInfo)
        {
            EquipAppearance = RawEquipAppearance;
        }

        private static void ParseFieldEquipSlot(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawEquipSlot;
            if ((RawEquipSlot = Value as string) != null)
                This.ParseEquipSlot(RawEquipSlot, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item EquipSlot");
        }

        private void ParseEquipSlot(string RawEquipSlot, ParseErrorInfo ErrorInfo)
        {
            ItemSlot ParsedSlot;
            StringToEnumConversion<ItemSlot>.TryParse(RawEquipSlot, out ParsedSlot, ErrorInfo);
            EquipSlot = ParsedSlot;
        }

        private static void ParseFieldIconId(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseIconId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item IconId");
        }

        private void ParseIconId(int RawIconId, ParseErrorInfo ErrorInfo)
        {
            if (RawIconId > 0)
            {
                this.RawIconId = RawIconId;
                ErrorInfo.AddIconId(RawIconId);
            }
            else
                this.RawIconId = null;
        }

        private static void ParseFieldInternalName(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInternalName;
            if ((RawInternalName = Value as string) != null)
                This.ParseInternalName(RawInternalName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item InternalName");
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;

            if (InternalName == "PerfectCotton")
                PerfectCottonRatio = 1.0F;
            else
                PerfectCottonRatio = float.NaN;
        }

        private static void ParseFieldIsTemporary(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsTemporary((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item IsTemporary");
        }

        private void ParseIsTemporary(bool RawIsTemporary, ParseErrorInfo ErrorInfo)
        {
            this.RawIsTemporary = RawIsTemporary;
        }

        private static void ParseFieldIsCrafted(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsCrafted((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item IsCrafted");
        }

        private void ParseIsCrafted(bool RawIsCrafted, ParseErrorInfo ErrorInfo)
        {
            this.RawIsCrafted = RawIsCrafted;
        }

        private static void ParseFieldKeywords(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawKeywords;
            if ((RawKeywords = Value as ArrayList) != null)
                This.ParseKeywords(RawKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item Keywords");
        }

        private void ParseKeywords(ArrayList RawKeywords, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawKeyword in RawKeywords)
            {
                string KeywordString;
                if ((KeywordString = RawKeyword as string) != null)
                {
                    string KeyString;
                    string ValueString;
                    float Value;
                    FloatFormat ValueFormat;

                    string[] Pairs = KeywordString.Split('=');
                    if (Pairs.Length == 1)
                    {
                        KeyString = KeywordString.Trim();
                        Value = float.NaN;
                    }

                    else if (Pairs.Length == 2)
                    {
                        KeyString = Pairs[0].Trim();
                        ValueString = Pairs[1].Trim();

                        if (!Tools.TryParseFloat(ValueString, out Value, out ValueFormat))
                        {
                            ErrorInfo.AddInvalidString("Item Keywords", KeywordString);
                            continue;
                        }
                    }
                    else
                    {
                        ErrorInfo.AddInvalidString("Item Keywords", KeywordString);
                        continue;
                    }

                    ItemKeyword Key;
                    if (!StringToEnumConversion<ItemKeyword>.TryParse(KeyString, out Key, ErrorInfo))
                        continue;

                    List<float> ValueList;
                    if (KeywordTable.ContainsKey(Key))
                        ValueList = KeywordTable[Key];
                    else
                    {
                        ValueList = new List<float>();
                        KeywordTable.Add(Key, ValueList);

                        RecipeItemKey ParsedKey;
                        if (StringToEnumConversion<RecipeItemKey>.TryParse(KeyString, out ParsedKey, null))
                            ItemKeyList.Add(ParsedKey);
                    }

                    if (!float.IsNaN(Value))
                        ValueList.Add(Value);
                }
                else
                {
                    ErrorInfo.AddInvalidObjectFormat("Item Keywords");
                    break;
                }
            }
        }

        private static void ParseFieldMacGuffinQuestName(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMacGuffinQuestName;
            if ((RawMacGuffinQuestName = Value as string) != null)
                This.ParseMacGuffinQuestName(RawMacGuffinQuestName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item MacGuffinQuestName");
        }

        private void ParseMacGuffinQuestName(string RawMacGuffinQuestName, ParseErrorInfo ErrorInfo)
        {
            this.RawMacGuffinQuestName = RawMacGuffinQuestName;
            MacGuffinQuestName = null;
            IsRawMacGuffinQuestNameParsed = false;
        }

        private static void ParseFieldMaxCarryable(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMaxCarryable((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item MaxCarryable");
        }

        private void ParseMaxCarryable(int RawMaxCarryable, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxCarryable = RawMaxCarryable;
        }

        private static void ParseFieldMaxOnVendor(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMaxOnVendor((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item MaxOnVendor");
        }

        private void ParseMaxOnVendor(int RawMaxOnVendor, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxOnVendor = RawMaxOnVendor;
        }

        private static void ParseFieldMaxStackSize(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMaxStackSize((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item MaxStackSize");
        }

        private void ParseMaxStackSize(int RawMaxStackSize, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxStackSize = RawMaxStackSize;
        }

        private static void ParseFieldName(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldRequiredAppearance(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRequiredAppearance;
            if ((RawRequiredAppearance = Value as string) != null)
                This.ParseRequiredAppearance(RawRequiredAppearance, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item RequiredAppearance");
        }

        private void ParseRequiredAppearance(string RawRequiredAppearance, ParseErrorInfo ErrorInfo)
        {
            Appearance ParsedAppearance;
            StringToEnumConversion<Appearance>.TryParse(RawRequiredAppearance, out ParsedAppearance, ErrorInfo);
            RequiredAppearance = ParsedAppearance;
        }

        private static void ParseFieldSkillReqs(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawSkillReqs;
            if ((RawSkillReqs = Value as Dictionary<string, object>) != null)
                This.ParseSkillReqs(RawSkillReqs, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item SkillReqs");
        }

        private void ParseSkillReqs(Dictionary<string, object> RawSkillReqs, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, ItemSkillLink> SkillRequirementTable = new Dictionary<string, ItemSkillLink>();

            foreach (KeyValuePair<string, object> SkillEntry in RawSkillReqs)
            {
                if (SkillEntry.Key == "Unknown")
                    continue;

                if (!SkillRequirementTable.ContainsKey(SkillEntry.Key))
                {
                    if (SkillEntry.Value is int)
                    {
                        int SkillValue = (int)SkillEntry.Value;
                        SkillRequirementTable.Add(SkillEntry.Key, new ItemSkillLink(SkillEntry.Key, SkillValue));
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Item SkillReqs");
                }
                else
                    ErrorInfo.AddDuplicateString("Item SkillReqs", SkillEntry.Key);
            }

            foreach (KeyValuePair<string, ItemSkillLink> ItemSkillEntry in SkillRequirementTable)
                SkillRequirementList.Add(ItemSkillEntry.Value);
        }

        private static void ParseFieldStockDye(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawStockDye;
            if ((RawStockDye = Value as string) != null)
                This.ParseStockDye(RawStockDye, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item StockDye");
        }

        private void ParseStockDye(string RawStockDye, ParseErrorInfo ErrorInfo)
        {
            string[] Split = RawStockDye.Split(';');
            if (Split.Length == 1)
            {
                StockDye = new List<uint>();
                return;
            }

            else if (Split.Length != 4)
            {
                ErrorInfo.AddInvalidString("Item StockDye", RawStockDye);
                StockDye = null;
                return;
            }

            if (Split[0].Length != 0)
            {
                ErrorInfo.AddInvalidString("Item StockDye", RawStockDye);
                StockDye = null;
                return;
            }

            uint[] ParsedColors = new uint[3];

            int i;
            for (i = 1; i < Split.Length; i++)
            {
                string ColorPrefix = "Color" + i.ToString() + "=";
                if (!Split[i].StartsWith(ColorPrefix))
                    break;

                string ColorString = Split[i].Substring(ColorPrefix.Length);

                uint ParsedColor;
                if (!Tools.TryParseColor(ColorString, out ParsedColor))
                    break;

                ParsedColors[i - 1] = ParsedColor;
            }

            if (i < Split.Length)
            {
                ErrorInfo.AddInvalidString("Item StockDye", RawStockDye);
                StockDye = null;
                return;
            }

            StockDye = new List<uint>();
            foreach (uint ParsedColor in ParsedColors)
                StockDye.Add(ParsedColor);
        }

        private static void ParseFieldValue(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseValue((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseValue(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item Value");
        }

        private void ParseValue(double RawValue, ParseErrorInfo ErrorInfo)
        {
            this.RawValue = RawValue;
        }

        private static void ParseFieldNumUses(Item This, object NumUses, ParseErrorInfo ErrorInfo)
        {
            if (NumUses is int)
                This.ParseNumUses((int)NumUses, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item NumUses");
        }

        private void ParseNumUses(int RawNumUses, ParseErrorInfo ErrorInfo)
        {
            this.RawNumUses = RawNumUses;
        }

        private static void ParseFieldDestroyWhenUsedUp(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseDestroyWhenUsedUp((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item DestroyWhenUsedUp");
        }

        private void ParseDestroyWhenUsedUp(bool RawDestroyWhenUsedUp, ParseErrorInfo ErrorInfo)
        {
            this.RawDestroyWhenUsedUp = RawDestroyWhenUsedUp;
        }

        private static void ParseFieldBehaviors(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is ArrayList)
                This.ParseBehaviors(Value as ArrayList, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item Behaviors");
        }

        private void ParseBehaviors(ArrayList RawBehaviors, ParseErrorInfo ErrorInfo)
        {
            List<ItemBehavior> ParsedBehaviorList;
            JsonObjectParser<ItemBehavior>.InitAsSublist(RawBehaviors, out ParsedBehaviorList, ErrorInfo);

            foreach (ItemBehavior ParsedBehavior in ParsedBehaviorList)
            {
                ParsedBehavior.SetLinkBack(this);
                BehaviorList.Add(ParsedBehavior);
            }
        }

        private static void ParseFieldDynamicCraftingSummary(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDynamicCraftingSummary;
            if ((RawDynamicCraftingSummary = Value as string) != null)
                This.ParseDynamicCraftingSummary(RawDynamicCraftingSummary, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item DynamicCraftingSummary");
        }

        private void ParseDynamicCraftingSummary(string RawDynamicCraftingSummary, ParseErrorInfo ErrorInfo)
        {
            DynamicCraftingSummary = RawDynamicCraftingSummary;
        }

        private static void ParseFieldIsSkillReqsDefaults(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsSkillReqsDefaults((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item IsSkillReqsDefaults");
        }

        private void ParseIsSkillReqsDefaults(bool RawIsSkillReqsDefaults, ParseErrorInfo ErrorInfo)
        {
            this.RawIsSkillReqsDefaults = RawIsSkillReqsDefaults;
        }

        private static void ParseFieldBestowTitle(Item This, object BestowTitle, ParseErrorInfo ErrorInfo)
        {
            if (BestowTitle is int)
                This.ParseBestowTitle((int)BestowTitle, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item BestowTitle");
        }

        private void ParseBestowTitle(int RawBestowTitle, ParseErrorInfo ErrorInfo)
        {
            this.RawBestowTitle = RawBestowTitle;
        }

        private static void ParseFieldBestowLoreBook(Item This, object BestowLoreBook, ParseErrorInfo ErrorInfo)
        {
            if (BestowLoreBook is int)
                This.ParseBestowLoreBook((int)BestowLoreBook, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item BestowLoreBook");
        }

        private void ParseBestowLoreBook(int RawBestowLoreBook, ParseErrorInfo ErrorInfo)
        {
            this.RawBestowLoreBook = RawBestowLoreBook;
        }

        private List<string> RawBestowRecipesList { get; } = new List<string>();
        private bool RawBestowRecipesListIsEmpty;
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddList("BestowRecipes", RawBestowRecipesList, RawBestowRecipesListIsEmpty);
            Generator.AddString("BestowAbility", RawBestowAbility);
            Generator.AddString("BestowQuest", RawBestowQuest);
            Generator.AddBoolean("AllowPrefix", RawAllowPrefix);
            Generator.AddBoolean("AllowSuffix", RawAllowSuffix);
            Generator.AddInteger("CraftPoints", RawCraftPoints);
            Generator.AddInteger("CraftingTargetLevel", RawCraftingTargetLevel);
            Generator.AddString("Description", Description);
            Generator.AddString("DroppedAppearance", DroppedAppearance.ToString());

            if (EffectDescriptionList.Count > 0)
            {
                Generator.OpenArray("EffectDescs");

                for (int i = 0; i < EffectDescriptionList.Count; i++)
                    Generator.AddString(null, EffectDescriptionList[i].AsEffectString());

                Generator.CloseArray();
            }
            else if (IsEffectDescriptionEmpty)
                Generator.AddEmptyArray("EffectDescs");

            if (DyeColor.HasValue)
            {
                string AsString = Tools.ColorToString(DyeColor.Value);
                Generator.AddString("DyeColor", AsString);
            }

            Generator.AddString("EquipAppearance", EquipAppearance);

            if (EquipSlot != ItemSlot.Internal_None)
                Generator.AddString("EquipSlot", EquipSlot.ToString());

            Generator.AddInteger("IconId", RawIconId);
            Generator.AddString("InternalName", InternalName);
            Generator.AddBoolean("IsTemporary", RawIsTemporary);
            Generator.AddBoolean("IsCrafted", RawIsCrafted);

            if (KeywordTable.Count > 0)
            {
                Generator.OpenArray("Keywords");

                foreach (KeyValuePair<ItemKeyword, List<float>> Entry in KeywordTable)
                    foreach (float f in Entry.Value)
                    GenerateKeywordLine(Generator, Entry.Key, f);

                Generator.CloseArray();
            }

            Generator.AddString("MacGuffinQuestName", RawMacGuffinQuestName);
            Generator.AddInteger("MaxCarryable", RawMaxCarryable);
            Generator.AddInteger("MaxOnVendor", RawMaxOnVendor);
            Generator.AddInteger("MaxStackSize", RawMaxStackSize);
            Generator.AddString("Name", Name);

            if (RequiredAppearance != Appearance.Internal_None)
                Generator.AddString("RequiredAppearance", RequiredAppearance.ToString());

            if (SkillRequirementList.Count > 0)
            {
                Generator.OpenObject("SkillReqs");

                foreach (ItemSkillLink SkillEntry in SkillRequirementList)
                    Generator.AddInteger(SkillEntry.SkillName, SkillEntry.SkillLevel);

                Generator.CloseObject();
            }

            if (StockDye != null)
            {
                if (StockDye.Count == 0)
                    Generator.AddString("StockDye", "");
                else
                {
                    string StockDyeString = "";
                    for (int i = 0; i < StockDye.Count; i++)
                    {
                        uint c = StockDye[i];
                        if (i == 2 && c == 0xFF00FFFF)
                            StockDyeString += ";Color" + (i + 1).ToString() + "=cyan";
                        else
                            StockDyeString += ";Color" + (i + 1).ToString() + "=" + Tools.ColorToString(c).ToLower();
                    }

                    Generator.AddString("StockDye", StockDyeString);
                }
            }

            Generator.AddDouble("Value", RawValue);

            Generator.CloseObject();
        }

        private void GenerateKeywordLine(JsonGenerator Generator, ItemKeyword EntryKey, float EntryValue)
        {
            if (EmptyKeywordList.Contains(EntryKey))
                Generator.AddString(null, EntryKey.ToString());

            if (float.IsNaN(EntryValue))
                Generator.AddString(null, EntryKey.ToString());
            else
                Generator.AddString(null, EntryKey.ToString() + "=" + EntryValue.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);
                AddWithFieldSeparator(ref Result, Description);
                foreach (KeyValuePair<string, Recipe> Entry in BestowRecipeTable)
                    AddWithFieldSeparator(ref Result, Entry.Value.Name);
                if (BestowAbility != null)
                    AddWithFieldSeparator(ref Result, BestowAbility.Name);
                if (BestowQuest != null)
                    AddWithFieldSeparator(ref Result, BestowQuest.Name);
                if (RawAllowPrefix.HasValue)
                    AddWithFieldSeparator(ref Result, "Allow Prefix");
                if (RawAllowSuffix.HasValue)
                    AddWithFieldSeparator(ref Result, "Allow Suffix");
                if (DroppedAppearance != ItemDroppedAppearance.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemDroppedAppearanceTextMap[DroppedAppearance]);

                foreach (ItemEffect ItemEffect in EffectDescriptionList)
                {
                    ItemSimpleEffect AsItemSimpleEffect;
                    ItemAttributeLink AsItemAttributeLink;

                    if ((AsItemSimpleEffect = ItemEffect as ItemSimpleEffect) != null)
                        AddWithFieldSeparator(ref Result, AsItemSimpleEffect.Description);

                    else if ((AsItemAttributeLink = ItemEffect as ItemAttributeLink) != null)
                        AddWithFieldSeparator(ref Result, AsItemAttributeLink.FriendlyName + ":" + AsItemAttributeLink.FriendlyEffect);
                }
                if (EquipSlot != ItemSlot.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemSlotTextMap[EquipSlot]);
                if (RawIsTemporary.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Temporary");
                if (RawIsCrafted.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Crafted");
                foreach (KeyValuePair<ItemKeyword, List<float>> Entry in KeywordTable)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[Entry.Key]);
                if (MacGuffinQuestName != null)
                    AddWithFieldSeparator(ref Result, MacGuffinQuestName.Name);
                if (RequiredAppearance != Appearance.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AppearanceTextMap[RequiredAppearance]);
                foreach (ItemSkillLink Requirement in SkillRequirementList)
                    AddWithFieldSeparator(ref Result, Requirement.Link.Name);
                if (RawDestroyWhenUsedUp.HasValue)
                    AddWithFieldSeparator(ref Result, "Destroy When Used Up");
                foreach (ItemBehavior Behavior in BehaviorList)
                    AddWithFieldSeparator(ref Result, Behavior.TextContent);
                if (RawIsSkillReqsDefaults.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Skill Requirement The Default");

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> QuestTable = AllTables[typeof(Quest)];
            Dictionary<string, IGenericJsonObject> LoreBookTable = AllTables[typeof(LoreBook)];

            IsConnected |= Recipe.ConnectTableByInternalName(ErrorInfo, RecipeTable, RawBestowRecipesList, BestowRecipeTable);

            BestowAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawBestowAbility, BestowAbility, ref IsRawBestowAbilityParsed, ref IsConnected, this);
            BestowQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawBestowQuest, BestowQuest, ref IsRawBestowQuestParsed, ref IsConnected, this);

            foreach (ItemEffect Effect in EffectDescriptionList)
            {
                ItemAttributeLink AsItemAttributeLink;
                if ((AsItemAttributeLink = Effect as ItemAttributeLink) != null)
                {
                    if (!AsItemAttributeLink.IsParsed)
                    {
                        bool IsParsed = false;
                        Attribute Link = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsItemAttributeLink.AttributeName, AsItemAttributeLink.Link, ref IsParsed, ref IsConnected, this);
                        AsItemAttributeLink.SetLink(Link);
                    }
                }
            }

            MacGuffinQuestName = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawMacGuffinQuestName, MacGuffinQuestName, ref IsRawMacGuffinQuestNameParsed, ref IsConnected, this);

            foreach (ItemSkillLink ItemSkill in SkillRequirementList)
                if (!ItemSkill.IsParsed)
                {
                    bool IsParsed = false;
                    Skill Link = Skill.ConnectSingleProperty(ErrorInfo, SkillTable, ItemSkill.SkillName, ItemSkill.Link, ref IsParsed, ref IsConnected, this);
                    ItemSkill.SetLink(Link);

                    if (!KeywordTable.ContainsKey(ItemKeyword.Decoction))
                        PgJsonObjects.Skill.UpdateAnySkillIcon(Link.CombatSkill, RawIconId);
                }

            foreach (ItemBehavior Behavior in BehaviorList)
                IsConnected |= Behavior.Connect(ErrorInfo, this, AllTables);

            if (RawBestowLoreBook.HasValue)
                ConnectedLoreBook = LoreBook.ConnectSingleProperty(ErrorInfo, LoreBookTable, BestowLoreBook, ConnectedLoreBook, ref IsLoreBookParsed, ref IsConnected, this);

            return IsConnected;
        }

        public static Item ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> ItemTable, string RawItemName, Item ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            if (RawItemName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
            {
                Item ItemValue = Entry.Value as Item;
                if (ItemValue.InternalName == RawItemName)
                {
                    IsConnected = true;
                    ItemValue.AddLinkBack(LinkBack);
                    return ItemValue;
                }
            }

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
            {
                Item ItemValue = Entry.Value as Item;
                if (ItemValue.Name == RawItemName)
                {
                    IsConnected = true;
                    ItemValue.AddLinkBack(LinkBack);
                    return ItemValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawItemName);

            return null;
        }

        public static Item ConnectByCode(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> ItemTable, int? RawItemCode, Item ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            if (RawItemCode == null)
                return null;

            string FullKey = "item_" + RawItemCode.Value;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
                if (Entry.Key == FullKey)
                {
                    Item ItemValue = Entry.Value as Item;

                    IsConnected = true;
                    ItemValue.AddLinkBack(LinkBack);
                    return ItemValue;
                }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(FullKey);

            return null;
        }

        public static List<Item> ConnectByItemKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> ItemTable, RecipeItemKey ItemKey, List<Item> ItemList, ref bool IsRawItemParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawItemParsed)
                return ItemList;

            IsRawItemParsed = true;

            if (ItemKey == RecipeItemKey.Internal_None)
                return ItemList;

            ItemList = new List<Item>();
            IsConnected = true;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
            {
                Item ItemValue = Entry.Value as Item;
                if (ItemValue.ItemKeyList.Contains(ItemKey))
                {
                    ItemValue.AddLinkBack(LinkBack);
                    ItemList.Add(ItemValue);
                }
            }

            if (ItemList.Count == 0 && ErrorInfo != null)
                ErrorInfo.AddMissingKey(ItemKey.ToString());

            return ItemList;
        }

        public static List<Item> ConnectByKeyword(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> ItemTable, ItemKeyword Keyword, List<Item> ItemList, ref bool IsRawItemParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawItemParsed)
                return ItemList;

            IsRawItemParsed = true;

            if (Keyword == ItemKeyword.Internal_None)
                return ItemList;

            ItemList = new List<Item>();
            IsConnected = true;

            foreach (KeyValuePair<string, IGenericJsonObject> ItemEntry in ItemTable)
            {
                Item ItemValue = ItemEntry.Value as Item;
                foreach (KeyValuePair<ItemKeyword, List<float>> KeywordEntry in ItemValue.KeywordTable)
                {
                    if (KeywordEntry.Key == Keyword)
                    {
                        ItemValue.AddLinkBack(LinkBack);
                        ItemList.Add(ItemValue);
                    }
                }
            }

            if (ItemList.Count == 0 && ErrorInfo != null)
                ErrorInfo.AddMissingKey(Keyword.ToString());

            return ItemList;
        }

        public static Item ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> ItemTable, int ItemId, Item ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            return ConnectById(ErrorInfo, ItemTable, "item_" + ItemId, ParsedItem, ref IsRawItemParsed, ref IsConnected, LinkBack);
        }

        public static Item ConnectById(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> ItemTable, string RawItemId, Item ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
            {
                Item ItemValue = Entry.Value as Item;
                if (ItemValue.Key == RawItemId)
                {
                    IsConnected = true;
                    ItemValue.AddLinkBack(LinkBack);
                    return ItemValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawItemId);

            return null;
        }
        #endregion

        #region Crunching
        public float GetWeight(WeightProfile WeightProfile)
        {
            if (WeightProfile == null)
                return 0;

            float Weight = 0;

            foreach (AttributeWeight AttributeWeight in WeightProfile.AttributeWeightList)
            {
                foreach (ItemEffect Effect in EffectDescriptionList)
                {
                    ItemAttributeLink AsItemAttributeLink;
                    if ((AsItemAttributeLink = Effect as ItemAttributeLink) != null)
                    {
                        if (AsItemAttributeLink.Link == AttributeWeight.Attribute)
                            Weight += AsItemAttributeLink.AttributeEffect * AttributeWeight.Weight;
                    }
                }
            }

            return Weight;
        }
        #endregion

        #region Recursive Components Sum
        public double PerfectCottonRatio { get; private set; }

        public void SetPerfectCottonRatio(double PerfectCottonRatioFromRecipe)
        {
            if (double.IsNaN(PerfectCottonRatio))
                PerfectCottonRatio = PerfectCottonRatioFromRecipe;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Item"; } }

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
