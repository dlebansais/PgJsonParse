namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    private static List<SentenceEx> SentenceList = new List<SentenceEx>()
    {
        new SentenceEx("Heal you for %f Health/Armor", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Health/Armor healing", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Heal your pet for %f Health/Armor", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health/Armor to your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health/Armor to your undead", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health/Armor to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Health/Armor", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Restore %f health (or Armor)", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Restore %f health or Armor", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Heal you for %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Heal you for %f", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Health to you and nearby allies", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelfAndAllies }),
        new SentenceEx("Restore %f Health to your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health to both you and your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelfAndPet }),
        new SentenceEx("Restore %f Health to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("You regain %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Boost the healing of your @ %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("You regenerate %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Recover %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Restoration %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Heal %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Healing %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Heal you %f", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Heal %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Recover %f Armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("Heal %f armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("Restore %f armor to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("And %f armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("Recover %f Armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("And %f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Power to you and nearby allies", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelfAndAllies }),
        new SentenceEx("Restore %f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("%f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("You recover %f power", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Recover %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("Power Restoration %f", CombatKeywordEx.RestorePower),
        new SentenceEx("You regain %f power", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("And %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("Restore %f", CombatKeywordEx.RestoreHealth),

        new SentenceEx("Increase your Max Health by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Increase your Max Health %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Max Health %f", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Max Health is %f", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Max Health by %f", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("%f Max Health", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Have %f health", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Max Armor %f", CombatKeywordEx.IncreaseMaxArmor),
        new SentenceEx("Increase your Max Armor by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Max Armor", CombatKeywordEx.IncreaseMaxArmor),
        new SentenceEx("Have %f Armor", CombatKeywordEx.IncreaseMaxArmor),
        new SentenceEx("Increase your Max Power by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxPower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Max Power %f", CombatKeywordEx.IncreaseMaxPower),
        new SentenceEx("Grant the target %f Max Power", CombatKeywordEx.IncreaseMaxPower),

        new SentenceEx("%f chance to", CombatKeywordEx.ApplyWithChance),

        new SentenceEx("Cause your", CombatKeywordEx.ApplyToSelf),

        new SentenceEx("After a %f second delay", CombatKeywordEx.EffectDelay),
        new SentenceEx("After an %f second delay", CombatKeywordEx.EffectDelay),

        new SentenceEx("For %f second", CombatKeywordEx.EffectDuration),

        new SentenceEx("Your Sprint Speed increase by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Sprint Speed increase by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Sprint Speed is increased %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("%f Sprint Speed", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Boost your movement speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost your movement speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost movement speed by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Increase your movement speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost your sprint speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost your out of combat sprint speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Increase your out of combat sprint speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Speed is %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Movement speed %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Sprint speed %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Sprint speed by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("%f Movement Speed", CombatKeywordEx.AddSprintSpeed),

        new SentenceEx("Your next use", CombatKeywordEx.NextUse),

        new SentenceEx("Armor Recovery Per Second: %f of Max Armor", CombatKeywordEx.RegenPercentageOfArmor),
        new SentenceEx("They recover Armor equal to %f of their Max Armor", CombatKeywordEx.RegenPercentageOfArmor),
        new SentenceEx("Healing abilities, if any, restore %f health", CombatKeywordEx.IncreaseHealEfficiency),

        new SentenceEx("Within %f meter", CombatKeywordEx.TargetRange),

        new SentenceEx("To you and your allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("Affects caster as well as allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("To all allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("All allies gain", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("Grant all allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("And your allies' attack", CombatKeywordEx.ApplyToAllies),

        new SentenceEx("When wielding two knives", CombatKeywordEx.RequireTwoKnives),

        new SentenceEx("Over %f second", CombatKeywordEx.EffecOverTime),

        new SentenceEx("If the target is not focused on you", CombatKeywordEx.RequireNoAggro),
        new SentenceEx("If they are not focused on you", CombatKeywordEx.RequireNoAggro),
        new SentenceEx("If target is not focused on you", CombatKeywordEx.RequireNoAggro),

        new SentenceEx("While in @ form", CombatKeywordEx.RequireSpecialForm),
        new SentenceEx("While playing", CombatKeywordEx.RequirePlayingSong),
    };
}
