namespace Preprocessor;

internal class Recipe
{
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
        ItemMenuCategory = rawRecipe.ItemMenuCategory;
        ItemMenuCategoryLevel = rawRecipe.ItemMenuCategoryLevel;
        ItemMenuKeywordRequirement = rawRecipe.ItemMenuKeywordReq;
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
        ResultEffects = rawRecipe.ResultEffects;
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
    public string[]? ResultEffects { get; set; }
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
        Result.ItemMenuCategory = ItemMenuCategory;
        Result.ItemMenuCategoryLevel = ItemMenuCategoryLevel;
        Result.ItemMenuKeywordReq = ItemMenuKeywordRequirement;
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
        Result.ResultEffects = ResultEffects;
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

    private readonly JsonArrayFormat IngredientsFormat;
    private readonly JsonArrayFormat OtherRequirementsFormat;
    private readonly JsonArrayFormat ProtoResultItemsFormat;
    private readonly JsonArrayFormat ResultItemsFormat;
}
