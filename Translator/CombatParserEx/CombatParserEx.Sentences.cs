namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    private static List<SentenceEx> SentenceList = new List<SentenceEx>()
    {
        new SentenceEx("%f Body Heat", CombatKeywordEx.RestoreBodyHeat),

        new SentenceEx("Heal you for %f Health/Armor", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Health/Armor healing", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Heal your pet for %f Health/Armor", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health/Armor to your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health/Armor to your undead", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health/Armor to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Health/Armor", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Restore %f health (or Armor)", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Restore %f health or Armor", CombatKeywordEx.RestoreHealthOrArmor),
        new SentenceEx("Restore %f health and Armor", CombatKeywordEx.RestoreHealthAndArmor),
        new SentenceEx("Recover %f health and Armor", CombatKeywordEx.RestoreHealthAndArmor),
        new SentenceEx("Heal you for %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Heal all targets for %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Heal you for %f", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Health to you and nearby allies", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelfAndAllies }),
        new SentenceEx("Restore %f Health to your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health to both you and your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelfAndPet }),
        new SentenceEx("Restore %f Health to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("You regain %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Boost the healing of your @ %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Boost your @ %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("You regenerate %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Recover %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Restoration %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("You heal %f health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Heal %f health", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Healing %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Heal you %f", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Recover %f Armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("Heal %f armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("Restore %f Armor to your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreArmor, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Armor to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("And %f Armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("Recover %f Armor", CombatKeywordEx.RestoreArmor),
        new SentenceEx("And %f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Power to you and nearby allies", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelfAndAllies }),
        new SentenceEx("Restore %f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("%f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("You recover %f power", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Recover %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("Regain %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("Power Restoration %f", CombatKeywordEx.RestorePower),
        new SentenceEx("You regain %f power", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("And %f power", CombatKeywordEx.RestorePower),
        new SentenceEx("Restore %f", CombatKeywordEx.RestoreHealth),
        new SentenceEx("Heal %f", CombatKeywordEx.RestoreHealth),

        new SentenceEx("Increase your Max Health by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Increase your Max Health %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Max Health %f", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Max Health is %f", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Max Health by %f", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("%f Max Health", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Have %f health", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("%f Pet Max Health", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxHealth, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Gain %f Health", CombatKeywordEx.IncreaseMaxHealth),
        new SentenceEx("Max Armor %f", CombatKeywordEx.IncreaseMaxArmor),
        new SentenceEx("Increase your Max Armor by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Max Armor", CombatKeywordEx.IncreaseMaxArmor),
        new SentenceEx("Have %f Armor", CombatKeywordEx.IncreaseMaxArmor),
        new SentenceEx("Increase your Max Power by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMaxPower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Max Power %f", CombatKeywordEx.IncreaseMaxPower),
        new SentenceEx("Grant the target %f Max Power", CombatKeywordEx.IncreaseMaxPower),

        new SentenceEx("After a %f second delay", CombatKeywordEx.EffectDelay),
        new SentenceEx("After an %f second delay", CombatKeywordEx.EffectDelay),
        new SentenceEx("After %f second", CombatKeywordEx.EffectDelay),

        new SentenceEx("For %f second", CombatKeywordEx.EffectDuration),
        new SentenceEx("For %f minute", CombatKeywordEx.EffectDurationInMinutes),
        new SentenceEx("Within %f second", CombatKeywordEx.EffectDuration),
        new SentenceEx("Lasts %f second", CombatKeywordEx.EffectDuration),

        new SentenceEx("Every %f second", CombatKeywordEx.RecurringEffect),

        new SentenceEx("Your Sprint Speed increase by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Sprint Speed increase by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Sprint Speed is increased %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("%f Sprint Speed", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Boost your movement speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost your movement speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost movement speed by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Increase your movement speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost your sprint speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Increase your sprint speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost your out of combat sprint speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Increase your out of combat sprint speed %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Speed is %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Movement speed %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Sprint speed %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Sprint speed by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("%f Movement Speed", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Move much faster", CombatKeywordEx.AddSprintSpeed),

        new SentenceEx("Armor Recovery Per Second: %f of Max Armor", CombatKeywordEx.RegenPercentageOfArmor),
        new SentenceEx("They recover Armor equal to %f of their Max Armor", CombatKeywordEx.RegenPercentageOfArmor),
        new SentenceEx("Healing abilities, if any, restore %f health", CombatKeywordEx.IncreaseHealEfficiency),

        new SentenceEx("Hit all enemies", CombatKeywordEx.BecomeBurst),
        new SentenceEx("Targets all enemies", CombatKeywordEx.BecomeBurst),
        new SentenceEx("Hit all target", CombatKeywordEx.BecomeBurst),
        new SentenceEx("Become a %fm Burst", new List<CombatKeywordEx>() { CombatKeywordEx.TargetRange, CombatKeywordEx.BecomeBurst }),

        new SentenceEx("Within %f meter", CombatKeywordEx.TargetRange),

        new SentenceEx("You and your allies", CombatKeywordEx.ApplyToSelfAndAllies),
        new SentenceEx("You and nearby allies", CombatKeywordEx.ApplyToSelfAndAllies),
        new SentenceEx("Yourself and all allies", CombatKeywordEx.ApplyToSelfAndAllies),
        new SentenceEx("Affects caster as well as allies", CombatKeywordEx.ApplyToSelfAndAllies),
        new SentenceEx("To all allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("All allies gain", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("Grant all allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("And your allies' attack", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("All allies", CombatKeywordEx.ApplyToAllies),
        new SentenceEx("Effects on allies", CombatKeywordEx.ApplyToAllies),

        new SentenceEx("When wielding two knives", CombatKeywordEx.RequireTwoKnives),

        new SentenceEx("React to incoming Melee with an eruption of vile blood: a Burst #D attack with Base Damage %f", new List<CombatKeywordEx>() { CombatKeywordEx.VileBloodAttack, CombatKeywordEx.OnIncomingMeleeAttack }),

        new SentenceEx("Over %f second", CombatKeywordEx.EffectOverTime),
        new SentenceEx("Per second", CombatKeywordEx.EffectEverySecond),

        new SentenceEx("If the target is not focused on you", CombatKeywordEx.RequireNoAggro),
        new SentenceEx("If they are not focused on you", CombatKeywordEx.RequireNoAggro),
        new SentenceEx("If target is not focused on you", CombatKeywordEx.RequireNoAggro),
        new SentenceEx("To targets not focused on you", CombatKeywordEx.RequireNoAggro),
        new SentenceEx("To targets that are not focused on you", CombatKeywordEx.RequireNoAggro),

        new SentenceEx("While in Blood-Mist form", CombatKeywordEx.RequireBloodMistForm),
        new SentenceEx("While playing", CombatKeywordEx.RequirePlayingSong),

        new SentenceEx("To the kicker", CombatKeywordEx.ToKickerTarget),
        new SentenceEx("Cause kicks", CombatKeywordEx.ToKickerTarget),

        new SentenceEx("Reuse Time %f second", CombatKeywordEx.IncreaseCurrentRefreshTime),
        new SentenceEx("Reuse Time is %f second faster", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reuse Time is %f second sooner", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reuse Time is %f second", CombatKeywordEx.IncreaseCurrentRefreshTime),
        new SentenceEx("Reuse Time is %f sec", CombatKeywordEx.IncreaseCurrentRefreshTime),
        new SentenceEx("Reuse time is increased %f second", CombatKeywordEx.IncreaseCurrentRefreshTime),
        new SentenceEx("Reuse time of @ is hastened by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reuse time on @ is hastened %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("@ reuse time is hastened %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Hasten current reuse time of @ by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Hasten the current reuse time of @ by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Hasten the current reuse time of @ %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Hasten the current reset time of @ by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Hasten the reuse time of @ %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Shorten the remaining reset time of @ by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Shorten by %f second the remaining reset time of @", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Shorten the current reuse time of @ by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Hasten the active reset time of @ by %f second", CombatKeywordEx.IncreaseCurrentRefreshTime, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reset time of @ is increased %f second", CombatKeywordEx.IncreaseCurrentRefreshTime),
        new SentenceEx("Reset time %f", CombatKeywordEx.IncreaseCurrentRefreshTime),

        new SentenceEx("Hasten your current Combat Refresh delay by %f second", CombatKeywordEx.IncreaseCombatRefreshTime, SignInterpretation.AlwaysNegative),

        new SentenceEx("Complete stun immunity", CombatKeywordEx.StunImmunity),
        new SentenceEx("Grant immunity to new stun", CombatKeywordEx.StunImmunity),
        new SentenceEx("Grant them immunity to stun", CombatKeywordEx.StunImmunity),

        new SentenceEx("Cause your pet to bleed for %f #D damage", new List<CombatKeywordEx>() { CombatKeywordEx.SelfDamage, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("The pet take %f #D damage", new List<CombatKeywordEx>() { CombatKeywordEx.SelfDamage, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Deal %f #D damage to YOU", CombatKeywordEx.SelfDamage),

        new SentenceEx("Both you and your pet", CombatKeywordEx.ApplyToSelfAndPet),

        new SentenceEx("Your pet's", CombatKeywordEx.ApplyToPet),
        new SentenceEx("Your pet gain", CombatKeywordEx.ApplyToPet),
        new SentenceEx("Give your pet", CombatKeywordEx.ApplyToPet),
        new SentenceEx("Grant your pet", CombatKeywordEx.ApplyToPet),
        new SentenceEx("To your pet", CombatKeywordEx.ApplyToPet),
        new SentenceEx("To your undead", CombatKeywordEx.ApplyToPet),

        new SentenceEx("Boost target pet's", CombatKeywordEx.ApplyToPetOfTarget),
        new SentenceEx("Grant target pet", CombatKeywordEx.ApplyToPetOfTarget),
        new SentenceEx("Increase target pet's", CombatKeywordEx.ApplyToPetOfTarget),

        new SentenceEx("Kill an enemy via direct damage", CombatKeywordEx.RequireDirectDamageKillShot),

        new SentenceEx("Targets are Knock back", CombatKeywordEx.Knockback),
        new SentenceEx("Knock back targets", CombatKeywordEx.Knockback),
        new SentenceEx("Knock all targets back", CombatKeywordEx.Knockback),
        new SentenceEx("Knock them back", CombatKeywordEx.Knockback),
        new SentenceEx("Knock the target backward", CombatKeywordEx.Knockback),
        new SentenceEx("Knock the enemy backward", CombatKeywordEx.Knockback),
        new SentenceEx("Knock the target back", CombatKeywordEx.Knockback),
        new SentenceEx("Knock target backward", CombatKeywordEx.Knockback),
        new SentenceEx("Knock targets backward", CombatKeywordEx.Knockback),

        new SentenceEx("Have less than %f of their Max Rage", CombatKeywordEx.RequireLowRage),
        new SentenceEx("Has less than %f of their Max Rage", CombatKeywordEx.RequireLowRage),
        new SentenceEx("With less than %f of their Max Rage", CombatKeywordEx.RequireLowRage),

        new SentenceEx("Whose Rage meter are at least %f full", CombatKeywordEx.RequireHighRage),
        new SentenceEx("Whose Rage meter is at least %f full", CombatKeywordEx.RequireHighRage),
        new SentenceEx("If target's Rage is at least %f full", CombatKeywordEx.RequireHighRage),
        new SentenceEx("If target's Rage meter is at least %f full", CombatKeywordEx.RequireHighRage),

        new SentenceEx("Rage attack deal %f", CombatKeywordEx.RageAttackBoost),
        new SentenceEx("Rage attack to deal %f", CombatKeywordEx.RageAttackBoost),
        new SentenceEx("Pet's Rage Attack Damage %f", CombatKeywordEx.RageAttackBoost),
        new SentenceEx("Rage Attack Damage %f", CombatKeywordEx.RageAttackBoost),
        new SentenceEx("Rage attack are further reduced by %f", CombatKeywordEx.RageAttackBoost, SignInterpretation.AlwaysNegative),

        new SentenceEx("Deal %f damage to Health and Armor", CombatKeywordEx.DealHealthAndArmorDamage),
        new SentenceEx("Dealing %f Health and Armor damage", CombatKeywordEx.DealHealthAndArmorDamage),

        new SentenceEx("Deal %f Armor damage", CombatKeywordEx.DealArmorDamage),
        new SentenceEx("Deal %f damage to Armor", CombatKeywordEx.DealArmorDamage),
        new SentenceEx("%f damage to Armor", CombatKeywordEx.DealArmorDamage),
        new SentenceEx("%f armor damage", CombatKeywordEx.DealArmorDamage),

        new SentenceEx("Target's Critical Hit Chance reduced by %f", CombatKeywordEx.IncreaseCriticalChance, SignInterpretation.Opposite),
        new SentenceEx("Chance to critically-hit is reduced by %f", CombatKeywordEx.IncreaseCriticalChance, SignInterpretation.Opposite),
        new SentenceEx("%f chance to crit", CombatKeywordEx.IncreaseCriticalChance),
        new SentenceEx("%f Critical Hit Chance", CombatKeywordEx.IncreaseCriticalChance),
        new SentenceEx("Critical Hit Chance %f", CombatKeywordEx.IncreaseCriticalChance),
        new SentenceEx("%f Crit Chance", CombatKeywordEx.IncreaseCriticalChance),
        new SentenceEx("Deal %f damage when they critically hit", CombatKeywordEx.IncreaseCriticalChance),
        new SentenceEx("Boost your Anatomy Critical Hit Chance %f", CombatKeywordEx.IncreaseCriticalChance),

        new SentenceEx("Damage-over-time effects deal %f damage per tick", CombatKeywordEx.DamageOverTimeBoost),

        new SentenceEx("Boost #S Skill Base Damage %f", CombatKeywordEx.BaseDamageBoost),
        new SentenceEx("Gain %f #S Skill Base Damage", CombatKeywordEx.BaseDamageBoost),
        new SentenceEx("Your #S Base Damage by %f", CombatKeywordEx.BaseDamageBoost),
        new SentenceEx("Your #S Base Damage increase %f", CombatKeywordEx.BaseDamageBoost),
        new SentenceEx("#S Base Damage %f", CombatKeywordEx.BaseDamageBoost),

        new SentenceEx("Boost the damage of @ by %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost }),
        new SentenceEx("Boost the #D damage of @ and @ %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost }),
        new SentenceEx("Increase the damage of all targets' attack %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToSelfAndAllies }),
        new SentenceEx("%f damage from all attack", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToSelfAndAllies }),
        new SentenceEx("#D attack Deal %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal up to %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Add up to %f extra damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal %f immediate #D damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal %f #D damage to health ", CombatKeywordEx.DealHealthDamage),
        new SentenceEx("Deal %f #D damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal direct #D damage to deal %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal %f damage if it is a #D attack", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal %f less damage", CombatKeywordEx.DamageBoost, SignInterpretation.AlwaysNegative),
        new SentenceEx("Deal %f more damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Plus %f more damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost your #S damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost your @ damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost your #D attack damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost #D attack damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost #D attack %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost the damage of your @ by %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost the damage of your @ %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost the damage from @ %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost the damage of all your attack %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost your #D damage %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost #D damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost your next attack %f if it is a #D attack", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Boost your next attack %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Boost your Indirect #D Damage %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToIndirect }),
        new SentenceEx("Future @ attack damage %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.NextAttack }),
        new SentenceEx("Boost the damage of future @ attack by %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.NextAttack }),
        new SentenceEx("Increase the damage of your next attack by %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Reduce the damage of the target's next attack by %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.NextAttack }, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce targets' next attack by %f", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.NextAttack }, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce their attack damage %f", CombatKeywordEx.DamageBoost, SignInterpretation.AlwaysNegative),
        new SentenceEx("Increase the damage of your @ by %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost your direct and indirect #D damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Direct and Indirect #D Damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Damage is boosted %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Boost the damage of @ %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("All attack deal %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Dealing %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Cause %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Your #D attack deal %f direct damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Deal a further %f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Buff targets' direct #D damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("%f #D damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("#D damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("%f damage", CombatKeywordEx.DamageBoost),
        new SentenceEx("Damage %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("Damage is %f", CombatKeywordEx.DamageBoost),
        new SentenceEx("#S damage %f", CombatKeywordEx.DamageBoost),

        new SentenceEx("Deal %f Direct health damage", CombatKeywordEx.DealHealthDamage),
        new SentenceEx("%f Direct health damage", CombatKeywordEx.DealHealthDamage),
        new SentenceEx("Deal %f health damage to you", new List<CombatKeywordEx>() { CombatKeywordEx.DealHealthDamage, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f health damage", CombatKeywordEx.DealHealthDamage),
        new SentenceEx("%f #D health damage", CombatKeywordEx.DealHealthDamage),

        new SentenceEx("%f Direct damage mitigation based on remaining Armor", CombatKeywordEx.AddArmorBasedMitigation),
        new SentenceEx("Absorb some direct damage based on their remaining Armor (absorbing zero when armor is empty, up to %f when armor is full", CombatKeywordEx.AddArmorBasedMitigation),

        new SentenceEx("%f Direct damage mitigation", CombatKeywordEx.AddMitigationDirect),
        new SentenceEx("Gain %f Direct Elite Vulnerability", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.RequireEliteTarget }, SignInterpretation.Opposite),
        new SentenceEx("Suffer %f damage from direct #D attack", CombatKeywordEx.AddMitigationDirect, SignInterpretation.AlwaysNegative),

        new SentenceEx("Deal %f direct damage", CombatKeywordEx.DirectDamageBoost),
        new SentenceEx("Gain %f Direct", CombatKeywordEx.DirectDamageBoost),
        new SentenceEx("Deal %f Direct", CombatKeywordEx.DirectDamageBoost),
        new SentenceEx("Direct #D Damage %f", CombatKeywordEx.DirectDamageBoost),
        new SentenceEx("%f Direct Damage", CombatKeywordEx.DirectDamageBoost),
        new SentenceEx("Direct Damage %f", CombatKeywordEx.DirectDamageBoost),

        new SentenceEx("Dispel any roots or slow you are currently suffering", new List<CombatKeywordEx>() { CombatKeywordEx.DispelRootSlow, CombatKeywordEx.ApplyToSelf }),

        new SentenceEx("Power cost to sprint in combat is reduced %f", CombatKeywordEx.AddSprintPowerCost),

        new SentenceEx("If it kills your target", CombatKeywordEx.RequireKillTarget),
        new SentenceEx("Kills its target", CombatKeywordEx.RequireKillTarget),
        new SentenceEx("If slain", CombatKeywordEx.RequireKillTarget),

        new SentenceEx("%f of target's attack miss and have no effect", CombatKeywordEx.IncreaseAccuracy, SignInterpretation.Opposite),
        new SentenceEx("%f of their attack miss and have no effect", CombatKeywordEx.IncreaseAccuracy, SignInterpretation.Opposite),
        new SentenceEx("Grant %f Accuracy to your next #S ability", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseAccuracy, CombatKeywordEx.NextUse, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Give you %f Accuracy", CombatKeywordEx.IncreaseAccuracy),
        new SentenceEx("Accuracy %f", CombatKeywordEx.IncreaseAccuracy),
        new SentenceEx("%f Accuracy", CombatKeywordEx.IncreaseAccuracy),
        new SentenceEx("%f more chance of missing", CombatKeywordEx.IncreaseAccuracy, SignInterpretation.Opposite),
        new SentenceEx("%f Miss Chance", CombatKeywordEx.IncreaseAccuracy, SignInterpretation.Opposite),
        new SentenceEx("Melee Accuracy %f", CombatKeywordEx.AddMeleeAccuracy),
        new SentenceEx("You gain Accuracy %f with melee", CombatKeywordEx.AddMeleeAccuracy),
        new SentenceEx("Target has a %f chance to Miss with any Melee", CombatKeywordEx.AddMeleeAccuracy, SignInterpretation.Opposite),

        new SentenceEx("Burst Accuracy %f", CombatKeywordEx.IncreaseBurstAccuracy),

        new SentenceEx("Power Cost %f", CombatKeywordEx.IncreasePowerCost),
        new SentenceEx("Power Cost is %f", CombatKeywordEx.IncreasePowerCost),
        new SentenceEx("Reduce the Power cost of your @ %f", CombatKeywordEx.IncreasePowerCost, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce the Power cost of your next @ by %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreasePowerCost, CombatKeywordEx.NextUse, CombatKeywordEx.ApplyToSelf }, SignInterpretation.AlwaysNegative),
        new SentenceEx("Further reduce ability cost %f", CombatKeywordEx.IncreasePowerCost, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce the Power cost of @ by %f", CombatKeywordEx.IncreasePowerCost, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce the Power cost of @ %f", CombatKeywordEx.IncreasePowerCost, SignInterpretation.AlwaysNegative),
        new SentenceEx("Cost %f Power", CombatKeywordEx.IncreasePowerCost),

        new SentenceEx("Cause all allies' Melee to cost %f Power", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseMeleePowerCost, CombatKeywordEx.ApplyToAllies }),
        new SentenceEx("Reduce the Power cost of melee by %f", CombatKeywordEx.IncreaseMeleePowerCost, SignInterpretation.AlwaysNegative),

        new SentenceEx("Reset the time on", CombatKeywordEx.ResetRefreshTime),

        new SentenceEx("Target does not yell for help because of this attack", CombatKeywordEx.NoYellForHelp),
        new SentenceEx("Doesn't cause the target to yell for help", CombatKeywordEx.NoYellForHelp),
        new SentenceEx("This attack does not cause the target to shout for help", CombatKeywordEx.NoYellForHelp),
        new SentenceEx("Does not cause the target to shout for help", CombatKeywordEx.NoYellForHelp),
        new SentenceEx("Does not call for help", CombatKeywordEx.NoYellForHelp),
        new SentenceEx("Do not call for help", CombatKeywordEx.NoYellForHelp),

        new SentenceEx("Mend a broken bone", CombatKeywordEx.RepairBrokenBone),
        new SentenceEx("Randomly repair broken bones over time", CombatKeywordEx.RepairBrokenBoneOverTime),

        new SentenceEx("Every other", CombatKeywordEx.EveryOtherUse),

        new SentenceEx("To Vulnerable target", CombatKeywordEx.RequireVulnerableTarget),
        new SentenceEx("If the target is Vulnerable", CombatKeywordEx.RequireVulnerableTarget),
        new SentenceEx("If target is Vulnerable", CombatKeywordEx.RequireVulnerableTarget),
        new SentenceEx("Is used on a Vulnerable target", CombatKeywordEx.RequireVulnerableTarget),

        new SentenceEx("If it is an Attack ability", CombatKeywordEx.RequireAttackAbility),

        new SentenceEx("To cost 0 Power", CombatKeywordEx.ZeroPowerCost),

        new SentenceEx("Conjures a magical field on the target", CombatKeywordEx.BestowProtectiveBubble),
        new SentenceEx("Covers the target in a barrier that mitigate %f damage from #D attack", CombatKeywordEx.BestowProtectiveBubble),
        new SentenceEx("Conjures a magical field around each target that mitigate %f of all physical damage they take", CombatKeywordEx.BestowProtectiveBubble),
        new SentenceEx("Conjures a magical field that mitigate %f of all physical damage you take", new List<CombatKeywordEx>() { CombatKeywordEx.BestowProtectiveBubble, CombatKeywordEx.ApplyToSelf}),

        new SentenceEx("Slowdown Cancelled", CombatKeywordEx.CancelSlowdown),
        new SentenceEx("has no slowdown effect", CombatKeywordEx.CancelSlowdown),

        new SentenceEx("Knockback Ignore Chance Ignored", CombatKeywordEx.NullifyIgnoreKnockback),
        new SentenceEx("any Knockback Ingore Chance is nullified", CombatKeywordEx.NullifyIgnoreKnockback),

        new SentenceEx("Reduce the taunt of all your attack by %f", CombatKeywordEx.GenerateTaunt, SignInterpretation.Opposite),
        new SentenceEx("Taunt %f", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("Taunted %f", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("Taunt as if they did %f more damage", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("Taunt as if they did %f damage", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("Taunt their opponents %f less", CombatKeywordEx.GenerateTaunt, SignInterpretation.Opposite),
        new SentenceEx("Taunt of all your attack %f", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("Your Taunt is %f", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("%f Taunt", CombatKeywordEx.GenerateTaunt),
        new SentenceEx("Lower target's aggro toward you by %f", CombatKeywordEx.GenerateTaunt, SignInterpretation.Opposite),

        new SentenceEx("Target's next attack has a %f chance to automatically miss", CombatKeywordEx.NextAttackMiss),
        new SentenceEx("Target's next attack to have a %f chance to automatically Miss", CombatKeywordEx.NextAttackMiss),

        new SentenceEx("You mitigate %f from all attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Grant %f Universal #D Mitigation", CombatKeywordEx.AddMitigation),
        new SentenceEx("Universal Damage Mitigation %f", CombatKeywordEx.AddMitigation),
        new SentenceEx("Grant targets %f Universal Damage Resistance", CombatKeywordEx.AddMitigation),
        new SentenceEx("Reduce the damage you take from #D attack by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Reduce the damage of the next attack that hit the target by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.NextAttack }),
        new SentenceEx("(#D) attack that hit you are reduced by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Take %f less damage from all attack", CombatKeywordEx.AddMitigation),
        new SentenceEx("Target take %f less damage from attack", CombatKeywordEx.AddMitigation),
        new SentenceEx("Target take %f less damage from #D attack", CombatKeywordEx.AddMitigation),
        new SentenceEx("Target to take %f less damage from attack", CombatKeywordEx.AddMitigation),
        new SentenceEx("Target to take %f less damage from #D attack", CombatKeywordEx.AddMitigation),
        new SentenceEx("Mitigate %f of all #D damage", CombatKeywordEx.AddMitigation),
        new SentenceEx("Mitigate %f damage from #D attacks", CombatKeywordEx.AddMitigation),
        new SentenceEx("Grant you %f mitigation vs #D", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("You gain %f mitigation vs #D", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Mitigation vs #D", CombatKeywordEx.AddMitigation),
        new SentenceEx("Increase your mitigation vs #D attack %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Further debuff their mitigation %f", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Grant you %f mitigation against all attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Mitigation from attack", CombatKeywordEx.AddMitigation),
        new SentenceEx("%f Mitigation from #D damage", CombatKeywordEx.AddMitigation),
        new SentenceEx("#D mitigation %f", CombatKeywordEx.AddMitigation),
        new SentenceEx("Mitigate %f #D Damage", CombatKeywordEx.AddMitigation),
        new SentenceEx("Your Direct and Indirect #D mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Direct and Indirect #D mitigation %f", CombatKeywordEx.AddMitigation),
        new SentenceEx("%f #D mitigation", CombatKeywordEx.AddMitigation),
        new SentenceEx("Increase your #D Mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Damage Mitigation", CombatKeywordEx.AddMitigation),
        new SentenceEx("Elite #D mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Take %f damage from elite attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }, SignInterpretation.Opposite),
        new SentenceEx("#D Mitigation vs Elites %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Mitigation vs Elite attack %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Resist %f damage from Elite attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("A further %f from Elite attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Mitigate %f more against Elite attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Mitigation vs all attack by Elites %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Against Elite enemies, mitigate %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Mitigate %f of all Elite attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("%f Mitigation from all Elite attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("All Elite attack deal %f damage to you.", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.RequireEliteTarget }, SignInterpretation.Opposite),
        new SentenceEx("Grant you %f #D Vulnerability", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf}, SignInterpretation.Opposite),
        new SentenceEx("%f #D Vulnerability", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("Debuff the target so that it take %f damage from future #D attack", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("Targets suffer %f damage from other #D attack", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("Increase target's vulnerability to future #D attack %f", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("Cause the targets to suffer %f damage from future #D attack (non-stacking)", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.GiveBuffEachAttack }, SignInterpretation.Opposite),
        new SentenceEx("Cause target to suffer %f damage from future attack", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("%f resistance to #D damage", CombatKeywordEx.AddMitigation),
        new SentenceEx("#D resistance %f", CombatKeywordEx.AddMitigation),
        new SentenceEx("%f resistant to #D damage", CombatKeywordEx.AddMitigation),

        new SentenceEx("Cause the target to take %f more damage from #D", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Target is %f more vulnerable to #D damage", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Target %f more vulnerable to #D damage", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Make the target %f more vulnerable to #D", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Cause the target to become %f more vulnerable to #D attack", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Target take %f more damage from #D", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Target take %f #D damage", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("You take %f damage from #D attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigation, CombatKeywordEx.ApplyToSelf }, SignInterpretation.Opposite),
        new SentenceEx("Take %f damage from #D attack", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("Take %f more damage from #D attack", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Indirect #D Vulnerability %f", CombatKeywordEx.AddMitigationIndirect, SignInterpretation.AlwaysNegative),
        new SentenceEx("#D Vulnerability %f", CombatKeywordEx.AddMitigation, SignInterpretation.Opposite),
        new SentenceEx("Target's #D Vulnerability %f", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Target's #D Vulnerability", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Target's vulnerability to #D %f", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Suffer %f damage from all Indirect Damage sources", CombatKeywordEx.AddMitigationIndirect, SignInterpretation.AlwaysNegative),
        new SentenceEx("Indirect Vulnerability %f", CombatKeywordEx.AddMitigationIndirect, SignInterpretation.AlwaysNegative),
        new SentenceEx("Targets suffer %f damage from other #D attack", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Cause the target to suffer %f #D damage", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Cause target to be %f more vulnerable to #D damage", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),
        new SentenceEx("Take %f damage from #D", CombatKeywordEx.AddMitigation, SignInterpretation.AlwaysNegative),

        new SentenceEx("Make the target %f more vulnerable to direct #D damage", CombatKeywordEx.AddMitigationDirect, SignInterpretation.AlwaysNegative),
        new SentenceEx("Cause the target to take %f damage from direct #D attack", CombatKeywordEx.AddMitigationDirect, SignInterpretation.Opposite),
        new SentenceEx("Gain %f Direct Elite Vulnerability", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.RequireEliteTarget }, SignInterpretation.Opposite),
        new SentenceEx("%f Vulnerability to Elite Direct Damage", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.RequireEliteTarget }, SignInterpretation.Opposite),
        new SentenceEx("Universal Direct Mitigation %f", CombatKeywordEx.AddMitigationDirect),
        new SentenceEx("Universal Direct Elite Mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Universal Elite Direct Mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Give you %f mitigation from direct attack", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f mitigation vs direct attack", CombatKeywordEx.AddMitigationDirect),
        new SentenceEx("Direct #D mitigation %f", CombatKeywordEx.AddMitigationDirect),
        new SentenceEx("%f mitigation from Direct #D damage", CombatKeywordEx.AddMitigationDirect),
        new SentenceEx("%f direct damage mitigation", CombatKeywordEx.AddMitigationDirect),
        new SentenceEx("Boost your direct damage mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationDirect, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Direct Mitigation", CombatKeywordEx.AddMitigationDirect),

        new SentenceEx("Suffer %f damage from Burst", CombatKeywordEx.AddMitigationBurst, SignInterpretation.Opposite),

        new SentenceEx("Vs Elite", CombatKeywordEx.RequireEliteTarget),
        new SentenceEx("If target is Elite or Boss", CombatKeywordEx.RequireEliteTarget),
        new SentenceEx("If target is Elite", CombatKeywordEx.RequireEliteTarget),
        new SentenceEx("To Elite targets", CombatKeywordEx.RequireEliteTarget),

        new SentenceEx("Chance to consume grass is %f", CombatKeywordEx.ChanceToConsume),
        new SentenceEx("Chance to consume carrot is %f", CombatKeywordEx.ChanceToConsume),

        new SentenceEx("Stun you", CombatKeywordEx.SelfStun),

        new SentenceEx("Until %f damage is mitigated", CombatKeywordEx.MaxMitigatedDamageLimit),
        new SentenceEx("Until it has absorbed a total of %f damage", CombatKeywordEx.MaxMitigatedDamageLimit),
        new SentenceEx("Until it has absorbed %f total damage", CombatKeywordEx.MaxMitigatedDamageLimit),
        new SentenceEx ("Up to a maximum of %f total mitigated damage", CombatKeywordEx.MaxMitigatedDamageLimit),

        new SentenceEx("Remove ongoing #D effects (up to %f dmg/sec)", CombatKeywordEx.RemoveEffect),

        new SentenceEx("Non-Rage attack damage %f", new List<CombatKeywordEx>() { CombatKeywordEx.NonRageAttackBoost }),

        new SentenceEx("Target take %f indirect #D damage", CombatKeywordEx.AddMitigationIndirect, SignInterpretation.Opposite),
        new SentenceEx("Cause the target to take %f damage from indirect #D", CombatKeywordEx.AddMitigationIndirect, SignInterpretation.Opposite),
        new SentenceEx("Universal Indirect Mitigation %f", CombatKeywordEx.AddMitigationIndirect),
        new SentenceEx("Mitigate all damage over time by %f per tick", CombatKeywordEx.AddMitigationIndirect),
        new SentenceEx("Indirect #D mitigation %f", CombatKeywordEx.AddMitigationIndirect),
        new SentenceEx("Grant you %f Indirect #D Vulnerability", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationIndirect, CombatKeywordEx.ApplyToSelf}, SignInterpretation.Opposite),

        new SentenceEx("Dispel stun", CombatKeywordEx.RemoveStun),
        new SentenceEx("Dispel any stun", CombatKeywordEx.RemoveStun),
        new SentenceEx("Doing so remove the stun effect from you", new List<CombatKeywordEx>() { CombatKeywordEx.RemoveStun, CombatKeywordEx.ApplyToSelf }),

        new SentenceEx("Can be used while stunned", CombatKeywordEx.AllowedWhileStunned),

        new SentenceEx("If the target of @ dies", CombatKeywordEx.IfTargetDies),

        new SentenceEx("Next attack that hit you", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextHit }),

        new SentenceEx("Cause your next attack", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Your next attack", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("The next attack you use", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Next attack", CombatKeywordEx.NextAttack),
        new SentenceEx("Next ability", CombatKeywordEx.NextAttack),
        new SentenceEx("For one attack", CombatKeywordEx.NextAttack),

        new SentenceEx("Cause your next", new List<CombatKeywordEx>() { CombatKeywordEx.NextUse, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Your next use", CombatKeywordEx.NextUse),
        new SentenceEx("Your next ability", CombatKeywordEx.NextUse),
        new SentenceEx("Next #S attack", CombatKeywordEx.NextUse),
        new SentenceEx("The next use of", CombatKeywordEx.NextUse),

        new SentenceEx("Deal #D damage", CombatKeywordEx.ChangeDamageType),
        new SentenceEx("Deal direct #D damage", CombatKeywordEx.ChangeDamageType),
        new SentenceEx("Damage type become #D", CombatKeywordEx.ChangeDamageType),
        new SentenceEx("Damage type is #D instead", CombatKeywordEx.ChangeDamageType),
        new SentenceEx("Damage become #D", CombatKeywordEx.ChangeDamageType),
        new SentenceEx("Damage type is changed to #D", CombatKeywordEx.ChangeDamageType),

        new SentenceEx("Max Breath %f", CombatKeywordEx.IncreaseMaxBreath),

        new SentenceEx("Radiation Protection %f", CombatKeywordEx.IncreaseRadiationProtection),

        new SentenceEx("If target is more than %f meter away", CombatKeywordEx.RequireMinimumDistance),

        new SentenceEx("Before you teleport", CombatKeywordEx.BeforeTrigger),
        new SentenceEx("Until you trigger the teleport", CombatKeywordEx.BeforeTrigger),
        new SentenceEx("While @ is active", CombatKeywordEx.BeforeTrigger),
        new SentenceEx("When @ active", CombatKeywordEx.BeforeTrigger),

        new SentenceEx("Absorb %f damage before dissipating", CombatKeywordEx.AbsorbDamage),
        new SentenceEx("Absorb the first %f #D damage you suffer", CombatKeywordEx.AbsorbDamage),

        new SentenceEx("%f Stun Resistance", CombatKeywordEx.StunResistance),

        new SentenceEx("The next attacker that hit the pet will be stunned", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyToPet, CombatKeywordEx.RequireBeingHit, CombatKeywordEx.Stun }),
        new SentenceEx("Stun targets", CombatKeywordEx.Stun),
        new SentenceEx("Stun", CombatKeywordEx.Stun),

        new SentenceEx("Generate no Taunt", CombatKeywordEx.GenerateNoTaunt),

        new SentenceEx("Generate no Rage", CombatKeywordEx.GenerateNoRage),

        new SentenceEx("Remove (up to) %f more Rage", CombatKeywordEx.IncreaseRage, SignInterpretation.Opposite),
        new SentenceEx("Reduce Rage by %f", CombatKeywordEx.IncreaseRage, SignInterpretation.Opposite),
        new SentenceEx("Reduce %f more Rage", CombatKeywordEx.IncreaseRage, SignInterpretation.Opposite),
        new SentenceEx("Then reduce it by %f more", CombatKeywordEx.IncreaseRage, SignInterpretation.Opposite),
        new SentenceEx("Reduce the target's Rage by %f", CombatKeywordEx.IncreaseRage, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce target's Rage by %f", CombatKeywordEx.IncreaseRage, SignInterpretation.AlwaysNegative),
        new SentenceEx("Reduce targets' Rage by %f", CombatKeywordEx.IncreaseRage, SignInterpretation.AlwaysNegative),
        new SentenceEx("%f Rage reduction", CombatKeywordEx.IncreaseRage, SignInterpretation.AlwaysNegative),
        new SentenceEx("Generate %f Rage", CombatKeywordEx.IncreaseRage),
        new SentenceEx("Remove %f Rage", CombatKeywordEx.IncreaseRage, SignInterpretation.Opposite),
        new SentenceEx("Deplete %f Rage", CombatKeywordEx.IncreaseRage, SignInterpretation.Opposite),
        new SentenceEx("Cause targets to lose %f Rage", CombatKeywordEx.IncreaseRage, SignInterpretation.AlwaysNegative),
        new SentenceEx("Cause your pet's attack to generate %f Rage", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseRage, CombatKeywordEx.ApplyToPet }),

        //new SentenceEx("Cause your", CombatKeywordEx.ApplyToSelf),

        new SentenceEx("Reap %f health", CombatKeywordEx.DrainHealth),
        new SentenceEx("Steal %f health", CombatKeywordEx.DrainHealth),
        new SentenceEx("Steal %f more health", CombatKeywordEx.DrainHealth),

        new SentenceEx("The reap cap is %f", CombatKeywordEx.IncreaseDrainHealthMax),

        new SentenceEx("In addition, if it deal Indirect #D damage is %f per tick", new List<CombatKeywordEx>() { CombatKeywordEx.IndirectDamageBoost, CombatKeywordEx.RequireDoingDamageOverTime }),
        new SentenceEx("Boost the target's #D damage-over-time by %f per tick", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Universal Indirect Damage %f", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Boost targets' indirect damage %f", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Indirect #D damage %f", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Indirect #D %f (per tick)", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Indirect #D damage is %f per tick", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("%f indirect damage per tick", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Deal indirect #D damage is %f per tick", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Indirect #D damage", CombatKeywordEx.IndirectDamageBoost),
        new SentenceEx("Deal %f indirect #D damage", CombatKeywordEx.IndirectDamageBoost),

        new SentenceEx("If it deal direct #D damage", CombatKeywordEx.RequireDirectDamageType),

        new SentenceEx("In addition, the target take a second full blast of delayed #D damage", CombatKeywordEx.DelayedSecondAttack),

        new SentenceEx("Turning half of that into #D damage", CombatKeywordEx.TurnRageToDamage),
        new SentenceEx("This absorbed damage is added to your next @ attack at a %f rate", CombatKeywordEx.TurnMitigationToDamage),
        new SentenceEx("This absorbed damage is added to your next @", CombatKeywordEx.TurnMitigationToDamage),

        new SentenceEx("(Randomly determined for each attack)", CombatKeywordEx.RandomDamage),
        new SentenceEx("(Randomly determined)", CombatKeywordEx.RandomDamage),
        new SentenceEx("(Random)", CombatKeywordEx.RandomDamage),
        new SentenceEx("+Up to %f extra damage", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.RandomDamage }),
        new SentenceEx("%f random damage", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.RandomDamage }),

        new SentenceEx("To Aberration", CombatKeywordEx.RequireAnatomyAberration),

        new SentenceEx("When you have %f or less of your Armor left", CombatKeywordEx.RequireBelowMaxArmor),

        new SentenceEx("Have less than %f of their Max Rage", CombatKeywordEx.RequireBelowMaxRage),
        new SentenceEx("With less than %f of their Max Rage", CombatKeywordEx.RequireBelowMaxRage),

        new SentenceEx("Grant this Critical Hit Chance bonus to your next", new List<CombatKeywordEx>() { CombatKeywordEx.GrantCriticalChance, CombatKeywordEx.NextAttack }),
        new SentenceEx("Can Critically Hit based on your Anatomy skill levels", CombatKeywordEx.GrantCriticalChance),

        new SentenceEx("To Arthropods", CombatKeywordEx.RequireArthropodTarget),

        new SentenceEx("Slow target's movement by %f", CombatKeywordEx.Slow),
        new SentenceEx("Slow target's movement speed by %f", CombatKeywordEx.Slow),

        new SentenceEx("Non-Elite target", CombatKeywordEx.RequireNonEliteTarget),
        new SentenceEx("Non-Elite enemies", CombatKeywordEx.RequireNonEliteTarget),

        new SentenceEx("To Stunned targets", CombatKeywordEx.RequireStunnedTarget),
        new SentenceEx("If target is stunned", CombatKeywordEx.RequireStunnedTarget),

        new SentenceEx("To targets that are Knock Down", CombatKeywordEx.RequireKnockedDownTarget),
        new SentenceEx("If target is Knock Down", CombatKeywordEx.RequireKnockedDownTarget),

        new SentenceEx("Target is prone to random self-stuns", CombatKeywordEx.Concussion),

        new SentenceEx("Raise the target's Max Rage by %f", CombatKeywordEx.IncreaseMaxRage),
        new SentenceEx("Raise target's Max Rage by %f", CombatKeywordEx.IncreaseMaxRage),
        new SentenceEx("Increase target's Max Rage by %f", CombatKeywordEx.IncreaseMaxRage),

        new SentenceEx("Burst Evasion and Projectile evasion %f", CombatKeywordEx.IncreaseEvasionBurstAndProjectile),

        new SentenceEx("%f chance to avoid being hit by burst", CombatKeywordEx.IncreaseEvasionBurst),
        new SentenceEx("%f evasion of burst", CombatKeywordEx.IncreaseEvasionBurst),
        new SentenceEx("Burst Evasion %f", CombatKeywordEx.IncreaseEvasionBurst),
        new SentenceEx("%f Burst Evasion", CombatKeywordEx.IncreaseEvasionBurst),
        new SentenceEx("Boost Burst Evasion by %f", CombatKeywordEx.IncreaseEvasionBurst),
        new SentenceEx("%f Direct Burst Vulnerability", CombatKeywordEx.IncreaseEvasionBurst, SignInterpretation.Opposite),
        new SentenceEx("Projectile Evasion %f", CombatKeywordEx.IncreaseEvasionProjectile),
        new SentenceEx("Give you %f Projectile Evasion", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseEvasionProjectile, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Grant you %f Projectile Evasion", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseEvasionProjectile, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Projectile Evasion", CombatKeywordEx.IncreaseEvasionProjectile),
        new SentenceEx("Melee Evasion %f", CombatKeywordEx.IncreaseEvasionMelee),
        new SentenceEx("Boost your Melee Evasion %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseEvasionMelee, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Grant you %f Melee Evasion", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseEvasionMelee, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Give pet %f Melee Evasion", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseEvasionMelee, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("%f Melee Evasion", CombatKeywordEx.IncreaseEvasionMelee),
        new SentenceEx("%f Ranged Evasion", CombatKeywordEx.IncreaseEvasionRanged),
        new SentenceEx("Boost melee evasion %f", CombatKeywordEx.IncreaseEvasionMelee),
        new SentenceEx("Melee Evasion is %f", CombatKeywordEx.IncreaseEvasionMelee),
        new SentenceEx("Lower targets' Evasion by %f", CombatKeywordEx.IncreaseEvasion, SignInterpretation.AlwaysNegative),

        new SentenceEx("You gain Elite Resistance %f", new List<CombatKeywordEx>() { CombatKeywordEx.IncreaseEliteResistance, CombatKeywordEx.ApplyToSelf }),

        new SentenceEx("Ignore mitigation from armor", CombatKeywordEx.IgnoreArmor),

        new SentenceEx("The next time the victim Evades an attack", CombatKeywordEx.NextEvade),

        new SentenceEx("Induces Fear in the target", CombatKeywordEx.Fear),
        new SentenceEx("Cause all sentient targets to flee in terror", new List<CombatKeywordEx>() { CombatKeywordEx.Fear, CombatKeywordEx.RequireSentientTarget }),

        new SentenceEx("Cause you to erupt in a fountain of vile blood: a Burst Trauma attack with Base Damage %f", CombatKeywordEx.VileBloodAttack),

        new SentenceEx("When used while stunned", CombatKeywordEx.RequireBeingStunned),
        new SentenceEx("If used while you are stunned", CombatKeywordEx.RequireBeingStunned),

        new SentenceEx("Implant insect eggs in the target. (Max 4 stacks.) Future Deer Kicks by any pet deer or player deer will cause target to take %f #D damage", new List<CombatKeywordEx>() { CombatKeywordEx.DamageBoost, CombatKeywordEx.ImplantDeerEgg}),
        new SentenceEx("Subsequent Deer Kicks to this target", CombatKeywordEx.ImplantDeerEgg),

        new SentenceEx("Summon a deer ally", CombatKeywordEx.SummonDeer),

        new SentenceEx("%f chance to", CombatKeywordEx.ApplyWithChance),
        new SentenceEx("There's a %f chance", CombatKeywordEx.ApplyWithChance),
        new SentenceEx("%f of the time", CombatKeywordEx.ApplyWithChance),

        new SentenceEx("Boost your mitigation vs physical damage %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationPhysical, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Mitigation vs physical damage %f", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("Elite Physical Damage Mitigation %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddMitigationPhysical, CombatKeywordEx.RequireEliteTarget }),
        new SentenceEx("Physical Damage Mitigation %f", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("%f absorption of any physical damage", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("%f mitigation of all physical attack", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("%f mitigation of any physical damage", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("%f mitigation against physical attack", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("Cause target to suffer %f damage from future physical attack", CombatKeywordEx.AddMitigationPhysical, SignInterpretation.AlwaysNegative),
        new SentenceEx("Mitigate %f of all physical damage", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("Mitigate %f physical damage", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("%f Mitigation vs physical (slashing, piercing, and crushing) attack", CombatKeywordEx.AddMitigationPhysical),
        new SentenceEx("%f Mitigation vs physical (slashing, crushing, and piercing) damage", CombatKeywordEx.AddMitigationPhysical),

        new SentenceEx("Ignore Knockback effect", CombatKeywordEx.IgnoreKnockback),
        new SentenceEx("Immunity to Knockback", CombatKeywordEx.IgnoreKnockback),
        new SentenceEx("Grant all targets immunity to Knockback", CombatKeywordEx.IgnoreKnockback),
        new SentenceEx("Grant Knockback Immunity", CombatKeywordEx.IgnoreKnockback),
        new SentenceEx("Immune to Knockback effects", CombatKeywordEx.IgnoreKnockback),
        new SentenceEx("Grant the target %f Knockback Ignore Chance", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyWithChance, CombatKeywordEx.IgnoreKnockback }),

        new SentenceEx("Take %f second to channel", CombatKeywordEx.IncreaseChannelingTime),
        new SentenceEx("Channeling time is %f second", CombatKeywordEx.IncreaseChannelingTime),

        new SentenceEx("Summon figment", CombatKeywordEx.SummonFigment),

        new SentenceEx("Place an extra trap", CombatKeywordEx.SummonStunTrap),

        new SentenceEx("Summon a second tornado", CombatKeywordEx.SummonTornado),

        new SentenceEx("If you are using the #S skill", CombatKeywordEx.RequireActiveSkill),
        new SentenceEx("While the #S skill is active", CombatKeywordEx.RequireActiveSkill),
        new SentenceEx("While #S skill is active", CombatKeywordEx.RequireActiveSkill),
        new SentenceEx("While #S skill active", CombatKeywordEx.RequireActiveSkill),
        new SentenceEx("When #S skill active", CombatKeywordEx.RequireActiveSkill),
        new SentenceEx("(If #S skill is active)", CombatKeywordEx.RequireActiveSkill),

        new SentenceEx("Deal double damage", CombatKeywordEx.DamageBoostDouble),

        new SentenceEx("Deal double direct damage", CombatKeywordEx.DamageBoostDouble),

        new SentenceEx("Boost Jump Height", CombatKeywordEx.IncreaseJumpHeight),

        new SentenceEx("To Undead", CombatKeywordEx.RequireUndeadTarget),
        new SentenceEx("On an undead target", CombatKeywordEx.RequireUndeadTarget),
        new SentenceEx("Vs Undead", CombatKeywordEx.RequireUndeadTarget),

        new SentenceEx("Cause the target to deal %f #D damage to themselves the next time they use a Rage attack", new List<CombatKeywordEx>() { CombatKeywordEx.SelfDamage, CombatKeywordEx.NextRageAttack }),

        new SentenceEx("While simultaneously channeling a different #S ability", CombatKeywordEx.RequireChanneling),

        new SentenceEx("Targets also take %f damage from sentient weather phenomena", CombatKeywordEx.TornadoVulnerability),

        new SentenceEx("%f Physical Damage Reflection", CombatKeywordEx.IncreasePhysicalReflection),
        new SentenceEx("Plus bonus Armor-specific damage equal to %f of the attack's total regular damage", CombatKeywordEx.IncreasePhysicalReflection),
        new SentenceEx("You reflect %f of any physical attack damage (#D) back at the attacker.", CombatKeywordEx.IncreasePhysicalReflection),

        new SentenceEx("Grant you immunity to direct damage", CombatKeywordEx.DirectDamageImmunity),

        new SentenceEx("Trigger the target's Vulnerability", CombatKeywordEx.MakeVulnerable),

        new SentenceEx("Future @ to the same target", CombatKeywordEx.RequireRepeatedAttack),

        new SentenceEx("All other #S attack", CombatKeywordEx.RequireUsingCombatSkill),
        new SentenceEx("If it is a #S ability", CombatKeywordEx.RequireUsingCombatSkill),

        new SentenceEx("If target has %f or more Damage-over-Time effects", CombatKeywordEx.RequireDebuffedTarget),

        new SentenceEx("To sentient creatures", CombatKeywordEx.RequireSentientTarget),

        new SentenceEx("Attack Range is %f", CombatKeywordEx.IncreaseRange),
        new SentenceEx("Range is %f meter", CombatKeywordEx.IncreaseRange),
        new SentenceEx("Range is increased %f meter", CombatKeywordEx.IncreaseRange),
        new SentenceEx("Range is reduced %f meter", CombatKeywordEx.IncreaseRange, SignInterpretation.AlwaysNegative),
        new SentenceEx("Range %f", CombatKeywordEx.IncreaseRange),

        new SentenceEx("If target is covered", CombatKeywordEx.RequireTargetUnderEffect),
        new SentenceEx("To targets that are covered", CombatKeywordEx.RequireTargetUnderEffect),

        new SentenceEx("Combat Refreshes restore %f power", CombatKeywordEx.IncreaseCombatRefreshPowerRestore),

        new SentenceEx("Combat Refresh restore %f health", CombatKeywordEx.IncreaseCombatRefreshHealing),
        new SentenceEx("Healing from Combat Refreshes %f", CombatKeywordEx.IncreaseCombatRefreshHealing),

        new SentenceEx("%f Resistance to Elemental damage (Fire, Cold, Electricity)", CombatKeywordEx.AddMitigationElemental),
        new SentenceEx("%f Mitigation vs elemental (fire, cold, and electricity) damage", CombatKeywordEx.AddMitigationElemental),

        new SentenceEx("In-Combat Armor Regeneration %f", CombatKeywordEx.IncreaseArmorRegeneration),

        new SentenceEx("It trigger again, targeting an enemy", CombatKeywordEx.TargetAnotherEnnemy),
        new SentenceEx("Also attack another enemy", CombatKeywordEx.TargetAnotherEnnemy),
        new SentenceEx("Your next @ will simultaneously shoot", new List<CombatKeywordEx>() { CombatKeywordEx.TargetAnotherEnnemy, CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Subsequent #S attack will also shoot", new List<CombatKeywordEx>() { CombatKeywordEx.TargetAnotherEnnemy, CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),
        new SentenceEx("Subsequent #S attack will also fire", new List<CombatKeywordEx>() { CombatKeywordEx.TargetAnotherEnnemy, CombatKeywordEx.ApplyToSelf, CombatKeywordEx.NextAttack }),

        new SentenceEx("Knock Down targets", CombatKeywordEx.Knockdown),

        new SentenceEx("Chance to ignore Stun %f", new List<CombatKeywordEx>() { CombatKeywordEx.ApplyWithChance, CombatKeywordEx.IgnoreStun }),
        new SentenceEx("Ignore Stun", CombatKeywordEx.IgnoreStun),

        new SentenceEx("Out of Combat Sprint Speed %f", CombatKeywordEx.AddOutOfCombatSpeed),
        new SentenceEx("%f Out of Combat Sprint Speed", CombatKeywordEx.AddOutOfCombatSpeed),
        new SentenceEx("Your Out of Combat Sprint speed %f", CombatKeywordEx.AddOutOfCombatSpeed),
        new SentenceEx("Your Out of Combat Sprint speed by %f", CombatKeywordEx.AddOutOfCombatSpeed),
        new SentenceEx("Non-combat Sprint Boost %f", CombatKeywordEx.AddOutOfCombatSpeed),

        new SentenceEx("Dispel slow and root", CombatKeywordEx.RemoveSlowRoot),
        new SentenceEx("Dispel any Slow or Root", CombatKeywordEx.RemoveSlowRoot),
        new SentenceEx("Dispel any active Slow or Root", CombatKeywordEx.RemoveSlowRoot),
        new SentenceEx("Grant immunity to new slow and root", CombatKeywordEx.GrantSlowRootImmunity),
        new SentenceEx("Grant them immunity to Slow and Root", CombatKeywordEx.GrantSlowRootImmunity),
        new SentenceEx("Grant you immunity to similar effects", CombatKeywordEx.GrantSlowRootImmunity),
        new SentenceEx("Slow/Root Ignore Chance %f", new List<CombatKeywordEx>() { CombatKeywordEx.GrantSlowRootImmunity, CombatKeywordEx.RemoveSlowRoot }),
        new SentenceEx("Grant the target %f Slow/Root Ignore Chance", CombatKeywordEx.GrantSlowRootImmunity),

        new SentenceEx("%f Cold Protection (Direct and Indirect)", CombatKeywordEx.IncreaseProtectionCold),
        new SentenceEx("%f Direct and Indirect Cold Protection", CombatKeywordEx.IncreaseProtectionCold),

        new SentenceEx("Target ignore you", CombatKeywordEx.GetIgnored),
        new SentenceEx("Cause the target to ignore you", CombatKeywordEx.GetIgnored),

        new SentenceEx("Shuffling their hatred", CombatKeywordEx.ShuffleTaunt),

        new SentenceEx("While standing near your @", CombatKeywordEx.RequireStandingSomewhere),

        new SentenceEx("Worth %f more XP", CombatKeywordEx.IncreaseXpGain),
        new SentenceEx("%f Earned Combat XP", CombatKeywordEx.IncreaseXpGain),

        new SentenceEx("Fly speed is boosted %f", CombatKeywordEx.AddFlySpeed),
        new SentenceEx("Fly Speed %f", CombatKeywordEx.AddFlySpeed),

        new SentenceEx("Turn while leaping", CombatKeywordEx.FreeMovementWhileLeaping),
        new SentenceEx("Free-form movement while leaping", CombatKeywordEx.FreeMovementWhileLeaping),

        new SentenceEx("Swim Speed %f", CombatKeywordEx.AddSwimSpeed),

        new SentenceEx("Temp-taunt the target %f", CombatKeywordEx.GenerateTemporaryTaunt),
        new SentenceEx("Taunt (Temporary) %f", CombatKeywordEx.GenerateTemporaryTaunt),

        new SentenceEx("%f Death Avoidance", CombatKeywordEx.IncreaseDeathAvoidance),

        new SentenceEx("Uniformly diminishes all targets' entire aggro lists by %f", CombatKeywordEx.ReduceAggro),

        new SentenceEx("Grant pets Health/Armor equal to the amount of Power generated %f", new List<CombatKeywordEx>() { CombatKeywordEx.TurnPowerToHealth, CombatKeywordEx.RequirePetTarget }),
        new SentenceEx("Targeted pets recover Health/Armor equal to the amount of Power generated %f", new List<CombatKeywordEx>() { CombatKeywordEx.TurnPowerToHealth, CombatKeywordEx.RequirePetTarget }),

        new SentenceEx("#D damage no longer dispel", CombatKeywordEx.NoDispel),

        new SentenceEx("Add %f Crystal Ice to your inventory", CombatKeywordEx.RestoreCrystalIce),
        new SentenceEx("Gain %f Crystal Ice", CombatKeywordEx.RestoreCrystalIce),

        new SentenceEx("Any vile blood eruptions from Blood-Mist deal %f direct damage", CombatKeywordEx.VileBloodDamage),
        new SentenceEx("Blood-Mist Eruption Damage %f", CombatKeywordEx.VileBloodDamage),

        new SentenceEx("If you are reduced to %f health", CombatKeywordEx.RequireLowHealth),

        new SentenceEx("Cancels the damage", CombatKeywordEx.CancelDamage),

        new SentenceEx("Or until you are attacked", CombatKeywordEx.RequireNotAttacked),

        new SentenceEx("Plant creatures", CombatKeywordEx.RequirePlantTarget),

        new SentenceEx("To pigs", CombatKeywordEx.RequirePigTarget),

        new SentenceEx("To fey", CombatKeywordEx.RequireFeyTarget),

        new SentenceEx("To lycanthropes", CombatKeywordEx.RequireWerewolfTarget),

        new SentenceEx("Ignite the target", CombatKeywordEx.Ignite),

        new SentenceEx("When you would have found non-magical equipment as loot, you'll find magical equipment instead", CombatKeywordEx.UpgradedLoot),
        new SentenceEx("When you would have found non-magical equipment as loot, you'll find Rare (blue-label) equipment instead", CombatKeywordEx.UpgradedLoot),

        new SentenceEx("Equipping this item grant you basic proficiency", CombatKeywordEx.GrantProficiency),
        new SentenceEx("Equipping this allows you to use the ability", CombatKeywordEx.GrantProficiency),
        new SentenceEx("Long-term animals that equip this can use the ability", CombatKeywordEx.GrantProficiency),
        new SentenceEx("You can use the ability", CombatKeywordEx.GrantProficiency),
        new SentenceEx("Equipping this armor teaches you", CombatKeywordEx.GrantProficiency),

        new SentenceEx("Periodically creates Sugar", CombatKeywordEx.CreateSugar),

        new SentenceEx("When your golf ball is used up", CombatKeywordEx.RequireGolfBallUsedUp),

        new SentenceEx("Dispel one fire effect on you (up to %f damage/tick)", new List<CombatKeywordEx>() { CombatKeywordEx.DispelOneFireEffect, CombatKeywordEx.ApplyToSelf }),

        new SentenceEx("Dispel lycanthropic regeneration effects", CombatKeywordEx.DispelLycanthropicEffects),

        new SentenceEx("Automatically Evade the first @ aimed at you", new List<CombatKeywordEx>() { CombatKeywordEx.EvadeNextAttack, CombatKeywordEx.ApplyToSelf }),

        new SentenceEx("While in water", CombatKeywordEx.RequireUnderwater),

        new SentenceEx("Critically Hit", CombatKeywordEx.RequireCriticalHit),

        new SentenceEx("This effect is increased by treasure that boost", CombatKeywordEx.ApplyOtherMods),
    };
}
