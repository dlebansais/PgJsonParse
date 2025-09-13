namespace Preprocessor;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

public class Ability
{
    private const string FormHeader = "form:";
    private const string AnatomyTypeHeader = "AnatomyType_";
    private const string AnatomyHeader = "Anatomy_";
    private const string EmptyNameReplacement = "Blade Assault 2";

    public Ability(RawAbility rawAbility)
    {
        AbilityGroup = rawAbility.AbilityGroup;
        AbilityGroupName = rawAbility.AbilityGroupName;
        AmmoConsumeChance = rawAbility.AmmoConsumeChance;
        AmmoDescription = rawAbility.AmmoDescription;
        AmmoKeywords = rawAbility.AmmoKeywords;
        AmmoStickChance = rawAbility.AmmoStickChance;
        Animation = rawAbility.Animation;
        AttributeThatPreventsDelayLoopAbortOnAttacked = rawAbility.AttributeThatPreventsDelayLoopAbortOnAttacked;
        AttributesThatDeltaCritChance = rawAbility.AttributesThatDeltaCritChance;
        AttributesThatDeltaDelayLoopTime = rawAbility.AttributesThatDeltaDelayLoopTime;
        AttributesThatDeltaPowerCost = rawAbility.AttributesThatDeltaPowerCost;
        AttributesThatDeltaResetTime = rawAbility.AttributesThatDeltaResetTime;
        AttributesThatDeltaWorksWhileStunned = rawAbility.AttributesThatDeltaWorksWhileStunned;
        AttributesThatModAmmoConsumeChance = rawAbility.AttributesThatModAmmoConsumeChance;
        AttributesThatModPowerCost = rawAbility.AttributesThatModPowerCost;
        CanBeOnSidebar = rawAbility.CanBeOnSidebar;
        CanSuppressMonsterShout = rawAbility.CanSuppressMonsterShout;
        CanTargetUntargetableEnemies = rawAbility.CanTargetUntargetableEnemies;
        CausesOfDeath = rawAbility.CausesOfDeath;
        CombatRefreshBaseAmount = rawAbility.CombatRefreshBaseAmount;
        ConditionalKeywords = ParseConditionalKeywords(rawAbility.ConditionalKeywords);
        Costs = rawAbility.Costs;
        DamageType = rawAbility.DamageType;
        DelayLoopIsAbortedIfAttacked = rawAbility.DelayLoopIsAbortedIfAttacked;
        DelayLoopIsOnlyUsedInCombat = rawAbility.DelayLoopIsOnlyUsedInCombat;
        DelayLoopMessage = rawAbility.DelayLoopMessage;
        DelayLoopTime = rawAbility.DelayLoopTime;
        Description = rawAbility.Description;
        DigitStrippedName = ToDigitStrippedName(rawAbility.InternalName);
        EffectKeywordRequirementErrorMessage = rawAbility.EffectKeywordReqErrorMessage;
        EffectKeywordRequirements = rawAbility.EffectKeywordReqs;
        EffectKeywordsIndicatingEnabled = rawAbility.EffectKeywordsIndicatingEnabled;
        ExtraKeywordsForTooltips = rawAbility.ExtraKeywordsForTooltips;
        IconId = rawAbility.IconID;
        IgnoreEffectErrors = rawAbility.IgnoreEffectErrors;
        InternalName = rawAbility.InternalName;
        InventoryKeywordRequirementErrorMessage = rawAbility.InventoryKeywordReqErrorMessage;
        InventoryKeywordRequirements = rawAbility.InventoryKeywordReqs;
        IsAoECenteredOnCaster = rawAbility.AoEIsCenteredOnCaster;
        IsCosmeticPet = rawAbility.IsCosmeticPet;
        IsHarmless = rawAbility.IsHarmless;
        IsTimerResetWhenDisabling = rawAbility.IsTimerResetWhenDisabling;
        IsInternalAbility = rawAbility.InternalAbility;
        ItemKeywordRequirementErrorMessage = rawAbility.ItemKeywordReqErrorMessage;
        (FormRequirement, ItemKeywordRequirements) = ParseItemKeywordReqs(rawAbility.ItemKeywordReqs);
        Keywords = rawAbility.Keywords;
        Level = rawAbility.Level;

        IsNameEmpty = rawAbility.Name == string.Empty;
        Name = IsNameEmpty ? EmptyNameReplacement : rawAbility.Name;

        PetTypeTagRequirement = rawAbility.PetTypeTagReq;
        PetTypeTagRequirementMax = rawAbility.PetTypeTagReqMax;
        Prerequisite = rawAbility.Prerequisite;

        IsProjectileNone = rawAbility.Projectile == "0";
        Projectile = IsProjectileNone ? null : rawAbility.Projectile;

        PvE = rawAbility.PvE is null ? null : new PvEAbility(rawAbility.PvE);
        Rank = rawAbility.Rank is null ? null : int.Parse(rawAbility.Rank);
        ResetTime = rawAbility.ResetTime;
        SelfParticle = AbilityParticle.Parse(rawAbility.SelfParticle);
        SelfPreParticle = AbilityParticle.Parse(rawAbility.SelfPreParticle);
        SharesResetTimerWith = rawAbility.SharesResetTimerWith;
        Skill = rawAbility.Skill;
        SpecialCasterRequirements = Preprocessor.ToSingleOrMultiple(rawAbility.SpecialCasterRequirements, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out SpecialCasterRequirementsFormat);
        SpecialCasterRequirementsErrorMessage = rawAbility.SpecialCasterRequirementsErrorMessage;
        SpecialInfo = rawAbility.SpecialInfo;
        SpecialTargetingTypeRequirement = rawAbility.SpecialTargetingTypeReq;
        Target = rawAbility.Target;
        TargetEffectKeywordRequirement = rawAbility.TargetEffectKeywordReq;
        TargetParticle = AbilityParticle.Parse(rawAbility.TargetParticle);
        TargetTypeTagRequirement = ParseTargetTypeTagReq(rawAbility.TargetTypeTagReq);
        UpgradeOf = rawAbility.UpgradeOf;
        WorksInCombat = rawAbility.WorksInCombat;
        WorksUnderwater = rawAbility.WorksUnderwater;
        WorksWhileFalling = rawAbility.WorksWhileFalling;
        WorksWhileMounted = rawAbility.WorksWhileMounted;
        WorksWhileStunned = rawAbility.WorksWhileStunned;

        // Remove Lint_NotLearnable for an ability that we can actually learn.
        if (InternalName == "SwordSlash" && Keywords is not null)
            Keywords = RemoveKeyword(Keywords, "Lint_NotLearnable");

        // Add new keywords needed for combat parsing.
        if (Skill == "Knife" && DamageType == "Slashing")
            Keywords = AddKeyword(Keywords, "KnifeSlashing");
        if (Skill == "Staff" && DamageType == "Crushing")
            Keywords = AddKeyword(Keywords, "StaffCrushing");

        if (Name is not null && Name.StartsWith("Life Steal") && SpecialInfo is not null && SpecialInfo.StartsWith("Steals "))
        {
            Debug.Assert(PvE is not null && PvE.SpecialValues is null);

            SpecialValue LifeStealSpecialValue = new();
            LifeStealSpecialValue.Label = "Steals";
            LifeStealSpecialValue.Suffix = "Health.";
            string[] Parts = SpecialInfo.Split(' ');
            Debug.Assert(Parts.Length == 3);
            LifeStealSpecialValue.Value = int.Parse(Parts[1]);

            SpecialInfo = null;
            PvE!.SpecialValues = new SpecialValue[] { LifeStealSpecialValue };
        }
    }

