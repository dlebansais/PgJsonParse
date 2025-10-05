namespace PgObjects;

using System.Collections.Generic;
using System.Diagnostics;

[DebuggerDisplay("{AsString,nq}")]
public class PgCombatConditionCollectionEx : List<CombatCondition>
{
    public string AsString
    {
        get
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
}
