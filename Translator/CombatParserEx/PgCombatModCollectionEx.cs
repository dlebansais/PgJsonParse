namespace PgObjects;

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

public class PgCombatModCollectionEx : List<PgCombatModEx>
{
    public void Display(string powerKey)
    {
        if (!TrueForAll(item => item.StaticEffects.Count == 0 && item.DynamicEffects.Count == 0))
        {
            Debug.WriteLine("        /*");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         *");
            Debug.WriteLine("         */");
            Debug.WriteLine($"        {{ \"{powerKey}\", new {GetType().Name}()");
            Debug.WriteLine("            {");

            foreach (PgCombatModEx Item in this)
                Display(Item);

            Debug.WriteLine("            }");
            Debug.WriteLine("        },");
        }
    }

    private static void Display(PgCombatModEx pgCombatModEx)
    {
        Debug.WriteLine($"                new {pgCombatModEx.GetType().Name}()");
        Debug.WriteLine("                {");
        Debug.WriteLine($"                    Description = \"{pgCombatModEx.Description.Replace("\"", "\\\"")}\",");
        Debug.WriteLine($"                    StaticEffects = new List<{typeof(PgCombatModEffectEx).Name}>()");
        Debug.WriteLine("                    {");

        foreach (PgCombatModEffectEx Item in pgCombatModEx.StaticEffects)
            Display(Item);

        Debug.WriteLine("                    },");
        Debug.WriteLine($"                    DynamicEffects = new List<{typeof(PgCombatModEffectEx).Name}>()");
        Debug.WriteLine("                    {");

        foreach (PgCombatModEffectEx Item in pgCombatModEx.DynamicEffects)
            Display(Item);

        Debug.WriteLine("                    },");
        Debug.WriteLine("                },");
    }

    private static void Display(PgCombatModEffectEx pgCombatModEffectEx)
    {
        Debug.WriteLine($"                        new {pgCombatModEffectEx.GetType().Name}()");
        Debug.WriteLine("                        {");
        Debug.WriteLine($"                            Keyword = {pgCombatModEffectEx.Keyword.GetType().Name}.{pgCombatModEffectEx.Keyword},");
        Debug.WriteLine($"                            AbilityList = new List<AbilityKeyword>() {{ {AbilityKeywordListToString(pgCombatModEffectEx.AbilityList)} }},");
        Debug.WriteLine($"                            Data = {NumericValueToString(pgCombatModEffectEx.Data)},");

        if (pgCombatModEffectEx.DamageType != GameDamageType.Internal_None)
            Debug.WriteLine($"                            DamageType = GameDamageType.{pgCombatModEffectEx.DamageType},");

        if (pgCombatModEffectEx.CombatSkill != GameCombatSkill.Internal_None)
            Debug.WriteLine($"                            CombatSkill = GameCombatSkill.{pgCombatModEffectEx.CombatSkill},");

        if (!float.IsNaN(pgCombatModEffectEx.RandomChance))
            Debug.WriteLine($"                            RandomChance = {pgCombatModEffectEx.RandomChance.ToString(CultureInfo.InvariantCulture)}F,");

        if (!float.IsNaN(pgCombatModEffectEx.DelayInSeconds))
            Debug.WriteLine($"                            DelayInSeconds = {pgCombatModEffectEx.DelayInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgCombatModEffectEx.DurationInSeconds))
            Debug.WriteLine($"                            DurationInSeconds = {pgCombatModEffectEx.DurationInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (pgCombatModEffectEx.Target != CombatTarget.Internal_None)
            Debug.WriteLine($"                            Target = CombatTarget.{pgCombatModEffectEx.Target},");

        Debug.WriteLine("                        },");
    }

    private static string AbilityKeywordListToString(List<AbilityKeyword> list)
    {
        string Result = string.Empty;

        foreach (AbilityKeyword Keyword in list)
        {
            if (Result.Length > 0)
                Result += ", ";

            Result += $"AbilityKeyword.{Keyword}";
        }

        return Result;
    }

    private static string NumericValueToString(PgNumericValueEx numericValue)
    {
        if (float.IsNaN(numericValue.Value))
            return $"{numericValue.GetType().Name}.Empty";

        string Result = $"new {numericValue.GetType().Name}() {{ Value = ";
        if (numericValue.Value > 0)
            Result += "+";

        Result += numericValue.Value.ToString(CultureInfo.InvariantCulture);
        Result += "F";

        if (numericValue.IsPercent)
            Result += ", IsPercent = true";

        Result += " }";

        return Result;
    }
}