    private static string[]? AddKeyword(string[]? Keywords, string keywordToAdd)
    {
        List<string> KeywordsList = Keywords.ToList();
        KeywordsList.Add(keywordToAdd);
        Keywords = KeywordsList.ToArray();

        return Keywords;
    }

    private static string[]? RemoveKeyword(string[]? Keywords, string keywordToRemove)
    {
        List<string> KeywordsList = Keywords.ToList();
        KeywordsList.Remove(keywordToRemove);
        Keywords = KeywordsList.ToArray();

        return Keywords;
    }

    private static ConditionalKeyword[]? ParseConditionalKeywords(RawConditionalKeyword[]? rawConditionalKeywords)
    {
        if (rawConditionalKeywords is null)
            return null;

        List<ConditionalKeyword> Result = new();
        foreach (RawConditionalKeyword ConditionalKeyword in rawConditionalKeywords)
            Result.Add(ParseRawConditionalKeyword(ConditionalKeyword));

        return Result.ToArray();
    }

    private static ConditionalKeyword ParseRawConditionalKeyword(RawConditionalKeyword rawConditionalKeyword)
    {
        ConditionalKeyword Result = new()
        {
            IsDefault = rawConditionalKeyword.Default,
            EffectKeywordMustExist = rawConditionalKeyword.EffectKeywordMustExist,
            EffectKeywordMustNotExist = rawConditionalKeyword.EffectKeywordMustNotExist,
            Keyword = rawConditionalKeyword.Keyword,
        };

        return Result;
    }

