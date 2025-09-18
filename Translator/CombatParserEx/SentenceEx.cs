namespace Translator;

using System.Collections.Generic;
using PgObjects;

public class SentenceEx
{
    #region Init
    public SentenceEx(string format, CombatKeywordEx associatedKeyword)
    {
        Format = format;
        AssociatedKeywordList = new List<CombatKeywordEx>() { associatedKeyword };
        SignInterpretation = SignInterpretation.Normal;

        foreach (CombatKeywordEx Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(Keyword);
    }

    public SentenceEx(string format, CombatKeywordEx associatedKeyword, SignInterpretation signInterpretation)
    {
        Format = format;
        AssociatedKeywordList = new List<CombatKeywordEx>() { associatedKeyword };
        SignInterpretation = signInterpretation;

        foreach (CombatKeywordEx Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(Keyword);
    }

    public SentenceEx(string format, List<CombatKeywordEx> combatKeywordList)
    {
        Format = format;
        AssociatedKeywordList = combatKeywordList;
        SignInterpretation = SignInterpretation.Normal;

        foreach (CombatKeywordEx Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(Keyword);
    }

    public SentenceEx(string format, List<CombatKeywordEx> combatKeywordList, SignInterpretation signInterpretation)
    {
        Format = format;
        AssociatedKeywordList = combatKeywordList;
        SignInterpretation = signInterpretation;

        foreach (CombatKeywordEx Keyword in AssociatedKeywordList)
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(Keyword);
    }
    #endregion

    #region Properties
    public string Format { get; }
    public List<CombatKeywordEx> AssociatedKeywordList { get; }
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
        foreach (CombatKeywordEx Keyword in AssociatedKeywordList)
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
