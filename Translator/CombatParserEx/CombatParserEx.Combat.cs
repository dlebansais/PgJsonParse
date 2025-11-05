namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;

internal partial class CombatParserEx
{
    public void AnalyzeMods()
    {
        List<string[]> StringKeyTable = new();
        List<PgModEffectCollectionEx> AnalyzedPowerKeyToCompleteEffectTable = new();

        AnalyzeMatchingPowersAndEffects(StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable);
        AnalyzeRemainingPowers(StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable);
    }
}
