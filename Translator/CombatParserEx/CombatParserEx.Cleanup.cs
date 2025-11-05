namespace TranslatorCombatParserEx;

using System.Diagnostics;
using PgObjects;

internal partial class CombatParserEx
{
    public void Cleanup(PgCombatModEx pgCombatModEx)
    {
        for (int i = 0; i < pgCombatModEx.DynamicEffects.Count; i++)
            CleanupDynamicEffects(pgCombatModEx, i);
    }

    private void CleanupDynamicEffects(PgCombatModEx pgCombatModEx, int index)
    {
        PgCombatModEffectEx DynamicEffect = pgCombatModEx.DynamicEffects[index];

        switch (DynamicEffect.Keyword)
        {
            case CombatKeywordEx.RestoreHealth:
                CleanupDynamicEffectRestoreHealth(pgCombatModEx, index, DynamicEffect);
                break;
        }
    }

    private void CleanupDynamicEffectRestoreHealth(PgCombatModEx pgCombatModEx, int index, PgCombatModEffectEx dynamicEffect)
    {
        Debug.Assert(dynamicEffect.Keyword == CombatKeywordEx.RestoreHealth);
    }
}