    private static string? ParseTargetTypeTagReq(string? rawTargetTypeTagReq)
    {
        string? Result = null;

        if (rawTargetTypeTagReq is not null)
        {
            if (rawTargetTypeTagReq.StartsWith(AnatomyTypeHeader))
                Result = $"{AnatomyHeader}{rawTargetTypeTagReq.Substring(AnatomyTypeHeader.Length)}";
            else
                throw new PreprocessorException();
        }

        return Result;
    }

    private static (string?, string[]?) ParseItemKeywordReqs(string[]? rawItemKeywordReqs)
    {
        if (rawItemKeywordReqs is null)
            return (null, null);

        string FormRequirements = string.Empty;
        List<string> OtherRequirements = new();

        foreach (string ItemKeywordRequirement in rawItemKeywordReqs)
            if (ItemKeywordRequirement.StartsWith(FormHeader))
                if (FormRequirements == string.Empty)
                    FormRequirements = ItemKeywordRequirement.Substring(FormHeader.Length);
                else
                    throw new PreprocessorException();
            else
                OtherRequirements.Add(ItemKeywordRequirement);

        return (FormRequirements != string.Empty ? FormRequirements : null, OtherRequirements.Count > 0 ? OtherRequirements.ToArray() : null);
    }

    private static string? ToDigitStrippedName(string? internalName)
    {
        if (internalName is null)
            return null;

        string Result = internalName;

        // Remove all digits.
        Result = Regex.Replace(Result, @"[\d]", string.Empty);

        // Update names that are subcategories.
        if (IdenticalAbilityNameTable.ContainsKey(Result))
            Result = IdenticalAbilityNameTable[Result];

        // Insert a whitespace before any upper case letter, except the first one.
        Result = Regex.Replace(Result, @"\B[A-Z]", m => " " + m.ToString());

        if (Result == "Poisoners Cut")
            Result = "Poisoner's Cut";
        else if (Result == "Hearts Power")
            Result = "Heart's Power";
        else if (Result == "Spleens Power")
            Result = "Spleen's Power";
        else if (Result == "Deaths Hold")
            Result = "Death's Hold";
        else if (Result == "Hunters Stride")
            Result = "Hunter's Stride";
        else if (Result == "Duelists Slash")
            Result = "Duelist's Slash";
        else if (Result == "Virtuosos Ballad")
            Result = "Virtuoso's Ballad";
        else if (Result == "Rabbits Foot")
            Result = "Rabbit's Foot";
        else if (Result == "Sic Em")
            Result = "Sic 'Em";
        else if (Result == "Summon Minigolem A")
            Result = "Summon Minigolem";

        const string PlayerPrefix = "Player ";
        if (Result.StartsWith(PlayerPrefix))
            Result = Result.Substring(PlayerPrefix.Length);

        return Result;
    }

    private static readonly Dictionary<string, string> IdenticalAbilityNameTable = new Dictionary<string, string>()
    {
        { "StabledPetLiving", "StabledPet" },
        { "TameRat", "TameAnimal" },
        { "TameCat", "TameAnimal" },
        { "TameBear", "TameAnimal" },
        { "TameBee", "TameAnimal" },
        { "BasicShotB", "BasicShot" },
        { "AimedShotB", "AimedShot" },
        { "BlitzShotB", "BlitzShot" },
        { "ToxinBombB", "MycotoxinFormula" },
        { "ToxinBombC", "AcidBomb" },
        { "FireWallB", "FireWall" },
        { "IceVeinsB", "IceVeins" },
        { "SliceB", "DuelistsSlash" },
        { "WerewolfPounceB", "PouncingRend" },
        { "WerewolfPounceBB", "PouncingRend" },
    };

