namespace PgObjects;

using System.Collections.Generic;

public class PgCombatConditionCollectionEx : List<CombatCondition>
{
    public override string ToString()
    {
        string Result = string.Empty;

        foreach (CombatCondition Condition in this)
        {
            if (Result.Length > 0)
                Result += ", ";

            Result += $"{Condition}";
        }

        return Result;
    }
}
