namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

public class Recipe
{
    private const string Whittling = "Whittling";
    private const string CraftingEnhanceItem = "CraftingEnhanceItem";
    private const string PolymorphRabbitPermanent = "PolymorphRabbitPermanent";
    private const string CreateMiningSurvey = "CreateMiningSurvey";
    private const string CreateGeologySurvey = "CreateGeologySurvey";
    private const string AreaHeader = "Area";

    public Recipe(RawRecipe rawRecipe)
    {
        ActionLabel = rawRecipe.ActionLabel;
        Costs = rawRecipe.Costs;
        Description = rawRecipe.Description;
        DyeColor = rawRecipe.DyeColor;
        IconId = rawRecipe.IconId;
        Ingredients = Preprocessor.ToSingleOrMultiple(rawRecipe.Ingredients, (RawRecipeItem rawRecipeItem) => new RecipeItem(rawRecipeItem), out IngredientsFormat);
        InternalName = rawRecipe.InternalName;
        IsItemMenuKeywordRequirementSufficient = rawRecipe.IsItemMenuKeywordReqSufficient;
        ItemMenuCategory = ParseItemMenuCategory(rawRecipe.ItemMenuCategory);
        ItemMenuCategoryLevel = rawRecipe.ItemMenuCategoryLevel;
        ItemMenuKeywordRequirement = ParseItemMenuKeywordReq(rawRecipe.ItemMenuKeywordReq);
        ItemMenuLabel = rawRecipe.ItemMenuLabel;
        Keywords = rawRecipe.Keywords;
        LoopParticle = RecipeParticle.Parse(rawRecipe.LoopParticle);
        MaxUses = rawRecipe.MaxUses;
        Name = rawRecipe.Name;
        NumberOfResultItems = rawRecipe.NumResultItems;
        OtherRequirements = Preprocessor.ToSingleOrMultiple(rawRecipe.OtherRequirements, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out OtherRequirementsFormat);
        Particle = RecipeParticle.Parse(rawRecipe.Particle);
        PrereqRecipe = rawRecipe.PrereqRecipe;
        ProtoResultItems = Preprocessor.ToSingleOrMultiple(rawRecipe.ProtoResultItems, (RawRecipeItem rawRecipeItem) => new RecipeItem(rawRecipeItem), out ProtoResultItemsFormat);
        RequiredAttributeNonZero = rawRecipe.RequiredAttributeNonZero;
        ResetTimeInSeconds = rawRecipe.ResetTimeInSeconds;
        ResultEffects = ParseResultEffects(rawRecipe.ResultEffects);
        ResultItems = Preprocessor.ToSingleOrMultiple(rawRecipe.ResultItems, (RawRecipeItem rawRecipeItem) => new RecipeItem(rawRecipeItem), out ResultItemsFormat);
        RewardAllowBonusXp = rawRecipe.RewardAllowBonusXp;
        RewardSkill = rawRecipe.RewardSkill;
        RewardSkillXp = rawRecipe.RewardSkillXp;
        RewardSkillXpDropOffLevel = rawRecipe.RewardSkillXpDropOffLevel;
        RewardSkillXpDropOffPct = rawRecipe.RewardSkillXpDropOffPct;
        RewardSkillXpDropOffRate = rawRecipe.RewardSkillXpDropOffRate;
        RewardSkillXpFirstTime = rawRecipe.RewardSkillXpFirstTime;
        SharesResetTimerWith = rawRecipe.SharesResetTimerWith;
        Skill = rawRecipe.Skill;
        SkillLevelRequirement = rawRecipe.SkillLevelReq;
        SortSkill = rawRecipe.SortSkill;
        UsageAnimation = rawRecipe.UsageAnimation;
        UsageAnimationEnd = rawRecipe.UsageAnimationEnd;
        UsageDelay = rawRecipe.UsageDelay;
        UsageDelayMessage = rawRecipe.UsageDelayMessage;
        ValidationIngredientKeywords = rawRecipe.ValidationIngredientKeywords;
    }

    private static RecipeResultEffect[]? ParseResultEffects(string[]? effects)
    {
        if (effects is null)
            return null;

        List<RecipeResultEffect> ResultEffects = new();

        foreach (string Effect in effects)
            ResultEffects.Add(ParseResultEffect(Effect));

        return ResultEffects.ToArray();
    }

