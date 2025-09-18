namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using System.Diagnostics;
using PgObjects;

internal partial class CombatParserEx
{
    private void VerifyConsistency()
    {
        /* TODO: restore
        foreach (AbilityKeyword Keyword in GenericAbilityList)
            if (!GenericAbilityListUsed.Contains(Keyword))
                Debug.WriteLine($"Generic ability keyword {Keyword} was not used.");
        */

        foreach (AbilityPetType PetType in PetTypeToKeywordTable.Keys)
            if (!PetTypeToKeywordTableUsed.Contains(PetType))
                Debug.WriteLine($"Pet type {PetType} was not used.");

        foreach (string Name in KnownBaseAbilityNameTable.Keys)
            if (!KnownBaseAbilityNameTableUsed.Contains(Name))
                Debug.WriteLine($"Known base ability name {Name} was not used.");

        foreach (AbilityKeyword Keyword in KeywordIgnoreList)
            if (!KeywordIgnoreListUsed.Contains(Keyword))
                Debug.WriteLine($"Ignored keyword {Keyword} was not used.");

        foreach (AbilityKeyword Keyword in AbilitySpecificKeywordList)
            if (!AbilitySpecificKeywordListUsed.Contains(Keyword))
                Debug.WriteLine($"Ability specific keyword {Keyword} was not used.");
    }

    private List<AbilityKeyword> GenericAbilityListUsed = new();
    private List<AbilityPetType> PetTypeToKeywordTableUsed = new();
    private List<string> KnownBaseAbilityNameTableUsed = new();
    private List<AbilityKeyword> KeywordIgnoreListUsed = new();
    private List<AbilityKeyword> AbilitySpecificKeywordListUsed = new();
}