    public string? AbilityGroup { get; set; }
    public string? AbilityGroupName { get; set; }
    public decimal? AmmoConsumeChance { get; set; }
    public string? AmmoDescription { get; set; }
    public Ammo[]? AmmoKeywords { get; set; }
    public decimal? AmmoStickChance { get; set; }
    public string? Animation { get; set; }
    public string? AttributeThatPreventsDelayLoopAbortOnAttacked { get; set; }
    public string[]? AttributesThatDeltaCritChance { get; set; }
    public string[]? AttributesThatDeltaDelayLoopTime { get; set; }
    public string[]? AttributesThatDeltaPowerCost { get; set; }
    public string[]? AttributesThatDeltaResetTime { get; set; }
    public string[]? AttributesThatDeltaWorksWhileStunned { get; set; }
    public string[]? AttributesThatModAmmoConsumeChance { get; set; }
    public string[]? AttributesThatModPowerCost { get; set; }
    public bool? CanBeOnSidebar { get; set; }
    public bool? CanSuppressMonsterShout { get; set; }
    public bool? CanTargetUntargetableEnemies { get; set; }
    public string[]? CausesOfDeath { get; set; }
    public int? CombatRefreshBaseAmount { get; set; }
    public ConditionalKeyword[]? ConditionalKeywords { get; set; }
    public Cost[]? Costs { get; set; }
    public string? DamageType { get; set; }
    public bool? DelayLoopIsAbortedIfAttacked { get; set; }
    public bool? DelayLoopIsOnlyUsedInCombat { get; set; }
    public string? DelayLoopMessage { get; set; }
    public float? DelayLoopTime { get; set; }
    public string? Description { get; set; }
    public string? DigitStrippedName { get; set; }
    public string? EffectKeywordRequirementErrorMessage { get; set; }
    public string[]? EffectKeywordRequirements { get; set; }
    public string[]? EffectKeywordsIndicatingEnabled { get; set; }
    public string[]? ExtraKeywordsForTooltips { get; set; }
    public string? FormRequirement { get; set; }
    public int IconId { get; set; }
    public bool? IgnoreEffectErrors { get; set; }
    public string? InternalName { get; set; }
    public string? InventoryKeywordRequirementErrorMessage { get; set; }
    public string[]? InventoryKeywordRequirements { get; set; }
    public bool? IsAoECenteredOnCaster { get; set; }
    public bool? IsCosmeticPet { get; set; }
    public bool? IsHarmless { get; set; }
    public bool? IsInternalAbility { get; set; }
    public bool? IsTimerResetWhenDisabling { get; set; }
    public string? ItemKeywordRequirementErrorMessage { get; set; }
    public string[]? ItemKeywordRequirements { get; set; }
    public string[]? Keywords { get; set; }
    public int Level { get; set; }
    public string? Name { get; set; }
    public string? PetTypeTagRequirement { get; set; }
    public int? PetTypeTagRequirementMax { get; set; }
    public string? Prerequisite { get; set; }
    public string? Projectile { get; set; }
    public PvEAbility? PvE { get; set; }
    public int? Rank { get; set; }
    public decimal ResetTime { get; set; }
    public AbilityParticle? SelfParticle { get; set; }
    public AbilityParticle? SelfPreParticle { get; set; }
    public string? SharesResetTimerWith { get; set; }
    public string? Skill { get; set; }
    public Requirement[]? SpecialCasterRequirements { get; set; }
    public string? SpecialCasterRequirementsErrorMessage { get; set; }
    public string? SpecialInfo { get; set; }
    public int? SpecialTargetingTypeRequirement { get; set; }
    public string? Target { get; set; }
    public string? TargetEffectKeywordRequirement { get; set; }
    public AbilityParticle? TargetParticle { get; set; }
    public string? TargetTypeTagRequirement { get; set; }
    public string? UpgradeOf { get; set; }
    public bool? WorksInCombat { get; set; }
    public bool? WorksUnderwater { get; set; }
    public bool? WorksWhileFalling { get; set; }
    public bool? WorksWhileMounted { get; set; }
    public bool? WorksWhileStunned { get; set; }