    private static RecipeResultEffect ParseResultEffect(string effect)
    {
        string EffectName;
        string EffectParameter;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(effect, ParameterPattern, RegexOptions.IgnoreCase);
        if (ParameterMatch.Success)
        {
            EffectName = effect.Substring(0, ParameterMatch.Index);
            EffectParameter = effect.Substring(ParameterMatch.Index + 1, effect.Length - ParameterMatch.Index - 2);
        }
        else
        {
            EffectName = effect;
            EffectParameter = string.Empty;
        }

        if (EffectName.StartsWith(Whittling))
            return ParseResultEffectWithTier(EffectName);
        else if (EffectName.StartsWith(CraftingEnhanceItem))
            return ParseCraftingEnhanceItem(CraftingEnhanceItem, EffectName.Substring(CraftingEnhanceItem.Length), EffectParameter);
        else if (EffectName.StartsWith(PolymorphRabbitPermanent))
            return ParsePolymorph(PolymorphRabbitPermanent, EffectName.Substring(PolymorphRabbitPermanent.Length));
        else if (EffectName.StartsWith(CreateMiningSurvey))
            return ParseCreateMiningSurvey(EffectName.Substring(CreateMiningSurvey.Length), EffectParameter);
        else if (EffectName.StartsWith(CreateGeologySurvey))
            return ParseCreateGeologySurvey(EffectName.Substring(CreateGeologySurvey.Length), EffectParameter);

        switch (EffectName)
        {
            case "ExtractTSysPower":
                return ParseExtractTSysPower(EffectName, EffectParameter);
            case "RepairItemDurability":
                return ParseRepairItemDurability(EffectName, EffectParameter);
            case "TSysCraftedEquipment":
                return ParseTSysCraftedEquipment(EffectName, EffectParameter);
            case "CraftSimpleTSysItem":
                return ParseCraftSimpleTSysItem(EffectName, EffectParameter);
            case "AddItemTSysPower":
                return ParseAddItemTSysPower(EffectName, EffectParameter);
            case "AddItemTSysPowerWax":
                return ParseAddItemTSysPowerWax(EffectName, EffectParameter);
            case "BrewItem":
                return ParseBrewItem(EffectName, EffectParameter);
            case "AdjustRecipeReuseTime":
                return ParseAdjustRecipeReuseTime(EffectName, EffectParameter);
            case "GiveTSysItem":
                return ParseGiveTSysItem(EffectName, EffectParameter);
            case "ConsumeItemUses":
                return ParseConsumeItemUses(EffectName, EffectParameter);
            case "DeltaCurFairyEnergy":
                return ParseDeltaCurFairyEnergy(EffectName, EffectParameter);
            case "Teleport":
                return ParseTeleport(EffectName, EffectParameter);
            case "SpawnPremonition_All_2sec":
                return ParseSpawnPremonition("SpawnPremonition", 2);
            case "SpawnPremonition_All_4sec":
                return ParseSpawnPremonition("SpawnPremonition", 4);
            case "PermanentlyRaiseMaxTempestEnergy":
                return ParsePermanentlyRaiseMaxTempestEnergy(EffectName, EffectParameter);
            case "CraftingResetItem":
            case "SendItemToSaddlebag":
            case "TransmogItemAppearance":
                return new RecipeResultEffect() { Type = EffectName };
            default:
                return new RecipeResultEffect() { Type = "Special", Effect = EffectName };
        }
    }

    private static RecipeResultEffect ParseResultEffectWithTier(string effectName)
    {
        // Get the last number in a string.
        string NumberPattern = @"\d+$";
        Match NumberMatch = Regex.Match(effectName, NumberPattern, RegexOptions.IgnoreCase);
        string EffectString = effectName.Substring(0, NumberMatch.Index);
        string TierString = NumberMatch.Value;
        int Tier = int.Parse(TierString);

        return new RecipeResultEffect() { Type = "Tiered", Keyword = EffectString, Tier = Tier };
    }

    private static RecipeResultEffect ParseCraftingEnhanceItem(string effectName, string enhancement, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 2)
            throw new PreprocessorException();

        decimal AddedQuantity = decimal.Parse(Splitted[0], CultureInfo.InvariantCulture);
        int ConsumedEnhancementPoint = int.Parse(Splitted[1]);

