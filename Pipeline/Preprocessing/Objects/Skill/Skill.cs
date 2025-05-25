namespace Preprocessor;

using System.Collections.Generic;
using System.Linq;

public class Skill
{
    public Skill(RawSkill rawSkill)
    {
        ActiveAdvancementTable = rawSkill.ActiveAdvancementTable;
        AdvancementHints = rawSkill.AdvancementHints;
        AuxCombat = rawSkill.AuxCombat;
        Combat = rawSkill.Combat;
        Description = rawSkill.Description;
        GuestLevelCap = rawSkill.GuestLevelCap;
        HideWhenZero = rawSkill.HideWhenZero;
        Id = rawSkill.Id;
        InteractionFlagLevelCaps = rawSkill.InteractionFlagLevelCaps;
        IsFakeCombatSkill = rawSkill.IsFakeCombatSkill;
        IsUmbrellaSkill = rawSkill.IsUmbrellaSkill;
        MaxBonusLevels = rawSkill.MaxBonusLevels;
        Name = rawSkill.Name;
        Parents = rawSkill.Parents;
        PassiveAdvancementTable = rawSkill.PassiveAdvancementTable;
        RecipeIngredientKeywords = rawSkill.RecipeIngredientKeywords;
        Reports = rawSkill.Reports;
        Rewards = rawSkill.Rewards;
        SkillLevelDisparityApplies = rawSkill.SkillLevelDisparityApplies;
        SkipBonusLevelsIfSkillUnlearned = rawSkill.SkipBonusLevelsIfSkillUnlearned;
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

    public string? ActiveAdvancementTable { get; set; }
    public SkillAdvancementHintCollection? AdvancementHints { get; set; }
    public bool? AuxCombat { get; set; }
    public bool? Combat { get; set; }
    public string? Description { get; set; }
    public int? GuestLevelCap { get; set; }
    public bool? HideWhenZero { get; set; }
    public int? Id { get; set; }
    public SkillLevelCapCollection? InteractionFlagLevelCaps { get; set; }
    public bool? IsFakeCombatSkill { get; set; }
    public bool? IsUmbrellaSkill { get; set; }
    public int? MaxBonusLevels { get; set; }
    public string? Name { get; set; }
    public string[]? Parents { get; set; }
    public string? PassiveAdvancementTable { get; set; }
    public string[]? RecipeIngredientKeywords { get; set; }
    public SkillReportCollection? Reports { get; set; }
    public SkillRewardCollection? Rewards { get; set; }
    public bool? SkillLevelDisparityApplies { get; set; }
    public bool? SkipBonusLevelsIfSkillUnlearned { get; set; }
    public string[]? TSysCompatibleCombatSkills { get; set; }
    public string? XpTable { get; set; }

    public RawSkill ToRawSkill()
    {
        RawSkill Result = new();

        Result.ActiveAdvancementTable = ActiveAdvancementTable;
        Result.AdvancementHints = AdvancementHints;
        Result.AuxCombat = AuxCombat;
        Result.Combat = Combat;
        Result.Description = Description;
        Result.GuestLevelCap = GuestLevelCap;
        Result.HideWhenZero = HideWhenZero;
        Result.Id = Id;
        Result.InteractionFlagLevelCaps = InteractionFlagLevelCaps;
        Result.IsFakeCombatSkill = IsFakeCombatSkill;
        Result.IsUmbrellaSkill = IsUmbrellaSkill;
        Result.MaxBonusLevels = MaxBonusLevels;
        Result.Name = Name;
        Result.Parents = Parents;
        Result.PassiveAdvancementTable = PassiveAdvancementTable;
        Result.Reports = Reports;
        Result.Rewards = Rewards;
        Result.SkillLevelDisparityApplies = SkillLevelDisparityApplies;
        Result.SkipBonusLevelsIfSkillUnlearned = SkipBonusLevelsIfSkillUnlearned;
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
