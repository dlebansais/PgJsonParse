namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

internal class Item
{
    public Item(RawItem rawItem)
    {
        AllowPrefix = rawItem.AllowPrefix;
        AllowSuffix = rawItem.AllowSuffix;
        AttuneOnPickup = rawItem.AttuneOnPickup;
        Behaviors = rawItem.Behaviors;
        BestowAbility = rawItem.BestowAbility;
        BestowLoreBook = rawItem.BestowLoreBook;
        BestowQuest = rawItem.BestowQuest;
        BestowRecipes = rawItem.BestowRecipes;
        BestowTitle = rawItem.BestowTitle;
        CraftPoints = rawItem.CraftPoints;
        CraftingTargetLevel = rawItem.CraftingTargetLevel;
        Description = rawItem.Description;
        DestroyWhenUsedUp = rawItem.DestroyWhenUsedUp;
        DroppedAppearance = ParseDroppedAppearance(rawItem.DroppedAppearance);
        DyeColor = rawItem.DyeColor;
        DynamicCraftingSummary = rawItem.DynamicCraftingSummary;
        EffectDescriptions = ParseEffectDescriptions(rawItem.EffectDescs);
        EquipAppearance = rawItem.EquipAppearance;
        EquipSlot = rawItem.EquipSlot;
        FoodDescription = rawItem.FoodDesc;
        IconId = rawItem.IconId;
        InternalName = rawItem.InternalName;
        IsCrafted = rawItem.IsCrafted;
        IsSkillRequirementsDefaults = rawItem.IsSkillReqsDefaults;
        IsTemporary = rawItem.IsTemporary;
        Keywords = ParseKeywords(rawItem.Keywords);
        Lint_VendorNpc = rawItem.Lint_VendorNpc;
        MacGuffinQuestName = rawItem.MacGuffinQuestName;
        MaxCarryable = rawItem.MaxCarryable;
        MaxOnVendor = rawItem.MaxOnVendor;
        MaxStackSize = rawItem.MaxStackSize;
        MountedAppearance = rawItem.MountedAppearance;
        Name = rawItem.Name;
        NumberOfUses = rawItem.NumUses;
        RequiredAppearance = rawItem.RequiredAppearance;
        SkillRequirements = rawItem.SkillReqs;
        StockDye = ParseStockDye(rawItem.StockDye, out HasStockDye);
        TSysProfile = rawItem.TSysProfile;
        Value = rawItem.Value;

        RawKeywords = rawItem.Keywords;
    }

