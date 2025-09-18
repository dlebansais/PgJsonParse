namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    private static List<SentenceEx> SentenceList = new List<SentenceEx>()
    {
        new SentenceEx("Heal you for %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthToSelf }),
        new SentenceEx("Restore %f Health to you", CombatKeywordEx.RestoreHealthToSelf),
        new SentenceEx("You regain %f Health", new List<CombatKeywordEx>() { CombatKeywordEx.RestoreHealthToSelf }),
    };
}
