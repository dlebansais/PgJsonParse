namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    private static List<SentenceEx> SentenceList = new List<SentenceEx>()
    {
        new SentenceEx("Heal you for %f Health/Armor", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Heal you for %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Restore %f Health to your pet", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToPet }),
        new SentenceEx("Restore %f Health to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("You regain %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealth, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f Power to you", new List<CombatKeywordEx>() { CombatKeywordEx.RestorePower, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("%f chance to", CombatKeywordEx.ApplyWithChance),
        new SentenceEx("After a %f second delay", CombatKeywordEx.EffectDelay),
        new SentenceEx("After an %f second delay", CombatKeywordEx.EffectDelay),
        new SentenceEx("For %f second", CombatKeywordEx.EffectDuration),
        new SentenceEx("Boost your movement speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
        new SentenceEx("Boost movement speed by %f", CombatKeywordEx.AddSprintSpeed),
        new SentenceEx("Increase your movement speed by %f", new List<CombatKeywordEx>() { CombatKeywordEx.AddSprintSpeed, CombatKeywordEx.ApplyToSelf }),
    };
}
