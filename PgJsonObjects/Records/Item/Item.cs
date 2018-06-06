﻿using PgJsonReader;
using Presentation;
using System;
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
        private List<string> AppearanceDetailList = new List<string>();
        public AppearanceSkin ItemAppearanceSkin { get; private set; }
        public AppearanceSkin ItemAppearanceCork { get; private set; }
        public AppearanceSkin ItemAppearanceFood { get; private set; }
        public AppearanceSkin ItemAppearancePlate { get; private set; }
        public uint? ItemAppearanceColor { get; private set; }
        public ObservableCollection<ItemEffect> EffectDescriptionList { get; } = new ObservableCollection<ItemEffect>();
        public bool IsEffectDescriptionEmpty { get; private set; } = false;
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
        private List<string> RawKeywordList = new List<string>();
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
        private Dictionary<string, ItemSkillLink> SkillRequirementTable = new Dictionary<string, ItemSkillLink>();
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
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "BestowRecipes", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawBestowRecipesList.Add(value),
                SetArrayIsEmpty = () => RawBestowRecipesListIsEmpty = true,
                GetStringArray = () => RawBestowRecipesList,
                GetArrayIsEmpty =() => RawBestowRecipesListIsEmpty } },
            { "BestowAbility", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawBestowAbility = value,
                GetString = () => RawBestowAbility } },
            { "BestowQuest", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawBestowQuest = value,
                GetString = () => RawBestowQuest } },
            { "AllowPrefix", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawAllowPrefix = value,
                GetBool = () => RawAllowPrefix } },
            { "AllowSuffix", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawAllowSuffix = value,
                GetBool = () => RawAllowSuffix } },
            { "CraftPoints", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCraftPoints = value,
                GetInteger = () => RawCraftPoints } },
            { "CraftingTargetLevel", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCraftingTargetLevel = value,
                GetInteger = () => RawCraftingTargetLevel } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Description = value,
                GetString = () => Description } },
            { "DroppedAppearance", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseDroppedAppearance,
                GetString = GetDroppedAppearance } },
            { "EffectDescs", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseEffectDescs,
                GetStringArray = GetEffectDescs } },
            { "DyeColor", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseDyeColor,
                GetString = () => DyeColor.HasValue ? InvariantCulture.ColorToString(DyeColor.Value) : null } },
            { "EquipAppearance", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => EquipAppearance = value,
                GetString = () => EquipAppearance } },
            { "EquipSlot", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => EquipSlot = StringToEnumConversion<ItemSlot>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<ItemSlot>.ToString(EquipSlot, null, ItemSlot.Internal_None) } },
            { "IconId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseIconId,
                GetInteger = () => RawIconId } },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseInternalName,
                GetString = () => InternalName } },
            { "IsTemporary", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsTemporary = value,
                GetBool = () => RawIsTemporary } },
            { "IsCrafted", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsCrafted = value,
                GetBool = () => RawIsCrafted } },
            { "Keywords", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseKeywords,
                GetStringArray = () => RawKeywordList } },
            { "MacGuffinQuestName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawMacGuffinQuestName = value,
                GetString = () => RawMacGuffinQuestName } },
            { "MaxCarryable", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMaxCarryable = value,
                GetInteger = () => RawMaxCarryable } },
            { "MaxOnVendor", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMaxOnVendor = value,
                GetInteger = () => RawMaxOnVendor } },
            { "MaxStackSize", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMaxStackSize = value,
                GetInteger = () => RawMaxStackSize } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Name = value,
                GetString = () => Name } },
            { "RequiredAppearance", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RequiredAppearance = StringToEnumConversion<Appearance>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<Appearance>.ToString(RequiredAppearance, null, Appearance.Internal_None) } },
            { "SkillReqs", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseSkillReqs,
                GetObject = GetSkillReqs } },
            { "StockDye", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseStockDye,
                GetString = GetStockDye } },
            { "Value", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawValue = value,
                GetFloat = () => RawValue } },
            { "NumUses", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumUses = value,
                GetInteger = () => RawNumUses } },
            { "DestroyWhenUsedUp", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawDestroyWhenUsedUp = value,
                GetBool = () => RawDestroyWhenUsedUp } },
            { "Behaviors", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParseBehaviors,
                GetObjectArray = () => BehaviorList } },
            { "DynamicCraftingSummary", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DynamicCraftingSummary = value,
                GetString = () => DynamicCraftingSummary } },
            { "IsSkillReqsDefaults", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsSkillReqsDefaults = value,
                GetBool = () => RawIsSkillReqsDefaults } },
            { "BestowTitle", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawBestowTitle = value,
                GetInteger = () => RawBestowTitle  } },
            { "BestowLoreBook", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawBestowLoreBook = value,
                GetInteger = () => RawBestowLoreBook } },
        }; } }

        private void ParseDroppedAppearance(string value, ParseErrorInfo ErrorInfo)
        {
            string AppearanceString;

            int index = value.IndexOf('(');
            if (index > 0)
            {
                if (!value.EndsWith(")"))
                {
                    ErrorInfo.AddInvalidObjectFormat("Item DroppedAppearance");
                    return;
                }

                AppearanceString = value.Substring(0, index);

                string[] Details = value.Substring(index + 1, value.Length - index - 2).Split(';');
                foreach (string Detail in Details)
                {
                    string[] Splitted = Detail.Split('=');
                    if (Splitted.Length != 2)
                    {
                        ErrorInfo.AddInvalidObjectFormat("Item DroppedAppearance");
                        return;
                    }

                    string DetailKey = Splitted[0].Trim();
                    string DetailValue = Splitted[1].Trim();

                    if (string.IsNullOrEmpty(DetailKey) || string.IsNullOrEmpty(DetailValue))
                    {
                        ErrorInfo.AddInvalidObjectFormat("Item DroppedAppearance");
                        return;
                    }

                    if (DetailKey == "Skin" && DetailValue.StartsWith("^"))
                    {
                        string RawValue = DetailValue.Substring(1);
                        ItemAppearanceSkin = StringToEnumConversion<AppearanceSkin>.Parse(RawValue, TextMaps.AppearanceSkinStringMap, ErrorInfo);
                        AppearanceDetailList.Add("Skin");
                    }
                    else if (DetailKey == "^Skin")
                    {
                        string RawValue = DetailValue;
                        ItemAppearanceSkin = StringToEnumConversion<AppearanceSkin>.Parse(RawValue, TextMaps.AppearanceSkinStringMap, ErrorInfo);
                        AppearanceDetailList.Add("^Skin");
                    }
                    else if (DetailKey == "^Cork")
                    {
                        string RawValue = DetailValue;
                        ItemAppearanceCork = StringToEnumConversion<AppearanceSkin>.Parse(RawValue, TextMaps.AppearanceSkinStringMap, ErrorInfo);
                        AppearanceDetailList.Add("^Cork");
                    }
                    else if (DetailKey == "^Food")
                    {
                        string RawValue = DetailValue;
                        ItemAppearanceFood = StringToEnumConversion<AppearanceSkin>.Parse(RawValue, TextMaps.AppearanceSkinStringMap, ErrorInfo);
                        AppearanceDetailList.Add("^Food");
                    }
                    else if (DetailKey == "^Plate")
                    {
                        string RawValue = DetailValue;
                        ItemAppearancePlate = StringToEnumConversion<AppearanceSkin>.Parse(RawValue, TextMaps.AppearanceSkinStringMap, ErrorInfo);
                        AppearanceDetailList.Add("^Plate");
                    }
                    else if (DetailKey == "Skin_Color")
                    {
                        string RawValue = DetailValue;
                        if (InvariantCulture.TryParseColor(RawValue, out uint ParsedColor))
                        {
                            ItemAppearanceColor = ParsedColor;
                            AppearanceDetailList.Add("Skin_Color");
                        }
                        else
                        {
                            ErrorInfo.AddInvalidObjectFormat("Item DroppedAppearance");
                            return;
                        }
                    }
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Item DroppedAppearance: " + DetailKey);
                        return;
                    }
                }
            }
            else
                AppearanceString = value;

            DroppedAppearance = StringToEnumConversion<ItemDroppedAppearance>.Parse(AppearanceString, ErrorInfo);
        }

        private string GetDroppedAppearance()
        {
            if (DroppedAppearance == ItemDroppedAppearance.Internal_None)
                return null;

            string Line = StringToEnumConversion<ItemDroppedAppearance>.ToString(DroppedAppearance);
            Line += "(" + ")";

            return Line;
        }

        private bool ParseEffectDescs(string RawEffectDesc, ParseErrorInfo ErrorInfo)
        {
            if (PgJsonObjects.ItemEffect.TryParse(RawEffectDesc, out ItemEffect ItemEffect))
            {
                EffectDescriptionList.Add(ItemEffect);
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("Item EffectDescs");
                return false;
            }
        }

        private List<string> GetEffectDescs()
        {
            List<string> Result = new List<string>();

            foreach (ItemEffect Effect in EffectDescriptionList)
                Result.Add(Effect.AsEffectString());

            return Result;
        }

        private void ParseDyeColor(string RawDyeColor, ParseErrorInfo ErrorInfo)
        {
            if (InvariantCulture.TryParseColor(RawDyeColor, out uint NewColor))
                DyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item DyeColor", RawDyeColor);
        }

        private void ParseIconId(int value, ParseErrorInfo ErrorInfo)
        {
            if (value > 0)
            {
                RawIconId = value;
                ErrorInfo.AddIconId(value);
            }
            else
                RawIconId = null;
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;

            if (InternalName == "PerfectCotton")
                PerfectCottonRatio = 1.0F;
            else
                PerfectCottonRatio = float.NaN;
        }

        private bool ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            RawKeywordList.Add(RawKeyword);

            string KeyString;
            string ValueString;
            float Value;
            FloatFormat ValueFormat;

            string[] Pairs = RawKeyword.Split('=');
            if (Pairs.Length == 1)
            {
                KeyString = RawKeyword.Trim();
                Value = float.NaN;
            }

            else if (Pairs.Length == 2)
            {
                KeyString = Pairs[0].Trim();
                ValueString = Pairs[1].Trim();

                if (!Tools.TryParseFloat(ValueString, out Value, out ValueFormat))
                {
                    ErrorInfo.AddInvalidString("Item Keywords", RawKeyword);
                    return false;
                }
            }
            else
            {
                ErrorInfo.AddInvalidString("Item Keywords", RawKeyword);
                return false;
            }

            ItemKeyword Key;
            if (!StringToEnumConversion<ItemKeyword>.TryParse(KeyString, out Key, ErrorInfo))
                return false;

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

            return true;
        }

        private void ParseSkillReqs(JsonObject RawSkillReqs, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> SkillEntry in RawSkillReqs)
                if (!SkillRequirementTable.ContainsKey(SkillEntry.Key))
                {
                    JsonInteger AsJsonInteger;
                    if ((AsJsonInteger = SkillEntry.Value as JsonInteger) != null)
                    {
                        int SkillValue = AsJsonInteger.Number;
                        SkillRequirementTable.Add(SkillEntry.Key, new ItemSkillLink(SkillEntry.Key, SkillValue));
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Item SkillReqs");
                }
                else
                    ErrorInfo.AddDuplicateString("Item SkillReqs", SkillEntry.Key);

            foreach (KeyValuePair<string, ItemSkillLink> ItemSkillEntry in SkillRequirementTable)
            {
                if (ItemSkillEntry.Key == "Unknown")
                    continue;

                SkillRequirementList.Add(ItemSkillEntry.Value);
            }
        }

        private IGenericJsonObject GetSkillReqs()
        {
            SkillRequirement Skillreq = new SkillRequirement();

            foreach (KeyValuePair<string, ItemSkillLink> ItemSkillEntry in SkillRequirementTable)
                Skillreq.SetFieldValue(ItemSkillEntry.Key, ItemSkillEntry.Value);

            return Skillreq;
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
                ErrorInfo.AddInvalidString("Item StockDye (1)", RawStockDye);
                StockDye = null;
                return;
            }

            if (Split[0].Length != 0)
            {
                ErrorInfo.AddInvalidString("Item StockDye (2)", RawStockDye);
                StockDye = null;
                return;
            }

            uint[] ParsedColors = new uint[3];

            int i;
            for (i = 1; i < Split.Length; i++)
            {
                string ColorPrefix = "Color" + i.ToString() + "=";
                if (!Split[i].StartsWith(ColorPrefix))
                {
                    ErrorInfo.AddInvalidString("Item StockDye (3) [i=" + i + "]", RawStockDye);
                    StockDye = null;
                    return;
                }

                string ColorString = Split[i].Substring(ColorPrefix.Length);

                uint ParsedColor;
                if (!InvariantCulture.TryParseColor(ColorString, out ParsedColor))
                {
                    ErrorInfo.AddInvalidString("Item StockDye (4) [i=" + i + "]", RawStockDye);
                    StockDye = null;
                    return;
                }

                ParsedColors[i - 1] = ParsedColor;
            }

            StockDye = new List<uint>();
            foreach (uint ParsedColor in ParsedColors)
                StockDye.Add(ParsedColor);
        }

        private string GetStockDye()
        {
            return "";
        }

        private void ParseBehaviors(JsonObject RawBehaviors, ParseErrorInfo ErrorInfo)
        {
            JsonObjectParser<ItemBehavior>.InitAsSubitem("Behaviors", RawBehaviors, out ItemBehavior ParsedBehavior, ErrorInfo);
            ParsedBehavior.SetLinkBack(this);
            BehaviorList.Add(ParsedBehavior);
        }

        private List<string> RawBestowRecipesList { get; } = new List<string>();
        private bool RawBestowRecipesListIsEmpty;
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddStringList("BestowRecipes", RawBestowRecipesList, RawBestowRecipesListIsEmpty);
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
                string AsString = InvariantCulture.ColorToString(DyeColor.Value);
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
                            StockDyeString += ";Color" + (i + 1).ToString() + "=" + InvariantCulture.ColorToString(c).ToLower();
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
                Generator.AddString(null, EntryKey.ToString() + "=" + InvariantCulture.SingleToString(EntryValue));
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

                EquipmentBoostServerInfoEffect AsEquipmentBoost;

                foreach (ItemBehavior Behavior in BehaviorList)
                    if (Behavior.ServerInfo != null)
                    {
                        foreach (ServerInfoEffect Effect in Behavior.ServerInfo.ServerInfoEffectList)
                            if ((AsEquipmentBoost = Effect as EquipmentBoostServerInfoEffect) != null)
                                if (AsEquipmentBoost.Boost != null)
                                {
                                    ItemAttributeLink AsItemAttributeLink;
                                    ItemSimpleEffect AsItemSimpleEffect;

                                    if ((AsItemAttributeLink = AsEquipmentBoost.Boost as ItemAttributeLink) != null)
                                    {
                                        if (AsItemAttributeLink.Link == AttributeWeight.Attribute)
                                            Weight += AsItemAttributeLink.AttributeEffect * AttributeWeight.Weight;
                                    }
                                    else if ((AsItemSimpleEffect = AsEquipmentBoost.Boost as ItemSimpleEffect) != null)
                                    {
                                        if (AsItemSimpleEffect.Description == AttributeWeight.Attribute.Key)
                                            if (AsEquipmentBoost.AttributeEffect != 0)
                                                Weight += AsEquipmentBoost.AttributeEffect * AttributeWeight.Weight;
                                    }
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
