namespace TranslatorCombatParserEx;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using PgObjects;
using Translator;

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
