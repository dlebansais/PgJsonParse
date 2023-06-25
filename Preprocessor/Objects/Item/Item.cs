namespace Preprocessor;

using System;
using System.Diagnostics;
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
        EffectDescriptions = rawItem.EffectDescs;
        EquipAppearance = rawItem.EquipAppearance;
        EquipSlot = rawItem.EquipSlot;
        FoodDescription = rawItem.FoodDesc;
        IconId = rawItem.IconId;
        InternalName = rawItem.InternalName;
        IsCrafted = rawItem.IsCrafted;
        IsSkillRequirementsDefaults = rawItem.IsSkillReqsDefaults;
        IsTemporary = rawItem.IsTemporary;
        Keywords = rawItem.Keywords;
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
        StockDye = rawItem.StockDye;
        TSysProfile = rawItem.TSysProfile;
        Value = rawItem.Value;
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
                    Result.Color = Particle.ParseColor(Value, string.Empty, out string? ColorAsName);
                    Result.SetColorAsName(ColorAsName);
                    break;
                case "Skin_Color":
                    Result.SkinColor = Particle.ParseColor($"#{Value}", string.Empty, out string? SkinColorAsName);
                    Result.SetSkinColorAsName(SkinColorAsName);
                    break;
                default:
                    throw new InvalidCastException();
            }
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
    public string[]? EffectDescriptions { get; set; }
    public string? EquipAppearance { get; set; }
    public string? EquipSlot { get; set; }
    public string? FoodDescription { get; set; }
    public int IconId { get; set; }
    public string? InternalName { get; set; }
    public bool? IsCrafted { get; set; }
    public bool? IsSkillRequirementsDefaults { get; set; }
    public bool? IsTemporary { get; set; }
    public string[]? Keywords { get; set; }
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
    public string? StockDye { get; set; }
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
        Result.EffectDescs = EffectDescriptions;
        Result.EquipAppearance = EquipAppearance;
        Result.EquipSlot = EquipSlot;
        Result.FoodDesc = FoodDescription;
        Result.IconId = IconId;
        Result.InternalName = InternalName;
        Result.IsCrafted = IsCrafted;
        Result.IsSkillReqsDefaults = IsSkillRequirementsDefaults;
        Result.IsTemporary = IsTemporary;
        Result.Keywords = Keywords;
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
        Result.StockDye = StockDye;
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
        string ColorString = droppedAppearance.Color is null ? string.Empty : (droppedAppearance.GetColorAsName() is string ColorAsName ? $"Color={ColorAsName}" : $"Color=#{droppedAppearance.Color}");
        string SkinColorString = droppedAppearance.SkinColor is null ? string.Empty : (droppedAppearance.GetSkinColorAsName() is string SkinColorAsName ? $"Skin_Color={SkinColorAsName}" : $"Skin_Color={droppedAppearance.SkinColor}");

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
}
