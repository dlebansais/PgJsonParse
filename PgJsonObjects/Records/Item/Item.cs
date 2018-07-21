using PgJsonReader;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Item : MainJsonObject<Item>, IPgItem
    {
        #region Direct Properties
        public IPgAbility BestowAbility { get; private set; }
        public IPgQuest BestowQuest { get; private set; }
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get; private set; }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get; private set; }
        public string Description { get; private set; }
        public ItemDroppedAppearance DroppedAppearance { get; private set; }
        public ItemSlot EquipSlot { get; private set; }
        public AppearanceSkin ItemAppearanceSkin { get; private set; }
        public AppearanceSkin ItemAppearanceCork { get; private set; }
        public AppearanceSkin ItemAppearanceFood { get; private set; }
        public AppearanceSkin ItemAppearancePlate { get; private set; }
        public uint ItemAppearanceColor { get { return RawItemAppearanceColor.HasValue ? RawItemAppearanceColor.Value : 0; } }
        public uint? RawItemAppearanceColor { get; private set; }
        public IPgItemEffectCollection EffectDescriptionList { get; } = new ItemEffectCollection();
        public uint DyeColor { get { return RawDyeColor.HasValue ? RawDyeColor.Value : 0; } }
        public uint? RawDyeColor { get; private set; }
        public string EquipAppearance { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; private set; }
        public string InternalName { get; private set; }
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        public bool? RawAllowPrefix { get; private set; }
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        public bool? RawAllowSuffix { get; private set; }
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        public bool? RawIsTemporary { get; private set; }
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        public bool? RawIsCrafted { get; private set; }
        public bool DestroyWhenUsedUp { get { return RawDestroyWhenUsedUp.HasValue && RawDestroyWhenUsedUp.Value; } }
        public bool? RawDestroyWhenUsedUp { get; private set; }
        public bool IsSkillReqsDefaults { get { return RawIsSkillReqsDefaults.HasValue && RawIsSkillReqsDefaults.Value; } }
        public bool? RawIsSkillReqsDefaults { get; private set; }
        public bool IsEffectDescriptionEmpty { get { return RawIsEffectDescriptionEmpty.HasValue && RawIsEffectDescriptionEmpty.Value; } }
        public bool? RawIsEffectDescriptionEmpty { get; private set; }
        public List<RecipeItemKey> ItemKeyList { get; } = new List<RecipeItemKey>();
        public List<ItemKeyword> EmptyKeywordList { get; } = new List<ItemKeyword>();
        public List<ItemKeyword> RepeatedKeywordList { get; } = new List<ItemKeyword>();
        public IPgQuest MacGuffinQuestName { get; private set; }
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        public int? RawMaxCarryable { get; private set; }
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        public int? RawMaxOnVendor { get; private set; }
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        public int? RawMaxStackSize { get; private set; }
        public string Name { get; private set; }
        public IPgItemSkillLinkCollection SkillRequirementList { get; } = new ItemSkillLinkCollection();
        public List<uint> StockDye { get; private set; } = new List<uint>();
        public List<string> StockDyeByName { get; private set; } = new List<string>();
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get; private set; }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get; private set; }
        public IPgItemBehaviorCollection BehaviorList { get; } = new ItemBehaviorCollection();
        public string DynamicCraftingSummary { get; private set; }
        public IPgPlayerTitle BestowTitle { get; private set; }
        public IPgLoreBook ConnectedLoreBook { get; private set; }
        public Appearance RequiredAppearance { get; private set; }
        public List<string> KeywordValueList { get; } = new List<string>();
        public IPgRecipeCollection BestowRecipeList { get; private set; } = null;
        public List<string> AppearanceDetailList { get; private set; } = new List<string>();
        public List<string> RawKeywordList { get; private set; } = new List<string>();
        public int UnknownSkillReqIndex { get { return RawUnknownSkillReqIndex.HasValue ? RawUnknownSkillReqIndex.Value : 0; } }
        public int? RawUnknownSkillReqIndex { get; private set; }

        private bool IsLoreBookParsed;
        private string RawBestowAbility;
        private bool IsRawBestowAbilityParsed;
        private string RawBestowQuest;
        private bool IsRawBestowQuestParsed;
        public Dictionary<ItemKeyword, List<float>> KeywordTable { get; } = new Dictionary<ItemKeyword, List<float>>();
        private string RawMacGuffinQuestName;
        private bool IsRawMacGuffinQuestNameParsed;
        private Dictionary<string, ItemSkillLink> SkillRequirementTable = new Dictionary<string, ItemSkillLink>();
        private int? RawBestowLoreBook;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "BestowRecipes", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawBestowRecipesList.Add(value),
                SetArrayIsEmpty = () => RawBestowRecipesListIsEmpty = true,
                GetStringArray = GetBestowRecipesList,
                GetArrayIsEmpty =() => BestowRecipesListIsEmpty } },
            { "BestowAbility", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawBestowAbility = value,
                GetString = () => BestowAbility != null ? BestowAbility.InternalName : null } },
            { "BestowQuest", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawBestowQuest = value,
                GetString = () => BestowQuest != null ? BestowQuest.InternalName : null } },
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
                SetArrayIsEmpty = () => RawIsEffectDescriptionEmpty = true,
                GetStringArray = GetEffectDescs,
                GetArrayIsEmpty = () => IsEffectDescriptionEmpty } },
            { "DyeColor", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseRawDyeColor,
                GetString = () => RawDyeColor.HasValue ? InvariantCulture.ColorToString(RawDyeColor.Value) : null } },
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
                GetString = () => MacGuffinQuestName != null ? MacGuffinQuestName.InternalName : null } },
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
                GetInteger = () => BestowTitle != null ? BestowTitle.Id : null  } },
            { "BestowLoreBook", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawBestowLoreBook = value,
                GetInteger = () => ConnectedLoreBook != null ? ConnectedLoreBook.Id : null } },
        }; } }

        private List<string> GetBestowRecipesList()
        {
            List<string> Result = new List<string>();

            foreach (Recipe Item in BestowRecipeList)
                Result.Add(Item.InternalName);

            return Result;
        }

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
                            RawItemAppearanceColor = ParsedColor;
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

            if (AppearanceDetailList.Count > 0)
            {
                string DetailString = "";

                foreach (string DetailKey in AppearanceDetailList)
                {
                    if (DetailString.Length > 0)
                        DetailString += ";";

                    if (DetailKey == "Skin")
                        DetailString += "Skin=^" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceSkin, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Skin")
                        DetailString += "^Skin=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceSkin, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Cork")
                        DetailString += "^Cork=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceCork, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Food")
                        DetailString += "^Food=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceFood, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Plate")
                        DetailString += "^Plate=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearancePlate, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "Skin_Color")
                        DetailString += "Skin_Color=" + InvariantCulture.ColorToString(RawItemAppearanceColor.Value);
                }

                Line += "(" + DetailString + ")";
            }

            return Line;
        }

        private bool ParseEffectDescs(string RawEffectDesc, ParseErrorInfo ErrorInfo)
        {
            if (PgJsonObjects.ItemEffect.TryParse(RawEffectDesc, out IPgItemEffect ItemEffect))
            {
                EffectDescriptionList.Add(ItemEffect as ItemEffect);
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

            foreach (IPgItemEffect Effect in EffectDescriptionList)
                Result.Add(Effect.AsEffectString());

            return Result;
        }

        private void ParseRawDyeColor(string RawRawDyeColor, ParseErrorInfo ErrorInfo)
        {
            if (InvariantCulture.TryParseColor(RawRawDyeColor, out uint NewColor))
                RawDyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item RawDyeColor", RawRawDyeColor);
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

        private bool ParseKeywords(string value, ParseErrorInfo errorInfo)
        {
            RawKeywordList.Add(value);

            bool Success = ParseRawKeyword(value, errorInfo, KeywordValueList, KeywordTable, out string ParsedKeyString);
            if (Success)
            {
                if (ParsedKeyString != null)
                {
                    if (StringToEnumConversion<RecipeItemKey>.TryParse(ParsedKeyString, out RecipeItemKey ParsedKey, null))
                        ItemKeyList.Add(ParsedKey);
                }
            }

            return Success;
        }

        public static bool ParseRawKeyword(string RawKeyword, ParseErrorInfo ErrorInfo, List<string> KeywordValueList, Dictionary<ItemKeyword, List<float>> KeywordTable, out string ParsedKeyString)
        {
            string KeyString;
            string ValueString;
            float Value;
            FloatFormat ValueFormat;

            ParsedKeyString = null;

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
                ParsedKeyString = KeyString;
            }

            if (!float.IsNaN(Value))
                ValueList.Add(Value);

            int Index = -1;
            for (int i = 0; i < KeywordValueList.Count; i++)
            {
                string[] Splitted = KeywordValueList[i].Split(',');
                if (Splitted.Length > 0 && int.TryParse(Splitted[0], out int KeywordIndex) && KeywordIndex == (int)Key)
                {
                    Index = i;
                    break;
                }
            }

            if (Index < 0)
            {
                Index = KeywordValueList.Count;
                KeywordValueList.Add("");
            }

            string Line = ((int)Key).ToString();
            foreach (float f in ValueList)
                Line += "," + InvariantCulture.SingleToString(f);

            KeywordValueList[Index] = Line;

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
                {
                    RawUnknownSkillReqIndex = (SkillRequirementList as IList<IPgItemSkillLink>).Count;
                    continue;
                }

                SkillRequirementList.Add(ItemSkillEntry.Value);
            }
        }

        private IObjectContentGenerator GetSkillReqs()
        {
            SkillRequirement Skillreq = new SkillRequirement();

            int Index = 0;
            foreach (ItemSkillLink Item in SkillRequirementList)
            {
                if (RawUnknownSkillReqIndex.HasValue && RawUnknownSkillReqIndex.Value == Index)
                    Skillreq.SetFieldValue("Unknown", new ItemSkillLink("Unknown", 0));

                Skillreq.SetFieldValue(Item.SkillName, Item);
                Index++;
            }

            if (RawUnknownSkillReqIndex.HasValue && RawUnknownSkillReqIndex.Value == Index)
                Skillreq.SetFieldValue("Unknown", new ItemSkillLink("Unknown", 0));

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
            string[] ParsedColorName = new string[3];

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
                ParsedColorName[i - 1] = ColorString;
            }

            StockDye = new List<uint>();
            StockDyeByName = new List<string>();
            for (int n = 0; n < ParsedColors.Length; n++)
            {
                StockDye.Add(ParsedColors[n]);
                StockDyeByName.Add(ParsedColorName[n]);
            }
        }

        private string GetStockDye()
        {
            if (StockDye == null)
                return null;

            string Result = "";

            for (int i = 0; i < StockDye.Count; i++)
            {
                string ColorPrefix = "Color" + (i + 1).ToString();
                Result += ";" + ColorPrefix + "=" + StockDyeByName[i];
            }

            return Result;
        }

        private void ParseBehaviors(JsonObject RawBehaviors, ParseErrorInfo ErrorInfo)
        {
            JsonObjectParser<ItemBehavior>.InitAsSubitem("Behaviors", RawBehaviors, out ItemBehavior ParsedBehavior, ErrorInfo);
            ParsedBehavior.SetLinkBack(this);
            BehaviorList.Add(ParsedBehavior);
        }

        private List<string> RawBestowRecipesList { get; } = new List<string>();
        public bool BestowRecipesListIsEmpty { get { return RawBestowRecipesListIsEmpty.HasValue && RawBestowRecipesListIsEmpty.Value; } }
        public bool? RawBestowRecipesListIsEmpty { get; private set; }
        private int? RawBestowTitle;
        private bool IsRawBestowTitleParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);
                AddWithFieldSeparator(ref Result, Description);
                foreach (Recipe Item in BestowRecipeList)
                    AddWithFieldSeparator(ref Result, Item.Name);
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> AttributeTable = AllTables[typeof(Attribute)];
            Dictionary<string, IJsonKey> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IJsonKey> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IJsonKey> QuestTable = AllTables[typeof(Quest)];
            Dictionary<string, IJsonKey> LoreBookTable = AllTables[typeof(LoreBook)];
            Dictionary<string, IJsonKey> PlayerTitleTable = AllTables[typeof(PlayerTitle)];

            if (BestowRecipeList == null)
            {
                BestowRecipeList = new RecipeCollection();

                foreach (string RawRecipe in RawBestowRecipesList)
                {
                    bool Found = false;
                    foreach (KeyValuePair<string, IJsonKey> Entry in RecipeTable)
                    {
                        Recipe RecipeValue = Entry.Value as Recipe;
                        if (RecipeValue.InternalName == RawRecipe)
                        {
                            Found = true;
                            IsConnected = true;
                            if (BestowRecipeList.Contains(RecipeValue))
                                ErrorInfo.AddDuplicateString("Recipe", RawRecipe);
                            else
                                BestowRecipeList.Add(RecipeValue);
                            break;
                        }
                    }

                    if (!Found)
                        ErrorInfo.AddMissingKey(RawRecipe);
                }
            }

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
                        IPgAttribute Link = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsItemAttributeLink.AttributeName, AsItemAttributeLink.Link, ref IsParsed, ref IsConnected);
                        AsItemAttributeLink.SetLink(Link);
                    }
                }
            }

            MacGuffinQuestName = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawMacGuffinQuestName, MacGuffinQuestName, ref IsRawMacGuffinQuestNameParsed, ref IsConnected, this);

            foreach (ItemSkillLink ItemSkill in SkillRequirementList)
                if (!ItemSkill.IsParsed)
                {
                    bool IsParsed = false;
                    IPgSkill Link = Skill.ConnectSingleProperty(ErrorInfo, SkillTable, ItemSkill.SkillName, ItemSkill.Link, ref IsParsed, ref IsConnected, this);
                    ItemSkill.SetLink(Link);

                    if (!KeywordTable.ContainsKey(ItemKeyword.Decoction))
                        PgJsonObjects.Skill.UpdateAnySkillIcon(Link.CombatSkill, RawIconId);
                }

            foreach (ItemBehavior Behavior in BehaviorList)
                IsConnected |= Behavior.Connect(ErrorInfo, this, AllTables);

            ConnectedLoreBook = LoreBook.ConnectSingleProperty(ErrorInfo, LoreBookTable, RawBestowLoreBook, ConnectedLoreBook, ref IsLoreBookParsed, ref IsConnected, this);
            BestowTitle = PlayerTitle.ConnectSingleProperty(ErrorInfo, PlayerTitleTable, RawBestowTitle, BestowTitle, ref IsRawBestowTitleParsed, ref IsConnected, this);

            return IsConnected;
        }

        public static IPgItem ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> ItemTable, string RawItemName, IPgItem ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            if (RawItemName == null)
                return null;

            foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
            {
                Item ItemValue = Entry.Value as Item;
                if (ItemValue.InternalName == RawItemName)
                {
                    IsConnected = true;
                    ItemValue.AddLinkBack(LinkBack);
                    return ItemValue;
                }
            }

            foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
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

        public static IPgItem ConnectByCode(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> ItemTable, int? RawItemCode, IPgItem ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            if (RawItemCode == null)
                return null;

            string FullKey = "item_" + RawItemCode.Value;

            foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
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

        public static List<Item> ConnectByItemKey(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> ItemTable, RecipeItemKey ItemKey, List<Item> ItemList, ref bool IsRawItemParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawItemParsed)
                return ItemList;

            IsRawItemParsed = true;

            if (ItemKey == RecipeItemKey.Internal_None)
                return ItemList;

            ItemList = new List<Item>();
            IsConnected = true;

            foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
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

        public static IPgItemCollection ConnectByKeyword(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> ItemTable, ItemKeyword Keyword, IPgItemCollection ItemList, ref bool IsRawItemParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawItemParsed)
                return ItemList;

            IsRawItemParsed = true;

            if (Keyword == ItemKeyword.Internal_None)
                return ItemList;

            ItemList = new ItemCollection();
            IsConnected = true;

            foreach (KeyValuePair<string, IJsonKey> ItemEntry in ItemTable)
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

            if ((ItemList as IList<IPgItem>).Count == 0 && ErrorInfo != null)
                ErrorInfo.AddMissingKey(Keyword.ToString());

            return ItemList;
        }

        public static IPgItem ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> ItemTable, int ItemId, IPgItem ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            return ConnectById(ErrorInfo, ItemTable, "item_" + ItemId, ParsedItem, ref IsRawItemParsed, ref IsConnected, LinkBack);
        }

        public static IPgItem ConnectById(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> ItemTable, string RawItemId, IPgItem ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
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
        public static float GetWeight(WeightProfile WeightProfile, IPgItem item)
        {
            if (WeightProfile == null)
                return 0;

            float Weight = 0;

            foreach (AttributeWeight AttributeWeight in WeightProfile.AttributeWeightList)
            {
                foreach (IPgItemEffect Effect in item.EffectDescriptionList)
                {
                    IPgItemAttributeLink AsItemAttributeLink;
                    if ((AsItemAttributeLink = Effect as IPgItemAttributeLink) != null)
                    {
                        if (AsItemAttributeLink.Link == AttributeWeight.Attribute)
                            Weight += AsItemAttributeLink.AttributeEffect * AttributeWeight.Weight;
                    }
                }

                IPgEquipmentBoostServerInfoEffect AsEquipmentBoost;

                foreach (IPgItemBehavior Behavior in item.BehaviorList)
                    if (Behavior.ServerInfo != null)
                    {
                        foreach (IPgServerInfoEffect Effect in Behavior.ServerInfo.ServerInfoEffectList)
                            if ((AsEquipmentBoost = Effect as IPgEquipmentBoostServerInfoEffect) != null)
                                if (AsEquipmentBoost.Boost != null)
                                {
                                    IPgItemAttributeLink AsItemAttributeLink;
                                    IPgItemSimpleEffect AsItemSimpleEffect;

                                    if ((AsItemAttributeLink = AsEquipmentBoost.Boost as IPgItemAttributeLink) != null)
                                    {
                                        if (AsItemAttributeLink.Link == AttributeWeight.Attribute)
                                            Weight += AsItemAttributeLink.AttributeEffect * AttributeWeight.Weight;
                                    }
                                    else if ((AsItemSimpleEffect = AsEquipmentBoost.Boost as IPgItemSimpleEffect) != null)
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<uint>> StoredUIntListTable = new Dictionary<int, List<uint>>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(BestowAbility as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObject(BestowQuest as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddInt(RawCraftPoints, data, ref offset, BaseOffset, 12);
            AddInt(RawCraftingTargetLevel, data, ref offset, BaseOffset, 16);
            AddString(Description, data, ref offset, BaseOffset, 20, StoredStringtable);
            AddEnum(DroppedAppearance, data, ref offset, BaseOffset, 24);
            AddEnum(EquipSlot, data, ref offset, BaseOffset, 26);
            AddEnum(ItemAppearanceSkin, data, ref offset, BaseOffset, 28);
            AddEnum(ItemAppearanceCork, data, ref offset, BaseOffset, 30);
            AddEnum(ItemAppearanceFood, data, ref offset, BaseOffset, 32);
            AddEnum(ItemAppearancePlate, data, ref offset, BaseOffset, 34);
            AddUInt(RawItemAppearanceColor, data, ref offset, BaseOffset, 36);
            AddObjectList(EffectDescriptionList, data, ref offset, BaseOffset, 40, StoredObjectListTable);
            AddUInt(RawDyeColor, data, ref offset, BaseOffset, 44);
            AddString(EquipAppearance, data, ref offset, BaseOffset, 48, StoredStringtable);
            AddInt(RawIconId, data, ref offset, BaseOffset, 52);
            AddString(InternalName, data, ref offset, BaseOffset, 56, StoredStringtable);
            AddBool(RawAllowPrefix, data, ref offset, ref BitOffset, BaseOffset, 60, 0);
            AddBool(RawAllowSuffix, data, ref offset, ref BitOffset, BaseOffset, 60, 2);
            AddBool(RawIsTemporary, data, ref offset, ref BitOffset, BaseOffset, 60, 4);
            AddBool(RawIsCrafted, data, ref offset, ref BitOffset, BaseOffset, 60, 6);
            AddBool(RawDestroyWhenUsedUp, data, ref offset, ref BitOffset, BaseOffset, 60, 8);
            AddBool(RawIsSkillReqsDefaults, data, ref offset, ref BitOffset, BaseOffset, 60, 10);
            AddBool(RawBestowRecipesListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 60, 12);
            AddBool(RawIsEffectDescriptionEmpty, data, ref offset, ref BitOffset, BaseOffset, 60, 14);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(RequiredAppearance, data, ref offset, BaseOffset, 62);
            AddEnumList(ItemKeyList, data, ref offset, BaseOffset, 64, StoredEnumListTable);
            AddEnumList(EmptyKeywordList, data, ref offset, BaseOffset, 68, StoredEnumListTable);
            AddEnumList(RepeatedKeywordList, data, ref offset, BaseOffset, 72, StoredEnumListTable);
            AddObject(MacGuffinQuestName as ISerializableJsonObject, data, ref offset, BaseOffset, 76, StoredObjectTable);
            AddInt(RawMaxCarryable, data, ref offset, BaseOffset, 80);
            AddInt(RawMaxOnVendor, data, ref offset, BaseOffset, 84);
            AddInt(RawMaxStackSize, data, ref offset, BaseOffset, 88);
            AddString(Name, data, ref offset, BaseOffset, 92, StoredStringtable);
            AddObjectList(SkillRequirementList, data, ref offset, BaseOffset, 96, StoredObjectListTable);
            AddUIntList(StockDye, data, ref offset, BaseOffset, 100, StoredUIntListTable);
            AddStringList(StockDyeByName, data, ref offset, BaseOffset, 104, StoredStringListTable);
            AddDouble(RawValue, data, ref offset, BaseOffset, 108);
            AddInt(RawNumUses, data, ref offset, BaseOffset, 112);
            AddObjectList(BehaviorList, data, ref offset, BaseOffset, 116, StoredObjectListTable);
            AddString(DynamicCraftingSummary, data, ref offset, BaseOffset, 120, StoredStringtable);
            AddObject(BestowTitle as ISerializableJsonObject, data, ref offset, BaseOffset, 124, StoredObjectTable);
            AddInt(RawUnknownSkillReqIndex, data, ref offset, BaseOffset, 128);
            AddObject(ConnectedLoreBook as ISerializableJsonObject, data, ref offset, BaseOffset, 132, StoredObjectTable);
            AddStringList(KeywordValueList, data, ref offset, BaseOffset, 136, StoredStringListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 140, StoredStringListTable);
            AddObjectList(BestowRecipeList, data, ref offset, BaseOffset, 144, StoredObjectListTable);
            AddStringList(AppearanceDetailList, data, ref offset, BaseOffset, 148, StoredStringListTable);
            AddStringList(RawKeywordList, data, ref offset, BaseOffset, 152, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 156, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, StoredUIntListTable, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
