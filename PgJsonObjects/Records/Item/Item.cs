using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PgJsonObjects
{
    public class Item : GenericJsonObject<Item>
    {
        #region Constants
        private static readonly Dictionary<ItemUseVerb, string> UseVerbMap = new Dictionary<ItemUseVerb, string>()
        {
            { ItemUseVerb.Place, "Place"},
            { ItemUseVerb.Fill, "Fill"},
            { ItemUseVerb.Drink, "Drink"},
            { ItemUseVerb.Learn, "Learn"},
            { ItemUseVerb.LearnWord, "Learn Word"},
            { ItemUseVerb.BlowWhistle, "Blow Whistle"},
            { ItemUseVerb.Read, "Read"},
            { ItemUseVerb.Eat, "Eat"},
            { ItemUseVerb.Delouse, "Delouse"},
            { ItemUseVerb.RubGem, "Rub Gem"},
            { ItemUseVerb.PlayGame, "Play Game"},
            { ItemUseVerb.EmptyPurse, "Empty Purse"},
            { ItemUseVerb.ActivateRune, "Activate Rune"},
            { ItemUseVerb.EmptySack, "Empty Sack"},
            { ItemUseVerb.ReadBook, "Read Book"},
            { ItemUseVerb.Plant, "Plant"},
            { ItemUseVerb.Appreciate, "Appreciate"},
            { ItemUseVerb.Deploy, "Deploy"},
            { ItemUseVerb.Appraise, "Appraise"},
            { ItemUseVerb.StudyPoetry, "Study Poetry"},
            { ItemUseVerb.LaunchFirework, "Launch Firework"},
            { ItemUseVerb.PopConfetti, "Pop Confetti"},
            { ItemUseVerb.BreakDown, "Break Down"},
            { ItemUseVerb.Apply, "Apply"},
            { ItemUseVerb.Swallow, "Swallow"},
            { ItemUseVerb.Inhale, "Inhale"},
            { ItemUseVerb.Inject, "Inject"},
            { ItemUseVerb.Fish, "Fish"},
            { ItemUseVerb.AgeCheese, "Age Cheese"},
            { ItemUseVerb.AgeCream, "Age Cream"},
            { ItemUseVerb.CheckSurvey, "Check Survey"},
            { ItemUseVerb.CheckNotes, "Check Notes"},
            { ItemUseVerb.CheckMap, "Check Map"},
            { ItemUseVerb.Translate, "Translate"},
            { ItemUseVerb.MemorizeWorkOrder, "Memorize Work Order"},
            { ItemUseVerb.Equip, "Equip"},
            { ItemUseVerb.HuddleForWarmth, "Huddle For Warmth"},
        };

        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
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
            { "MetabolismCost", ParseFieldMetabolismCost },
            { "Name", ParseFieldName },
            { "RequiredAppearance", ParseFieldRequiredAppearance },
            { "OtherRequirements", ParseFieldOtherRequirements },
            { "SkillReqs", ParseFieldSkillReqs },
            { "UseDelay", ParseFieldUseDelay },
            { "UseDelayAnimation", ParseFieldUseDelayAnimation },
            { "StockDye", ParseFieldStockDye },
            { "UseRequirements", ParseFieldUseRequirements },
            { "UseVerb", ParseFieldUseVerb },
            { "Value", ParseFieldValue },
        };
        #endregion

        #region Properties
        public Dictionary<string, Recipe> BestowRecipeTable { get; private set; }
        public Ability BestowAbility { get; private set; }
        private string RawBestowAbility;
        public string BestowQuest { get; private set; }
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        private bool? RawAllowPrefix;
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        private bool? RawAllowSuffix;
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        private int? RawCraftPoints;
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        private int? RawCraftingTargetLevel;
        public string Description { get; private set; }
        public string DroppedAppearance { get; private set; }
        public bool IsEffectDescriptionEmpty { get; private set; }
        public ObservableCollection<ItemEffect> EffectDescriptionList { get; private set; }
        public uint? DyeColor;
        public string EquipAppearance { get; private set; }
        public ItemSlot EquipSlot { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        private int? RawIconId;
        public string InternalName { get; private set; }
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        private bool? RawIsTemporary;
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        private bool? RawIsCrafted;
        public Dictionary<ItemKeyword, float> KeywordTable { get; private set; }
        public List<ItemKeyword> EmptyKeywordList { get; private set; }
        public List<ItemKeyword> RepeatedKeywordList { get; private set; }
        public string MacGuffinQuestName { get; private set; }
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        private int? RawMaxCarryable;
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        private int? RawMaxOnVendor;
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        private int? RawMaxStackSize;
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        private int? RawMetabolismCost;
        public string Name { get; private set; }
        public Appearance RequiredAppearance { get; private set; }
        public OtherRequirementType OtherRequirement { get; private set; }
        public float CurHealth { get; private set; }
        public Race RequiredRace { get; private set; }
        public AnimalForm RequiredForm { get; private set; }
        public List<ItemSkillLink> SkillRequirementList { get; private set; }
        public double UseDelay { get { return RawUseDelay.HasValue ? RawUseDelay.Value : 0; } }
        private double? RawUseDelay;
        public string UseDelayAnimation { get; private set; }
        public uint[] StockDye { get; private set; }
        public List<ItemUseRequirement> UseRequirementList { get; private set; }
        public ItemUseVerb UseVerb { get; private set; }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        private double? RawValue;
        #endregion

        #region Client Interface
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
            BestowQuest = RawBestowQuest;
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
            DroppedAppearance = RawDroppedAppearance;
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
            this.RawIconId = RawIconId;
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
                        Value = 0;
                    }

                    else if (Pairs.Length == 2)
                    {
                        KeyString = Pairs[0].Trim();
                        ValueString = Pairs[1].Trim();

                        if (!Tools.TryParseFloat(ValueString, out Value, out ValueFormat) || ValueFormat != FloatFormat.Standard)
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

                    if (KeywordTable.ContainsKey(Key))
                    {
                        if (KeywordTable[Key] == 0.0)
                        {
                            EmptyKeywordList.Add(Key);
                            KeywordTable[Key] = Value;
                        }

                        else if (KeywordTable[Key] == Value)
                            RepeatedKeywordList.Add(Key);

                        else
                            ErrorInfo.AddDuplicateString("Item Keywords", Name);
                    }
                    else
                        KeywordTable.Add(Key, Value);
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
            MacGuffinQuestName = RawMacGuffinQuestName;
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

        private static void ParseFieldMetabolismCost(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMetabolismCost((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item MetabolismCost");
        }

        private void ParseMetabolismCost(int RawMetabolismCost, ParseErrorInfo ErrorInfo)
        {
            this.RawMetabolismCost = RawMetabolismCost;
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

        private static void ParseFieldOtherRequirements(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawOtherRequirements;
            if ((RawOtherRequirements = Value as Dictionary<string, object>) != null)
                This.ParseOtherRequirements(RawOtherRequirements, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item OtherRequirements");
        }

        public void ParseOtherRequirements(Dictionary<string, object> RawOtherRequirements, ParseErrorInfo ErrorInfo)
        {
            if (!RawOtherRequirements.ContainsKey("T"))
            {
                ErrorInfo.AddMissingEnum("Item OtherRequirements", "T");
                return;
            }

            string RequirementType = RawOtherRequirements["T"] as string;

            OtherRequirementType RequirementTypeValue;
            if (!StringToEnumConversion<OtherRequirementType>.TryParse(RequirementType, out RequirementTypeValue, ErrorInfo))
                return;

            switch (RequirementTypeValue)
            {
                case OtherRequirementType.CurHealth:
                    if (RawOtherRequirements.ContainsKey("Health"))
                    {
                        string HealthRequirementString;
                        int? HealthRequirementInt;

                        if ((HealthRequirementString = RawOtherRequirements["Health"] as string) != null)
                        {
                            float NewCurHealth;
                            FloatFormat NewCurHealthFormat;
                            if (Tools.TryParseFloat(HealthRequirementString, out NewCurHealth, out NewCurHealthFormat) && NewCurHealthFormat == FloatFormat.Standard)
                            {
                                OtherRequirement = RequirementTypeValue;
                                CurHealth = NewCurHealth;
                                break;
                            }
                        }
                        else if ((HealthRequirementInt = RawOtherRequirements["Health"] as int?) != null)
                        {
                            OtherRequirement = RequirementTypeValue;
                            CurHealth = HealthRequirementInt.Value;
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Item OtherRequirements Health");
                    return;

                case OtherRequirementType.Race:
                    if (RawOtherRequirements.ContainsKey("AllowedRace"))
                    {
                        string RaceRequirement = RawOtherRequirements["AllowedRace"] as string;

                        Race RaceValue;
                        if (StringToEnumConversion<Race>.TryParse(RaceRequirement, out RaceValue, ErrorInfo))
                        {
                            OtherRequirement = RequirementTypeValue;
                            RequiredRace = RaceValue;
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Item OtherRequirements AllowedRace");
                    return;

                case OtherRequirementType.Appearance:
                    if (RawOtherRequirements.ContainsKey("Appearance"))
                    {
                        if (RequiredAppearance == Appearance.Internal_None)
                        {
                            string AppearanceRequirement = RawOtherRequirements["Appearance"] as string;

                            Appearance AppearanceValue;
                            if (StringToEnumConversion<Appearance>.TryParse(AppearanceRequirement, out AppearanceValue, ErrorInfo))
                            {
                                OtherRequirement = RequirementTypeValue;
                                RequiredAppearance = AppearanceValue;
                                break;
                            }
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Item OtherRequirements Appearance");
                    return;

                case OtherRequirementType.HasEffectKeyword:
                    if (RawOtherRequirements.ContainsKey("Keyword"))
                    {
                        string KeywordRequirement = RawOtherRequirements["Keyword"] as string;

                        AnimalForm FormValue;
                        if (StringToEnumConversion<AnimalForm>.TryParse(KeywordRequirement, out FormValue, ErrorInfo))
                        {
                            OtherRequirement = RequirementTypeValue;
                            RequiredForm = FormValue;
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Item OtherRequirements object");
                    return;

                case OtherRequirementType.IsAdmin:
                case OtherRequirementType.IsLycanthrope:
                case OtherRequirementType.FullMoon:
                case OtherRequirementType.IsHardcore:
                    OtherRequirement = RequirementTypeValue;
                    break;

                case OtherRequirementType.Or:
                    OtherRequirement = RequirementTypeValue;
                    break;

                default:
                    ErrorInfo.AddInvalidObjectFormat("Item OtherRequirements Keyword");
                    OtherRequirement = RequirementTypeValue;
                    break;
            }
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

        private static void ParseFieldUseDelay(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseUseDelay((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseUseDelay(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item UseDelay");
        }

        private void ParseUseDelay(double RawUseDelay, ParseErrorInfo ErrorInfo)
        {
            this.RawUseDelay = RawUseDelay;
        }

        private static void ParseFieldUseDelayAnimation(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUseDelayAnimation;
            if ((RawUseDelayAnimation = Value as string) != null)
                This.ParseUseDelayAnimation(RawUseDelayAnimation, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item UseDelayAnimation");
        }

        private void ParseUseDelayAnimation(string RawUseDelayAnimation, ParseErrorInfo ErrorInfo)
        {
            UseDelayAnimation = RawUseDelayAnimation;
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
                StockDye = new uint[0];
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

            StockDye = ParsedColors;
        }

        private static void ParseFieldUseRequirements(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawUseRequirements;
            if ((RawUseRequirements = Value as ArrayList) != null)
                This.ParseUseRequirements(RawUseRequirements, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item UseRequirements");
        }

        private void ParseUseRequirements(ArrayList RawUseRequirements, ParseErrorInfo ErrorInfo)
        {
            foreach (string RawUseRequirement in RawUseRequirements)
            {
                ItemUseRequirement UseRequirementValue;
                if (StringToEnumConversion<ItemUseRequirement>.TryParse(RawUseRequirement, out UseRequirementValue, ErrorInfo))
                {
                    if (!UseRequirementList.Contains(UseRequirementValue))
                        UseRequirementList.Add(UseRequirementValue);
                    else
                        ErrorInfo.AddDuplicateString("Item UseRequirements", RawUseRequirement);
                }
            }
        }

        private static void ParseFieldUseVerb(Item This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUseVerb;
            if ((RawUseVerb = Value as string) != null)
                This.ParseUseVerb(RawUseVerb, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Item UseVerb");
        }

        private void ParseUseVerb(string RawUseVerb, ParseErrorInfo ErrorInfo)
        {
            ItemUseVerb ParsedUseVerb;
            StringToEnumConversion<ItemUseVerb>.TryParse(RawUseVerb, UseVerbMap, out ParsedUseVerb, ErrorInfo);
            UseVerb = ParsedUseVerb;
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

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddList("BestowRecipes", RawBestowRecipesList, RawBestowRecipesListIsEmpty);
            Generator.AddString("BestowAbility", RawBestowAbility);
            Generator.AddString("BestowQuest", BestowQuest);
            Generator.AddBoolean("AllowPrefix", RawAllowPrefix);
            Generator.AddBoolean("AllowSuffix", RawAllowSuffix);
            Generator.AddInteger("CraftPoints", RawCraftPoints);
            Generator.AddInteger("CraftingTargetLevel", RawCraftingTargetLevel);
            Generator.AddString("Description", Description);
            Generator.AddString("DroppedAppearance", DroppedAppearance);

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

                foreach (KeyValuePair<ItemKeyword, float> Entry in KeywordTable)
                    GenerateKeywordLine(Generator, Entry);

                Generator.CloseArray();
            }

            Generator.AddString("MacGuffinQuestName", MacGuffinQuestName);
            Generator.AddInteger("MaxCarryable", RawMaxCarryable);
            Generator.AddInteger("MaxOnVendor", RawMaxOnVendor);
            Generator.AddInteger("MaxStackSize", RawMaxStackSize);
            Generator.AddFloat("MetabolismCost", RawMetabolismCost);
            Generator.AddString("Name", Name);

            if (RequiredAppearance != Appearance.Internal_None)
                Generator.AddString("RequiredAppearance", RequiredAppearance.ToString());

            switch (OtherRequirement)
            {
                case OtherRequirementType.IsAdmin:
                case OtherRequirementType.IsLycanthrope:
                    Generator.OpenObject("OtherRequirements");
                    Generator.AddString("T", OtherRequirement.ToString());
                    Generator.CloseObject();
                    break;

                case OtherRequirementType.CurHealth:
                    Generator.OpenObject("OtherRequirements");
                    Generator.AddString("T", "CurHealth");
                    Generator.AddFloat("Health", CurHealth);
                    Generator.CloseObject();
                    break;

                case OtherRequirementType.Race:
                    Generator.OpenObject("OtherRequirements");
                    Generator.AddString("T", "Race");
                    Generator.AddString("AllowedRace", RequiredRace.ToString());
                    Generator.CloseObject();
                    break;

                case OtherRequirementType.HasEffectKeyword:
                    Generator.OpenObject("OtherRequirements");
                    Generator.AddString("T", "HasEffectKeyword");
                    Generator.AddString("Keyword", RequiredForm.ToString());
                    Generator.CloseObject();
                    break;
            }

            if (SkillRequirementList.Count > 0)
            {
                Generator.OpenObject("SkillReqs");

                foreach (ItemSkillLink SkillEntry in SkillRequirementList)
                    Generator.AddInteger(SkillEntry.SkillName, SkillEntry.SkillLevel);

                Generator.CloseObject();
            }

            Generator.AddDouble("UseDelay", RawUseDelay);
            Generator.AddString("UseDelayAnimation", UseDelayAnimation);

            if (StockDye != null)
            {
                if (StockDye.Length == 0)
                    Generator.AddString("StockDye", "");
                else
                {
                    string StockDyeString = "";
                    for (int i = 0; i < StockDye.Length; i++)
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

            if (UseRequirementList.Count > 0)
            {
                Generator.OpenArray("UseRequirements");

                foreach (ItemUseRequirement UseRequirement in UseRequirementList)
                    Generator.AddString(null, UseRequirement.ToString());

                Generator.CloseArray();
            }

            if (UseVerb != ItemUseVerb.Internal_None)
                Generator.AddString("UseVerb", StringToEnumConversion<ItemUseVerb>.ToString(UseVerb, UseVerbMap));

            Generator.AddDouble("Value", RawValue);

            Generator.CloseObject();
        }

        private void GenerateKeywordLine(JsonGenerator Generator, KeyValuePair<ItemKeyword, float> Entry)
        {
            if (EmptyKeywordList.Contains(Entry.Key))
                Generator.AddString(null, Entry.Key.ToString());

            if (Entry.Value == 0)
                Generator.AddString(null, Entry.Key.ToString());
            else
                Generator.AddString(null, Entry.Key.ToString() + "=" + Entry.Value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

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

        public static Item ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Item> ItemTable, string RawItemName, Item ParsedItem, ref bool IsRawItemParsed, ref bool IsConnected)
        {
            if (IsRawItemParsed)
                return ParsedItem;

            IsRawItemParsed = true;

            if (RawItemName == null)
                return null;

            foreach (KeyValuePair<string, Item> Entry in ItemTable)
                if (Entry.Value.InternalName == RawItemName)
                {
                    IsConnected = true;
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawItemName);
            return null;
        }

        private List<string> RawBestowRecipesList;
        private bool RawBestowRecipesListIsEmpty;
        private bool IsRawBestowAbilityParsed;
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Item"; } }

        protected override void InitializeFields()
        {
            RawBestowRecipesList = new List<string>();
            BestowRecipeTable = new Dictionary<string, Recipe>();
            EffectDescriptionList = new ObservableCollection<ItemEffect>();
            IsEffectDescriptionEmpty = false;
            KeywordTable = new Dictionary<ItemKeyword, float>();
            EmptyKeywordList = new List<ItemKeyword>();
            RepeatedKeywordList = new List<ItemKeyword>();
            SkillRequirementList = new List<ItemSkillLink>();
            UseRequirementList = new List<ItemUseRequirement>();
            OtherRequirement = OtherRequirementType.Internal_None;
            CurHealth = 0;
            RequiredRace = Race.Internal_None;
            RequiredForm = AnimalForm.Internal_None;
            StockDye = null;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool IsConnected = false;

            IsConnected |= Recipe.ConnectTableByInternamName(ErrorInfo, RecipeTable, RawBestowRecipesList, BestowRecipeTable);

            BestowAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawBestowAbility, BestowAbility, ref IsRawBestowAbilityParsed, ref IsConnected);

            foreach (ItemEffect Effect in EffectDescriptionList)
            {
                ItemAttributeLink AsItemAttributeLink;
                if ((AsItemAttributeLink = Effect as ItemAttributeLink) != null)
                {
                    if (!AsItemAttributeLink.IsParsed)
                    {
                        bool IsParsed = false;
                        Attribute Link = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsItemAttributeLink.AttributeName, AsItemAttributeLink.Link, ref IsParsed, ref IsConnected);
                        AsItemAttributeLink.SetLink(Link);
                    }
                }
            }

            foreach (ItemSkillLink ItemSkill in SkillRequirementList)
                if (!ItemSkill.IsParsed)
                {
                    bool IsParsed = false;
                    Skill Link = Skill.ConnectSingleProperty(ErrorInfo, SkillTable, ItemSkill.SkillName, ItemSkill.Link, ref IsParsed, ref IsConnected);
                    ItemSkill.SetLink(Link);
                }

            return IsConnected;
        }
        #endregion
    }
}