    public RawAbility ToRawAbility()
    {
        RawAbility Result = new();

        Result.AbilityGroup = AbilityGroup;
        Result.AbilityGroupName = AbilityGroupName;
        Result.AmmoConsumeChance = AmmoConsumeChance;
        Result.AmmoDescription = AmmoDescription;
        Result.AmmoKeywords = AmmoKeywords;
        Result.AmmoStickChance = AmmoStickChance;
        Result.Animation = Animation;
        Result.AoEIsCenteredOnCaster = IsAoECenteredOnCaster;
        Result.AttributeThatPreventsDelayLoopAbortOnAttacked = AttributeThatPreventsDelayLoopAbortOnAttacked;
        Result.AttributesThatDeltaCritChance = AttributesThatDeltaCritChance;
        Result.AttributesThatDeltaDelayLoopTime = AttributesThatDeltaDelayLoopTime;
        Result.AttributesThatDeltaPowerCost = AttributesThatDeltaPowerCost;
        Result.AttributesThatDeltaResetTime = AttributesThatDeltaResetTime;
        Result.AttributesThatDeltaWorksWhileStunned = AttributesThatDeltaWorksWhileStunned;
        Result.AttributesThatModAmmoConsumeChance = AttributesThatModAmmoConsumeChance;
        Result.AttributesThatModPowerCost = AttributesThatModPowerCost;
        Result.CanBeOnSidebar = CanBeOnSidebar;
        Result.CanSuppressMonsterShout = CanSuppressMonsterShout;
        Result.CanTargetUntargetableEnemies = CanTargetUntargetableEnemies;
        Result.CausesOfDeath = CausesOfDeath;
        Result.CombatRefreshBaseAmount = CombatRefreshBaseAmount;
        Result.ConditionalKeywords = ToRawConditionalKeywords(ConditionalKeywords);
        Result.Costs = Costs;
        Result.DamageType = DamageType;
        Result.DelayLoopIsAbortedIfAttacked = DelayLoopIsAbortedIfAttacked;
        Result.DelayLoopIsOnlyUsedInCombat = DelayLoopIsOnlyUsedInCombat;
        Result.DelayLoopMessage = DelayLoopMessage;
        Result.DelayLoopTime = DelayLoopTime;
        Result.Description = Description;
        Result.EffectKeywordReqErrorMessage = EffectKeywordRequirementErrorMessage;
        Result.EffectKeywordReqs = EffectKeywordRequirements;
        Result.EffectKeywordsIndicatingEnabled = EffectKeywordsIndicatingEnabled;
        Result.ExtraKeywordsForTooltips = ExtraKeywordsForTooltips;
        Result.IconID = IconId;
        Result.IgnoreEffectErrors = IgnoreEffectErrors;
        Result.InternalAbility = IsInternalAbility;
        Result.InternalName = InternalName;
        Result.InventoryKeywordReqErrorMessage = InventoryKeywordRequirementErrorMessage;
        Result.InventoryKeywordReqs = InventoryKeywordRequirements;
        Result.IsCosmeticPet = IsCosmeticPet;
        Result.IsHarmless = IsHarmless;
        Result.IsTimerResetWhenDisabling = IsTimerResetWhenDisabling;
        Result.ItemKeywordReqErrorMessage = ItemKeywordRequirementErrorMessage;
        Result.ItemKeywordReqs = ToRawItemKeywordReqs(FormRequirement, ItemKeywordRequirements);
        Result.Keywords = Keywords;
        Result.Level = Level;

        Result.Name = IsNameEmpty ? string.Empty : Name;

        Result.PetTypeTagReq = PetTypeTagRequirement;
        Result.PetTypeTagReqMax = PetTypeTagRequirementMax;
        Result.Prerequisite = Prerequisite;

        Result.Projectile = IsProjectileNone ? "0" : Projectile;

        Result.PvE = PvE?.ToRawPvEAbility();
        Result.Rank = Rank is null ? null : Rank.ToString();
        Result.ResetTime = ResetTime;
        Result.SelfParticle = AbilityParticle.ToString(SelfParticle);
        Result.SelfPreParticle = AbilityParticle.ToString(SelfPreParticle);
        Result.SharesResetTimerWith = SharesResetTimerWith;
        Result.Skill = Skill;
        Result.SpecialCasterRequirements = Preprocessor.FromSingleOrMultiple(SpecialCasterRequirements, (Requirement requirement) => requirement.ToRawRequirement(), SpecialCasterRequirementsFormat);
        Result.SpecialCasterRequirementsErrorMessage = SpecialCasterRequirementsErrorMessage;
        Result.SpecialInfo = SpecialInfo;
        Result.SpecialTargetingTypeReq = SpecialTargetingTypeRequirement;
        Result.Target = Target;
        Result.TargetEffectKeywordReq = TargetEffectKeywordRequirement;
        Result.TargetParticle = AbilityParticle.ToString(TargetParticle);
        Result.TargetTypeTagReq = ToRawTargetTypeTagReq(TargetTypeTagRequirement);
        Result.UpgradeOf = UpgradeOf;
        Result.WorksInCombat = WorksInCombat;
        Result.WorksUnderwater = WorksUnderwater;
        Result.WorksWhileFalling = WorksWhileFalling;
        Result.WorksWhileMounted = WorksWhileMounted;
        Result.WorksWhileStunned = WorksWhileStunned;

        if (Result.Keywords is not null)
        {
            if (InternalName == "SwordSlash")
                Result.Keywords = AddKeyword(Result.Keywords, "Lint_NotLearnable");

            if (Skill == "Knife" && DamageType == "Slashing")
                Result.Keywords = RemoveKeyword(Result.Keywords, "KnifeSlashing");
            if (Skill == "Staff" && DamageType == "Crushing")
                Result.Keywords = RemoveKeyword(Result.Keywords, "StaffCrushing");
        }

        if (Result.Name is not null && Result.Name.StartsWith("Life Steal") && Result.PvE?.SpecialValues is not null && Result.PvE.SpecialValues.Length == 1)
        {
            SpecialValue LifeStealSpecialValue = Result.PvE.SpecialValues[0];
            if (LifeStealSpecialValue.Label == "Steals" && LifeStealSpecialValue.Suffix == "Health.")
            {
                Debug.Assert(Result.SpecialInfo is null);

                Result.SpecialInfo = $"{LifeStealSpecialValue.Label} {LifeStealSpecialValue.Value} {LifeStealSpecialValue.Suffix}";
                Result.PvE.SpecialValues = null;
            }
        }

        return Result;
    }

