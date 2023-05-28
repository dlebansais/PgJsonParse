namespace Translator;

using System.Collections.Generic;
using PgObjects;

public class Sentence
{
    #region Init
    public Sentence(string format, CombatKeyword associatedKeyword)
    {
        Format = format;
        AssociatedKeywordList = new List<CombatKeyword>() { associatedKeyword };
        SignInterpretation = SignInterpretation.Normal;

        foreach (CombatKeyword Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(Keyword);
    }

    public Sentence(string format, CombatKeyword associatedKeyword, SignInterpretation signInterpretation)
    {
        Format = format;
        AssociatedKeywordList = new List<CombatKeyword>() { associatedKeyword };
        SignInterpretation = signInterpretation;

        foreach (CombatKeyword Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(Keyword);
    }

    public Sentence(string format, List<CombatKeyword> combatKeywordList)
    {
        Format = format;
        AssociatedKeywordList = combatKeywordList;
        SignInterpretation = SignInterpretation.Normal;

        foreach (CombatKeyword Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(Keyword);
    }

    public Sentence(string format, List<CombatKeyword> combatKeywordList, SignInterpretation signInterpretation)
    {
        Format = format;
        AssociatedKeywordList = combatKeywordList;
        SignInterpretation = signInterpretation;

        foreach (CombatKeyword Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(Keyword);
    }
    #endregion

    #region Properties
    public string Format { get; }
    public List<CombatKeyword> AssociatedKeywordList { get; }
    public SignInterpretation SignInterpretation { get; }
    public bool IsUsed { get; private set; }
    #endregion

    #region Client Interface
    public void SetUsed()
    {
        IsUsed = true;
    }
    #endregion

    #region Debugging
    public override string ToString()
    {
        string KeywordListString = string.Empty;
        foreach (CombatKeyword Keyword in AssociatedKeywordList)
        {
            if (KeywordListString.Length > 0)
                KeywordListString += ", ";

            KeywordListString += Keyword.ToString();
        }

        string SignInterpretationString = SignInterpretation == SignInterpretation.Normal ? string.Empty : $". Sign: {SignInterpretation}";
        return $"{Format} -> {KeywordListString}{SignInterpretationString}";
    }
    #endregion
}