        return new RecipeResultEffect() { Type = effectName, Enhancement = enhancement, AddedQuantity = AddedQuantity, ConsumedEnhancementPoints = ConsumedEnhancementPoint };
    }

    private static RecipeResultEffect ParsePolymorph(string effectName, string effectParameter)
    {
        string Color = RgbColor.Parse(effectParameter, string.Empty, out ColorFormat ColorFormat);

        RecipeResultEffect Result = new RecipeResultEffect() { Type = effectName, Color = Color };
        Result.SetColorFormat(ColorFormat);
        return Result;
    }

    private static RecipeResultEffect ParseExtractTSysPower(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 4)
            throw new PreprocessorException();

        string Augment = Splitted[0];
        string Skill = Splitted[1];
        int MinLevel = int.Parse(Splitted[2]);
        int MaxLevel = int.Parse(Splitted[3]);

        return new RecipeResultEffect() { Type = effectName, Augment = Augment, Skill = Skill, MinLevel = MinLevel, MaxLevel = MaxLevel };
    }

    private static RecipeResultEffect ParseRepairItemDurability(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 5)
            throw new PreprocessorException();

        int RepairMinEfficiency = (int)(decimal.Parse(Splitted[0], CultureInfo.InvariantCulture) * 100);
        int RepairMaxEfficiency = (int)(decimal.Parse(Splitted[1], CultureInfo.InvariantCulture) * 100);
        Time RepairCooldown = new Time() { Hours = int.Parse(Splitted[2]) };
        int MinLevel = int.Parse(Splitted[3]);
        int MaxLevel = int.Parse(Splitted[4]);

        return new RecipeResultEffect() { Type = effectName, RepairMinEfficiency = RepairMinEfficiency, RepairMaxEfficiency = RepairMaxEfficiency, RepairCooldown = RepairCooldown, MinLevel = MinLevel, MaxLevel = MaxLevel };
    }

    private static RecipeResultEffect ParseTSysCraftedEquipment(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length > 3)
            throw new PreprocessorException();

        string CraftedItem = Splitted[0].Trim();
        if (CraftedItem == string.Empty)
            throw new PreprocessorException();

        bool? IsCamouflaged = null;
        if (CraftedItem[CraftedItem.Length - 1] == 'C')
        {
            IsCamouflaged = true;
            CraftedItem = CraftedItem.Substring(0, CraftedItem.Length - 1);
        }

        int? BoostLevel = null;

        // Get the last number in a string.
        string NumberPattern = @"\d+$";
        Match NumberMatch = Regex.Match(CraftedItem, NumberPattern, RegexOptions.IgnoreCase);
        if (NumberMatch.Success)
        {
            CraftedItem = CraftedItem.Substring(0, NumberMatch.Index);
            BoostLevel = int.Parse(NumberMatch.Value);
        }

        int? AdditionalEnchantments = Splitted.Length > 1 ? int.Parse(Splitted[1]) : null;
        string? BoostedAnimal = Splitted.Length > 2 ? Splitted[2] : null;

        return new RecipeResultEffect() { Type = effectName, Boost = CraftedItem, IsCamouflaged = IsCamouflaged, BoostLevel = BoostLevel, AdditionalEnchantments = AdditionalEnchantments, BoostedAnimal = BoostedAnimal };
    }

    private static RecipeResultEffect ParseCraftSimpleTSysItem(string effectName, string effectParameter)
    {
        return new RecipeResultEffect() { Type = effectName, Item = effectParameter };
    }

    private static RecipeResultEffect ParseAddItemTSysPower(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 2)
            throw new PreprocessorException();

        string Slot = Splitted[0];
        int Tier = int.Parse(Splitted[1]);

        return new RecipeResultEffect() { Type = effectName, Slot = Slot, Tier = Tier };
    }

    private static RecipeResultEffect ParseAddItemTSysPowerWax(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 3)
            throw new PreprocessorException();

        string PowerWaxType = Splitted[0];
        int PowerLevel = int.Parse(Splitted[1]);
        int MaxHitCount = int.Parse(Splitted[2]);

        return new RecipeResultEffect() { Type = effectName, PowerWaxType = PowerWaxType, PowerLevel = PowerLevel, MaxHitCount = MaxHitCount };
    }

    private static RecipeResultEffect ParseBrewItem(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 3)
            throw new PreprocessorException();

        int BrewLine = int.Parse(Splitted[0]);
        int BrewStrength = int.Parse(Splitted[1]);
        string[] PartSplit = Splitted[2].Trim().Split('=');

        if (PartSplit.Length != 2)
            throw new PreprocessorException();

        string[] BrewParts = PartSplit[0].Trim().Split('+');
        string[] BrewResults = PartSplit[1].Trim().Split('+');

        return new RecipeResultEffect() { Type = effectName, BrewLine = BrewLine, BrewStrength = BrewStrength, BrewParts = BrewParts, BrewResults = BrewResults };
    }

    private static RecipeResultEffect ParseAdjustRecipeReuseTime(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 2)
            throw new PreprocessorException();

        int Seconds = -int.Parse(Splitted[0]);
        if ((Seconds % 60) != 0)
            throw new PreprocessorException();

        int Minutes = Seconds / 60;
        int Hours = Minutes / 60;
        Time AdjustedReuseTime;

        if (Minutes == Hours * 60)
            AdjustedReuseTime = new Time() { Hours = Hours };
        else
            AdjustedReuseTime = new Time() { Hours = Hours, Minutes = Minutes - Hours * 60 };

        string MoonPhase = Splitted[1];

        return new RecipeResultEffect() { Type = effectName, AdjustedReuseTime = AdjustedReuseTime, MoonPhase = MoonPhase };
    }

    private static RecipeResultEffect ParseGiveTSysItem(string effectName, string effectParameter)
    {
        return new RecipeResultEffect() { Type = effectName, Item = effectParameter };
    }

    private static RecipeResultEffect ParseConsumeItemUses(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 2)
            throw new PreprocessorException();

        string Keyword = Splitted[0];
        int ConsumedUses = int.Parse(Splitted[1]);

        return new RecipeResultEffect() { Type = effectName, Keyword = Keyword, ConsumedUses = ConsumedUses };
    }

    private static RecipeResultEffect ParseDeltaCurFairyEnergy(string effectName, string effectParameter)
    {
        int Delta = int.Parse(effectParameter);

        return new RecipeResultEffect() { Type = effectName, Delta = Delta };
    }

    private static RecipeResultEffect ParseTeleport(string effectName, string effectParameter)
    {
        string[] Splitted = effectParameter.Split(',');

        if (Splitted.Length != 2)
            throw new PreprocessorException();

        string AreaName = Splitted[0];
        string Other = Splitted[1];

        if (!AreaName.StartsWith(AreaHeader))
            throw new PreprocessorException();

        AreaName = AreaName.Substring(AreaHeader.Length);
        AreaName = Area.FromRawAreaName(AreaName, out _)!;

        if (Other == " NewFairySpot")
            Other = "New Fairy Spot";
        else if (Other == " Landing_Boat")
            Other = "Landing Boat";
        else if (Other == " SpecialDestination")
            Other = "Special Destination";
        else
            throw new PreprocessorException();

        return new RecipeResultEffect() { Type = effectName, AreaName = AreaName, Other = Other };
    }

    private static RecipeResultEffect ParseCreateMiningSurvey(string effectName, string effectParameter)
    {
        return new RecipeResultEffect() { Type = CreateMiningSurvey, Effect = effectName, Item = effectParameter };
    }

    private static RecipeResultEffect ParseCreateGeologySurvey(string effectName, string effectParameter)
    {
        return new RecipeResultEffect() { Type = CreateGeologySurvey, Effect = effectName, Item = effectParameter };
    }

    private static RecipeResultEffect ParseSpawnPremonition(string effectName, int durationInSeconds)
    {
        return new RecipeResultEffect() { Type = effectName, DurationInSeconds = durationInSeconds };
    }

    private static RecipeResultEffect ParsePermanentlyRaiseMaxTempestEnergy(string effectName, string effectParameter)
    {
        int Delta = int.Parse(effectParameter);

        return new RecipeResultEffect() { Type = effectName, Delta = Delta };
    }

    private static string? ParseItemMenuCategory(string? rawItemMenuCategory)
    {
        switch (rawItemMenuCategory)
        {
            case "TSysExtract":
                return "Extract";
            case "TSysDistill":
                return "Distill";
            default:
                return null;
        }
    }

    private static string? ParseItemMenuKeywordReq(string? rawItemMenuKeywordReq)
    {
        switch (rawItemMenuKeywordReq)
        {
            case null:
                return null;
            case "MinRarity:Uncommon":
                return "MinRarity_Uncommon";
            default:
                return rawItemMenuKeywordReq;
        }
    }

    public string? ActionLabel { get; set; }
    public Cost[]? Costs { get; set; }
    public string? Description { get; set; }
    public string? DyeColor { get; set; }
    public int IconId { get; set; }
    public RecipeItem[]? Ingredients { get; set; }
    public string? InternalName { get; set; }
    public bool? IsItemMenuKeywordRequirementSufficient { get; set; }
    public string? ItemMenuCategory { get; set; }
    public int? ItemMenuCategoryLevel { get; set; }
    public string? ItemMenuKeywordRequirement { get; set; }
    public string? ItemMenuLabel { get; set; }
    public string[]? Keywords { get; set; }
    public RecipeParticle? LoopParticle { get; set; }
    public int? MaxUses { get; set; }
    public string? Name { get; set; }
    public int? NumberOfResultItems { get; set; }
    public Requirement[]? OtherRequirements { get; set; }
    public RecipeParticle? Particle { get; set; }
    public string? PrereqRecipe { get; set; }
    public RecipeItem[]? ProtoResultItems { get; set; }
    public string? RequiredAttributeNonZero { get; set; }
    public int? ResetTimeInSeconds { get; set; }
    public RecipeResultEffect[]? ResultEffects { get; set; }
    public RecipeItem[]? ResultItems { get; set; }
    public bool? RewardAllowBonusXp { get; set; }
    public string? RewardSkill { get; set; }
    public int? RewardSkillXp { get; set; }
    public int? RewardSkillXpDropOffLevel { get; set; }
    public decimal? RewardSkillXpDropOffPct { get; set; }
    public int? RewardSkillXpDropOffRate { get; set; }
    public int? RewardSkillXpFirstTime { get; set; }
    public string? SharesResetTimerWith { get; set; }
    public string? Skill { get; set; }
    public int? SkillLevelRequirement { get; set; }
    public string? SortSkill { get; set; }
    public string? UsageAnimation { get; set; }
    public string? UsageAnimationEnd { get; set; }
    public decimal? UsageDelay { get; set; }
    public string? UsageDelayMessage { get; set; }
    public string[]? ValidationIngredientKeywords { get; set; }

    public RawRecipe ToRawRecipe()
    {
        RawRecipe Result = new();

        Result.ActionLabel = ActionLabel;
        Result.Costs = Costs;
        Result.Description = Description;
        Result.DyeColor = DyeColor;
        Result.IconId = IconId;
        Result.Ingredients = Preprocessor.FromSingleOrMultiple(Ingredients, (RecipeItem recipeItem) => recipeItem.ToRawRecipeItem(), IngredientsFormat);
        Result.InternalName = InternalName;
        Result.IsItemMenuKeywordReqSufficient = IsItemMenuKeywordRequirementSufficient;
        Result.ItemMenuCategory = ToRawItemMenuCategory(ItemMenuCategory);
        Result.ItemMenuCategoryLevel = ItemMenuCategoryLevel;
        Result.ItemMenuKeywordReq = ToRawItemMenuKeywordReq(ItemMenuKeywordRequirement);
        Result.ItemMenuLabel = ItemMenuLabel;
        Result.Keywords = Keywords;
        Result.LoopParticle = RecipeParticle.ToString(LoopParticle);
        Result.MaxUses = MaxUses;
        Result.Name = Name;
        Result.NumResultItems = NumberOfResultItems;
        Result.OtherRequirements = Preprocessor.FromSingleOrMultiple(OtherRequirements, (Requirement requirement) => requirement.ToRawRequirement(), OtherRequirementsFormat);
        Result.Particle = RecipeParticle.ToString(Particle);
        Result.PrereqRecipe = PrereqRecipe;
        Result.ProtoResultItems = Preprocessor.FromSingleOrMultiple(ProtoResultItems, (RecipeItem recipeItem) => recipeItem.ToRawRecipeItem(), ProtoResultItemsFormat);
        Result.RequiredAttributeNonZero = RequiredAttributeNonZero;
        Result.ResetTimeInSeconds = ResetTimeInSeconds;
        Result.ResultEffects = ToRawResultEffects(ResultEffects);
        Result.ResultItems = Preprocessor.FromSingleOrMultiple(ResultItems, (RecipeItem recipeItem) => recipeItem.ToRawRecipeItem(), ResultItemsFormat);
        Result.RewardAllowBonusXp = RewardAllowBonusXp;
        Result.RewardSkill = RewardSkill;
        Result.RewardSkillXp = RewardSkillXp;
        Result.RewardSkillXpDropOffLevel = RewardSkillXpDropOffLevel;
        Result.RewardSkillXpDropOffPct = RewardSkillXpDropOffPct;
        Result.RewardSkillXpDropOffRate = RewardSkillXpDropOffRate;
        Result.RewardSkillXpFirstTime = RewardSkillXpFirstTime;
        Result.SharesResetTimerWith = SharesResetTimerWith;
        Result.Skill = Skill;
        Result.SkillLevelReq = SkillLevelRequirement;
        Result.SortSkill = SortSkill;
        Result.UsageAnimation = UsageAnimation;
        Result.UsageAnimationEnd = UsageAnimationEnd;
        Result.UsageDelay = UsageDelay;
        Result.UsageDelayMessage = UsageDelayMessage;
        Result.ValidationIngredientKeywords = ValidationIngredientKeywords;

        return Result;
    }

    private static string[]? ToRawResultEffects(RecipeResultEffect[]? effects)
    {
        if (effects is null)
            return null;

        List<string> ResultEffects = new();

        foreach (RecipeResultEffect Effect in effects)
        {
            string RawResultEffect = ToRawResultEffect(Effect);
            ResultEffects.Add(RawResultEffect);
        }

        return ResultEffects.ToArray();
    }

    private static string ToRawResultEffect(RecipeResultEffect effect)
    {
        string Type = effect.Type;

        if (Type.StartsWith(CraftingEnhanceItem))
            return ToRawCraftingEnhanceItem(effect);
        else if (Type.StartsWith(PolymorphRabbitPermanent))
            return ToRawPolymorph(effect);

        switch (effect.Type)
        {
            case "Tiered":
                return $"{effect.Keyword}{effect.Tier}";
            case "ExtractTSysPower":
                return $"{effect.Type}({effect.Augment},{effect.Skill},{effect.MinLevel},{effect.MaxLevel})";
            case "RepairItemDurability":
                return ToRawRepairItemDurability(effect);
            case "TSysCraftedEquipment":
                return ToRawTSysCraftedEquipment(effect);
            case "CraftSimpleTSysItem":
                return $"{effect.Type}({effect.Item})";
            case "AddItemTSysPower":
                return $"{effect.Type}({effect.Slot},{effect.Tier})";
            case "AddItemTSysPowerWax":
                return $"{effect.Type}({effect.PowerWaxType},{effect.PowerLevel},{effect.MaxHitCount})";
            case "BrewItem":
                return ToRawBrewItem(effect);
            case "AdjustRecipeReuseTime":
                return ToRawAdjustRecipeReuseTime(effect);
            case "GiveTSysItem":
                return $"{effect.Type}({effect.Item})";
            case "ConsumeItemUses":
                return $"{effect.Type}({effect.Keyword},{effect.ConsumedUses})";
            case "DeltaCurFairyEnergy":
                return $"{effect.Type}({effect.Delta})";
            case "Teleport":
                return ToRawTeleport(effect);
            case CreateMiningSurvey:
                return $"{effect.Type}{effect.Effect}({effect.Item})";
            case CreateGeologySurvey:
                return $"{effect.Type}{effect.Effect}({effect.Item})";
            case "SpawnPremonition":
                return $"{effect.Type}_All_{effect.DurationInSeconds}sec";
            case "PermanentlyRaiseMaxTempestEnergy":
                return $"{effect.Type}({effect.Delta})";
            case "Special":
                Debug.Assert(effect.Effect is not null);
                return effect.Effect!;
            default:
                return effect.Type;
        }
    }

    private static string ToRawCraftingEnhanceItem(RecipeResultEffect effect)
    {
        Debug.Assert(effect.AddedQuantity is not null);

        return $"{effect.Type}{effect.Enhancement}({effect.AddedQuantity!.Value.ToString(CultureInfo.InvariantCulture)},{effect.ConsumedEnhancementPoints})";
    }

    private static string ToRawPolymorph(RecipeResultEffect effect)
    {
        return $"{effect.Type}{effect.GetColorFormat()}";
    }

    private static string ToRawRepairItemDurability(RecipeResultEffect effect)
    {
        Debug.Assert(effect.RepairMinEfficiency is not null);
        Debug.Assert(effect.RepairMaxEfficiency is not null);
        Debug.Assert(effect.RepairCooldown is not null);

        Time RepairCooldown = effect.RepairCooldown!;
        Debug.Assert(RepairCooldown.Hours is not null);

        decimal RepairMinEfficiency = ((decimal)effect.RepairMinEfficiency!.Value) / 100;
        string RepairMinEfficiencyString = RepairMinEfficiency.ToString(CultureInfo.InvariantCulture);
        decimal RepairMaxEfficiency = ((decimal)effect.RepairMaxEfficiency!.Value) / 100;
        string RepairMaxEfficiencyString = RepairMaxEfficiency.ToString(CultureInfo.InvariantCulture);

        if (RepairMaxEfficiencyString == "1")
            RepairMaxEfficiencyString = "1.0";

        return $"{effect.Type}({RepairMinEfficiencyString},{RepairMaxEfficiencyString},{RepairCooldown.Hours},{effect.MinLevel},{effect.MaxLevel})";
    }

    private static string ToRawTSysCraftedEquipment(RecipeResultEffect effect)
    {
        string CraftedItem = $"{effect.Boost}{effect.BoostLevel}{(effect.IsCamouflaged == true ? "C" : "")}";

        if (effect.AdditionalEnchantments is int AdditionalEnchantments)
            CraftedItem += $",{AdditionalEnchantments}";

        if (effect.BoostedAnimal is string BoostedAnimal)
            CraftedItem += $",{BoostedAnimal}";

        return $"{effect.Type}({CraftedItem})";
    }

    private static string ToRawBrewItem(RecipeResultEffect effect)
    {
        string BrewParts = string.Join("+", effect.BrewParts);
        string BrewResults = string.Join("+", effect.BrewResults);

        return $"{effect.Type}({effect.BrewLine},{effect.BrewStrength},{BrewParts}={BrewResults})";
    }

    private static string ToRawAdjustRecipeReuseTime(RecipeResultEffect effect)
    {
        int Seconds = 0;

        Debug.Assert(effect.AdjustedReuseTime is not null);
        Time AdjustedReuseTime = effect.AdjustedReuseTime!;

        Seconds += AdjustedReuseTime.Hours is null ? 0 : AdjustedReuseTime.Hours.Value * 3600;
        Seconds += AdjustedReuseTime.Minutes is null ? 0 : AdjustedReuseTime.Minutes.Value * 60;

        return $"{effect.Type}(-{Seconds},{effect.MoonPhase})";
    }

    private static string ToRawTeleport(RecipeResultEffect effect)
    {
        string Other;

        switch (effect.Other)
        {
            case "Landing Boat":
                Other = "Landing_Boat";
                break;
            default: // Special Destination and New Fairy Spot
                Other = effect.Other!.Replace(" ", string.Empty);
                break;
        }

        string? AreaName = Area.FromRawAreaName(effect.AreaName, out _);

        return $"{effect.Type}(Area{AreaName}, {Other})";
    }

    private static string? ToRawItemMenuCategory(string? itemMenuCategory)
    {
        switch (itemMenuCategory)
        {
            case "Extract":
                return "TSysExtract";
            case "Distill":
                return "TSysDistill";
            default:
                return null;
        }
    }

    private static string? ToRawItemMenuKeywordReq(string? itemMenuKeywordRequirement)
    {
        switch (itemMenuKeywordRequirement)
        {
            case null:
                return null;
            case "MinRarity_Uncommon":
                return "MinRarity:Uncommon";
            default:
                return itemMenuKeywordRequirement;
        }
    }

    private readonly JsonArrayFormat IngredientsFormat;
    private readonly JsonArrayFormat OtherRequirementsFormat;
    private readonly JsonArrayFormat ProtoResultItemsFormat;
    private readonly JsonArrayFormat ResultItemsFormat;
}