    private static RawConditionalKeyword[]? ToRawConditionalKeywords(ConditionalKeyword[]? conditionalKeywords)
    {
        if (conditionalKeywords == null)
            return null;

        List<RawConditionalKeyword> Result = new List<RawConditionalKeyword>();
        foreach (ConditionalKeyword ConditionalKeyword in conditionalKeywords)
            Result.Add(ToRawConditionalKeyword(ConditionalKeyword));

        return Result.ToArray();
    }

    private static RawConditionalKeyword ToRawConditionalKeyword(ConditionalKeyword conditionalKeyword)
    {
        RawConditionalKeyword Result = new()
        {
            Default = conditionalKeyword.IsDefault,
            EffectKeywordMustExist = conditionalKeyword.EffectKeywordMustExist,
            EffectKeywordMustNotExist = conditionalKeyword.EffectKeywordMustNotExist,
            Keyword = conditionalKeyword.Keyword,
        };

        return Result;
    }

    private static string? ToRawTargetTypeTagReq(string? targetTypeTagRequirement)
    {
        string? Result = null;

        if (targetTypeTagRequirement is not null)
        {
            Debug.Assert(targetTypeTagRequirement.StartsWith(AnatomyHeader));

            Result = $"{AnatomyTypeHeader}{targetTypeTagRequirement.Substring(AnatomyHeader.Length)}";
        }

        return Result;
    }

    private static string[]? ToRawItemKeywordReqs(string? formRequirement, string[]? itemKeywordRequirements)
    {
        if (formRequirement is null && itemKeywordRequirements is null)
            return null;

        List<string> Result = new();

        if (formRequirement is not null)
            Result.Add($"{FormHeader}{formRequirement}");

        if (itemKeywordRequirements is not null)
            foreach (string ItemKeywordRequirement in itemKeywordRequirements)
                Result.Add(ItemKeywordRequirement);

        return Result.ToArray();
    }

    private readonly JsonArrayFormat SpecialCasterRequirementsFormat;
    private readonly bool IsProjectileNone;
    private readonly bool IsNameEmpty;
}