    private static DroppedAppearance? ParseDroppedAppearance(string? content)
    {
        if (content is null)
            return null;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(content, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
            return new DroppedAppearance { Appearance = content };

        string Appearance = content.Substring(0, ParameterMatch.Index);
        string InsideParameterString = content.Substring(ParameterMatch.Index + 1, content.Length - ParameterMatch.Index - 2);

        DroppedAppearance Result = new DroppedAppearance { Appearance = Appearance };
        string[] Fields = InsideParameterString.Split(';');
        foreach (string Field in Fields)
        {
            string[] KeyValue = Field.Split('=');
            if (KeyValue.Length != 2)
                throw new InvalidCastException();

            string Key = KeyValue[0].Trim();
            string Value = KeyValue[1].Trim();

            switch (Key)
            {
                case "^Skin":
                    Result.Skin = Value;
                    break;
                case "Skin":
                    if (Value.Length > 1 && Value[0] == '^')
                    {
                        Result.Skin = Value.Substring(1);
                        Result.SetIsSkinInverted(true);
                    }
                    else
                        throw new InvalidCastException();
                    break;
                case "^Cork":
                    Result.Cork = Value;
                    break;
                case "^Food":
                    Result.Food = Value;
                    break;
                case "^Plate":
                    Result.Plate = Value;
                    break;
                case "Color":
                    Result.Color = RgbColor.Parse(Value, string.Empty, out ColorFormat ColorFormat);
                    Result.SetColorFormat(ColorFormat);
                    break;
                case "Skin_Color":
                    Result.SkinColor = RgbColor.Parse(Value, string.Empty, out ColorFormat SkinColorFormat);
                    Result.SetSkinColorFormat(SkinColorFormat);
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

        return Result;
    }

    private static ItemEffect[]? ParseEffectDescriptions(string[]? content)
    {
        if (content is null)
            return null;

        ItemEffect[] Result = new ItemEffect[content.Length];

        for (int i = 0; i < content.Length; i++)
            Result[i] = ParseEffectDescription(content[i]);

        return Result;
    }

    private static ItemEffect ParseEffectDescription(string content)
    {
        if (content.StartsWith("{") && content.EndsWith("}"))
        {
            string EffectString = content.Substring(1, content.Length - 2);
            return ParseAttributeEffectDescription(EffectString);
        }
        else if (content.Contains("{") || content.Contains("}"))
            throw new InvalidCastException();
        else
            return new ItemEffect { Description = content };
    }

    private static ItemEffect ParseAttributeEffectDescription(string effectString)
    {
        string[] Split = effectString.Split('{');
        if (Split.Length != 2)
            throw new InvalidCastException();

        string AttributeName = Split[0];
        string AttributeEffectString = Split[1];

        if (!AttributeName.EndsWith("}"))
            throw new InvalidCastException();

        AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
        if (AttributeName.Contains("{") || AttributeName.Contains("}"))
            throw new InvalidCastException();

        if (AttributeName.Length == 0 || AttributeEffectString.Length == 0)
            throw new InvalidCastException();

        decimal AttributeEffect = decimal.Parse(AttributeEffectString, CultureInfo.InvariantCulture);

        return new ItemEffect() { AttributeName = AttributeName, AttributeEffect = AttributeEffect };
    }

    private static KeywordValues[]? ParseKeywords(string[]? content)
    {
        if (content is null)
            return null;

        Dictionary<string, List<decimal>> KeywordTable = new();

        for (int i = 0; i < content.Length; i++)
            ParseKeyword(content[i], KeywordTable);

        List<KeywordValues> Result = new();
        foreach (KeyValuePair<string, List<decimal>> Entry in KeywordTable)
        {
            KeywordValues NewKeywordValues = new() { Keyword = Entry.Key };
            if (Entry.Value.Count > 0)
                NewKeywordValues.Values = Entry.Value.ToArray();

            Result.Add(NewKeywordValues);
        }

        return Result.ToArray();
    }

    private static void ParseKeyword(string content, Dictionary<string, List<decimal>> keywordTable)
    {
        string KeyString;
        string ValueString;
        decimal? KeywordValue;

        string[] Pairs = content.Split('=');
        if (Pairs.Length == 1)
        {
            KeyString = content.Trim();
            KeywordValue = null;
        }
        else if (Pairs.Length == 2)
        {
            KeyString = Pairs[0].Trim();
            ValueString = Pairs[1].Trim();

            KeywordValue = decimal.Parse(ValueString, CultureInfo.InvariantCulture);
        }
        else
            throw new InvalidCastException();

        List<decimal> ValueList;
        if (keywordTable.ContainsKey(KeyString))
            ValueList = keywordTable[KeyString];
        else
        {
            ValueList = new List<decimal>();
            keywordTable.Add(KeyString, ValueList);
        }

        if (KeywordValue is not null)
            ValueList.Add(KeywordValue.Value);
    }

    private static StockDye? ParseStockDye(string? content, out bool hasStockDye)
    {
        if (content is null)
        {
            hasStockDye = false;
            return null;
        }

        string[] StockDyeSplit = content.Split(';');
        if (StockDyeSplit.Length <= 1)
        {
            hasStockDye = true;
            return null;
        }

        if (StockDyeSplit.Length < 4 || StockDyeSplit.Length > 5)
            throw new InvalidCastException();

        StockDye Result = new();
        hasStockDye = true;

        for (int i = 1; i < 4; i++)
        {
            string ColorHeader = $"Color{i}=";
            string StockDyeString = StockDyeSplit[i];

            if (!StockDyeSplit[i].StartsWith(ColorHeader))
                throw new InvalidCastException();

            string Color = RgbColor.Parse(StockDyeString.Substring(ColorHeader.Length), string.Empty, out ColorFormat ColorFormat);
            Result.SetColorFormat(i - 1, ColorFormat);

            switch (i)
            {
                case 1:
                    Result.Color1 = Color;
                    break;
                case 2:
                    Result.Color2 = Color;
                    break;
                case 3:
                    Result.Color3 = Color;
                    break;
            }
        }

        if (StockDyeSplit.Length == 5)
        {
            if (StockDyeSplit[4] == "GlowEnabled=y")
                Result.IsGlowEnabled = true;
            else
                throw new InvalidCastException();
        }

        return Result;
    }

    public bool? AllowPrefix { get; set; }
    public bool? AllowSuffix { get; set; }
    public bool? AttuneOnPickup { get; set; }
    public Behavior[]? Behaviors { get; set; }
    public string? BestowAbility { get; set; }
    public int? BestowLoreBook { get; set; }
    public string? BestowQuest { get; set; }
    public string[]? BestowRecipes { get; set; }
    public int? BestowTitle { get; set; }
    public int? CraftPoints { get; set; }
    public int? CraftingTargetLevel { get; set; }
    public string? Description { get; set; }
    public bool? DestroyWhenUsedUp { get; set; }
    public DroppedAppearance? DroppedAppearance { get; set; }
    public string? DyeColor { get; set; }
    public string? DynamicCraftingSummary { get; set; }
    public ItemEffect[]? EffectDescriptions { get; set; }
    public string? EquipAppearance { get; set; }
    public string? EquipSlot { get; set; }
    public string? FoodDescription { get; set; }
    public int IconId { get; set; }
    public string? InternalName { get; set; }
    public bool? IsCrafted { get; set; }
    public bool? IsSkillRequirementsDefaults { get; set; }
    public bool? IsTemporary { get; set; }
    public KeywordValues[]? Keywords { get; set; }
    public string? Lint_VendorNpc { get; set; }
    public string? MacGuffinQuestName { get; set; }
    public int? MaxCarryable { get; set; }
    public int? MaxOnVendor { get; set; }
    public int? MaxStackSize { get; set; }
    public string? MountedAppearance { get; set; }
    public string? Name { get; set; }
    public int? NumberOfUses { get; set; }
    public string? RequiredAppearance { get; set; }
    public SkillRequirementDictionary? SkillRequirements { get; set; }
    public StockDye? StockDye { get; set; }
    public string? TSysProfile { get; set; }
    public decimal? Value { get; set; }

    public RawItem ToRawItem()
    {
        RawItem Result = new();

        Result.AllowPrefix = AllowPrefix;
        Result.AllowSuffix = AllowSuffix;
        Result.AttuneOnPickup = AttuneOnPickup;
        Result.Behaviors = Behaviors;
        Result.BestowAbility = BestowAbility;
        Result.BestowLoreBook = BestowLoreBook;
        Result.BestowQuest = BestowQuest;
        Result.BestowRecipes = BestowRecipes;
        Result.BestowTitle = BestowTitle;
        Result.CraftPoints = CraftPoints;
        Result.CraftingTargetLevel = CraftingTargetLevel;
        Result.Description = Description;
        Result.DestroyWhenUsedUp = DestroyWhenUsedUp;
        Result.DroppedAppearance = DroppedAppearanceToStrings(DroppedAppearance);
        Result.DyeColor = DyeColor;
        Result.DynamicCraftingSummary = DynamicCraftingSummary;
        Result.EffectDescs = EffectDescriptionsToString(EffectDescriptions);
        Result.EquipAppearance = EquipAppearance;
        Result.EquipSlot = EquipSlot;
        Result.FoodDesc = FoodDescription;
        Result.IconId = IconId;
        Result.InternalName = InternalName;
        Result.IsCrafted = IsCrafted;
        Result.IsSkillReqsDefaults = IsSkillRequirementsDefaults;
        Result.IsTemporary = IsTemporary;
        Result.Keywords = KeywordsToString(Keywords, RawKeywords);
        Result.Lint_VendorNpc = Lint_VendorNpc;
        Result.MacGuffinQuestName = MacGuffinQuestName;
        Result.MaxCarryable = MaxCarryable;
        Result.MaxOnVendor = MaxOnVendor;
        Result.MaxStackSize = MaxStackSize;
        Result.MountedAppearance = MountedAppearance;
        Result.Name = Name;
        Result.NumUses = NumberOfUses;
        Result.RequiredAppearance = RequiredAppearance;
        Result.SkillReqs = SkillRequirements;
        Result.StockDye = StockDyeToStrings(StockDye, HasStockDye);
        Result.TSysProfile = TSysProfile;
        Result.Value = Value;

        return Result;
    }

    private static string? DroppedAppearanceToStrings(DroppedAppearance? droppedAppearance)
    {
        if (droppedAppearance is null)
            return null;

        string SkinString = droppedAppearance.Skin is null ? string.Empty : (droppedAppearance.GetIsSkinInverted() ? $"Skin=^{droppedAppearance.Skin}" : $"^Skin={droppedAppearance.Skin}");
        string CorkString = droppedAppearance.Cork is null ? string.Empty : $"^Cork={droppedAppearance.Cork}";
        string FoodString = droppedAppearance.Food is null ? string.Empty : $"^Food={droppedAppearance.Food}";
        string PlateString = droppedAppearance.Plate is null ? string.Empty : $"^Plate={droppedAppearance.Plate}";
        string ColorString = droppedAppearance.Color is null ? string.Empty : $"Color={droppedAppearance.GetColorFormat()}";
        string SkinColorString = droppedAppearance.SkinColor is null ? string.Empty : $"Skin_Color={droppedAppearance.GetSkinColorFormat()}";

        string[] StringParameters = new string[]
        {
            SkinString,
            CorkString,
            FoodString,
            PlateString,
            ColorString,
            SkinColorString,
        };

        string Parameter = string.Join(";", StringParameters);

        while (Parameter.Contains(";;"))
            Parameter = Parameter.Replace(";;", ";");

        if (Parameter.StartsWith(";"))
            Parameter = Parameter.Substring(1);
        if (Parameter.EndsWith(";"))
            Parameter = Parameter.Substring(0, Parameter.Length - 1);

        string? Result = Parameter == string.Empty ? droppedAppearance.Appearance : $"{droppedAppearance.Appearance}({Parameter})";

        return Result;
    }

    private static string[]? EffectDescriptionsToString(ItemEffect[]? effectDescriptions)
    {
        if (effectDescriptions is null)
            return null;

        string[] Result = new string[effectDescriptions.Length];

        for (int i = 0; i < effectDescriptions.Length; i++)
            Result[i] = EffectDescriptionToString(effectDescriptions[i]);

        return Result;
    }

    private static string EffectDescriptionToString(ItemEffect effectDescription)
    {
        if (effectDescription.Description is not null)
            return effectDescription.Description;
        else
            return $"{{{effectDescription.AttributeName}}}{{{effectDescription.AttributeEffect?.ToString(CultureInfo.InvariantCulture)}}}";
    }

    private static string[]? KeywordsToString(KeywordValues[]? keywordValuesArray, string[]? rawKeywords)
    {
        if (keywordValuesArray is null)
            return null;

        List<string> Result = new();

        foreach (KeywordValues KeywordValues in keywordValuesArray)
            if (KeywordValues.Keyword is string Keyword)
            {
                if (KeywordValues.Values is decimal[] Values)
                {
                    foreach (decimal Value in Values)
                        Result.Add($"{Keyword}={Value.ToString(CultureInfo.InvariantCulture)}");
                }
                else
                    Result.Add(Keyword);
            }

        string[] ConvertedResult = Result.ToArray();
        KeywordValues[]? ConfirmKeywordValuesArray = ParseKeywords(ConvertedResult);

        if (ConfirmKeywordValuesArray is null || rawKeywords is null)
            throw new NullReferenceException();

        if (keywordValuesArray.Length != ConfirmKeywordValuesArray.Length)
            throw new InvalidCastException();

        for (int i = 0; i < keywordValuesArray.Length; i++)
            if (!keywordValuesArray[i].Equals(ConfirmKeywordValuesArray[i]))
                throw new InvalidCastException();

        return rawKeywords;
    }

    private static string? StockDyeToStrings(StockDye? stockDye, bool hasStockDye)
    {
        if (stockDye is null)
            return hasStockDye ? string.Empty : null;

        string Result = $";Color1={stockDye.GetColorFormat(0)};Color2={stockDye.GetColorFormat(1)};Color3={stockDye.GetColorFormat(2)}";

        if (stockDye.IsGlowEnabled)
            Result += ";GlowEnabled=y";

        return Result;
    }

    private string[]? RawKeywords;
    private bool HasStockDye;
}
