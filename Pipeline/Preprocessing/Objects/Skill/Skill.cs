namespace Preprocessor;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Skill
{
    public Skill(string key)
    {
        Key = key;
    }

    public Skill(string key, RawSkill rawSkill)
        : this(key)
    {
        ActiveAdvancementTable = rawSkill.ActiveAdvancementTable;
        AdvancementHints = rawSkill.AdvancementHints;
        AssociatedAppearances = rawSkill.AssociatedAppearances;
        AssociatedItemKeywords = rawSkill.AssociatedItemKeywords;
        AuxCombat = rawSkill.AuxCombat;
        Combat = rawSkill.Combat;
        Description = rawSkill.Description;
        DisallowedAppearances = rawSkill.DisallowedAppearances;
        DisallowedItemKeywords = rawSkill.DisallowedItemKeywords;
        GuestLevelCap = rawSkill.GuestLevelCap;
        HideWhenZero = rawSkill.HideWhenZero;
        Id = rawSkill.Id;
        InteractionFlagLevelCaps = rawSkill.InteractionFlagLevelCaps;
        IsFakeCombatSkill = rawSkill.IsFakeCombatSkill;
        IsUmbrellaSkill = rawSkill.IsUmbrellaSkill;
        MaxBonusLevels = rawSkill.MaxBonusLevels;
        Name = rawSkill.Name;
        Parents = rawSkill.Parents;
        ParagonEnabledInteractionFlag = rawSkill.ParagonEnabledInteractionFlag;
        PassiveAdvancementTable = rawSkill.PassiveAdvancementTable;
        RecipeIngredientKeywords = rawSkill.RecipeIngredientKeywords;
        Reports = rawSkill.Reports;
        Rewards = rawSkill.Rewards;
        SkillLevelDisparityApplies = rawSkill.SkillLevelDisparityApplies;
        SkipBonusLevelsIfSkillUnlearned = rawSkill.SkipBonusLevelsIfSkillUnlearned;
        XpEarnedAttributes = rawSkill.XpEarnedAttributes;
        XpTable = rawSkill.XpTable;

        if (rawSkill._RecipeIngredientKeywords is not null)
        {
            if (rawSkill.RecipeIngredientKeywords is not null)
                throw new PreprocessorException(this);

            Is_RecipeIngredientKeywords = true;
            RecipeIngredientKeywords = rawSkill._RecipeIngredientKeywords;
        }
        else
            RecipeIngredientKeywords = rawSkill.RecipeIngredientKeywords;

        if (rawSkill.TSysCompatibleCombatSkills is not null)
        {
            UnsortedCombatSkills = rawSkill.TSysCompatibleCombatSkills;

            List<string> CombatSkills = new(UnsortedCombatSkills);
            CombatSkills.Sort();
            TSysCompatibleCombatSkills = CombatSkills.ToArray();
        }
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public string Key { get; set; }

    public string? ActiveAdvancementTable { get; set; }

    [Navigate(nameof(SkillAdvancementHint.Key))]
    public List<SkillAdvancementHint>? AdvancementHints { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AssociatedAppearances { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AssociatedItemKeywords { get; set; }

    public bool? AuxCombat { get; set; }

    public bool? Combat { get; set; }

    public string? Description { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? DisallowedAppearances { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? DisallowedItemKeywords { get; set; }

    public int? GuestLevelCap { get; set; }

    public bool? HideWhenZero { get; set; }

    public int? Id { get; set; }

    [Navigate(nameof(SkillLevelCap.Key))]
    public List<SkillLevelCap>? InteractionFlagLevelCaps { get; set; }

    public bool? IsFakeCombatSkill { get; set; }

    public bool? IsUmbrellaSkill { get; set; }

    public int? MaxBonusLevels { get; set; }

    public string? Name { get; set; }

    public string? ParagonEnabledInteractionFlag { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Parents { get; set; }

    public string? PassiveAdvancementTable { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? RecipeIngredientKeywords { get; set; }

    [Navigate(nameof(SkillReport.Key))]
    public List<SkillReport>? Reports { get; set; }

    [Navigate(nameof(SkillReward.Key))]
    public List<SkillReward>? Rewards { get; set; }

    public bool? SkillLevelDisparityApplies { get; set; }

    public bool? SkipBonusLevelsIfSkillUnlearned { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? TSysCompatibleCombatSkills { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? XpEarnedAttributes { get; set; }

    public string? XpTable { get; set; }

    public RawSkill ToRawSkill()
    {
        RawSkill Result = new();

        Result.ActiveAdvancementTable = ActiveAdvancementTable;
        Result.AdvancementHints = AdvancementHints is null ? null : new(AdvancementHints);
        Result.AssociatedAppearances = AssociatedAppearances;
        Result.AssociatedItemKeywords = AssociatedItemKeywords;
        Result.AuxCombat = AuxCombat;
        Result.Combat = Combat;
        Result.Description = Description;
        Result.DisallowedAppearances = DisallowedAppearances;
        Result.DisallowedItemKeywords = DisallowedItemKeywords;
        Result.GuestLevelCap = GuestLevelCap;
        Result.HideWhenZero = HideWhenZero;
        Result.Id = Id;
        Result.InteractionFlagLevelCaps = InteractionFlagLevelCaps is null ? null : new(InteractionFlagLevelCaps);
        Result.IsFakeCombatSkill = IsFakeCombatSkill;
        Result.IsUmbrellaSkill = IsUmbrellaSkill;
        Result.MaxBonusLevels = MaxBonusLevels;
        Result.Name = Name;
        Result.Parents = Parents;
        Result.ParagonEnabledInteractionFlag = ParagonEnabledInteractionFlag;
        Result.PassiveAdvancementTable = PassiveAdvancementTable;
        Result.Reports = Reports is null ? null : new(Reports);
        Result.Rewards = Rewards is null ? null : new(Rewards);
        Result.SkillLevelDisparityApplies = SkillLevelDisparityApplies;
        Result.SkipBonusLevelsIfSkillUnlearned = SkipBonusLevelsIfSkillUnlearned;
        Result.XpEarnedAttributes = XpEarnedAttributes;
        Result.XpTable = XpTable;

        if (Is_RecipeIngredientKeywords)
            Result._RecipeIngredientKeywords = RecipeIngredientKeywords;
        else
            Result.RecipeIngredientKeywords = RecipeIngredientKeywords;

        if (UnsortedCombatSkills is not null)
            Result.TSysCompatibleCombatSkills = UnsortedCombatSkills.ToArray();
        else
            Result.TSysCompatibleCombatSkills = TSysCompatibleCombatSkills;

        return Result;
    }

    private bool Is_RecipeIngredientKeywords;
    private string[]? UnsortedCombatSkills;
}
