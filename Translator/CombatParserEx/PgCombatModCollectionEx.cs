namespace PgObjects;

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

public class PgCombatModCollectionEx : List<PgCombatModEx>
{
    public static bool DebugMode { get; set; } = true;

    public void Display(string powerKey)
    {
        if (!TrueForAll(item => item.PermanentEffects.Count == 0 && item.DynamicEffects.Count == 0))
        {
            Write("  ", $"{{ \"{powerKey}\", new {GetType().Name}()");
            Write("     ", " {");

            foreach (PgCombatModEx Item in this)
                Display(Item);

            Write("     ", " }");
            Write(" ", " },", newLine: true);
        }
    }

    private static void Display(PgCombatModEx pgCombatModEx)
    {
        Write("         ", $" new {pgCombatModEx.GetType().Name}()");
        Write("         ", " {");
        Write("             ", $" Description = \"{pgCombatModEx.Description.Replace("\"", "\\\"")}\",");
        Write("             ", $" PermanentEffects = new List<{typeof(PgPermanentModEffectEx).Name}>()");
        Write("             ", " {");

        foreach (PgPermanentModEffectEx Item in pgCombatModEx.PermanentEffects)
            Display(Item);

        Write("             ", " },");
        Write("             ", $" DynamicEffects = new List<{typeof(PgCombatModEffectEx).Name}>()");
        Write("             ", " {");

        foreach (PgCombatModEffectEx Item in pgCombatModEx.DynamicEffects)
            Display(Item);

        Write("             ", " },");
        Write("         ", " },");
    }

    private static void Display(PgPermanentModEffectEx pgPermanentModEffectEx)
    {
        Debug.Assert(pgPermanentModEffectEx.Target != CombatTarget.Internal_None);

        Write("                 ", $" new {pgPermanentModEffectEx.GetType().Name}()");
        Write("                 ", " {");
        Write("                     ", $" Keyword = {pgPermanentModEffectEx.Keyword.GetType().Name}.{pgPermanentModEffectEx.Keyword},");
        Write("                     ", $" Data = {NumericValueToString(pgPermanentModEffectEx.Data)},");

        if (!float.IsNaN(pgPermanentModEffectEx.DelayInSeconds))
            Write("                     ", $" DelayInSeconds = {pgPermanentModEffectEx.DelayInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgPermanentModEffectEx.RecurringDelay))
            Write("                     ", $" RecurringDelay = {pgPermanentModEffectEx.RecurringDelay.ToString(CultureInfo.InvariantCulture)},");

        Write("                     ", $" Target = CombatTarget.{pgPermanentModEffectEx.Target},");

        if (pgPermanentModEffectEx.Condition != CombatCondition.Internal_None)
            Write("                     ", $" Condition = CombatCondition.{pgPermanentModEffectEx.Condition},");

        if (pgPermanentModEffectEx.ActiveAbilityCondition != AbilityKeyword.Internal_None)
            Write("                     ", $" ActiveAbilityCondition = AbilityKeyword.{pgPermanentModEffectEx.ActiveAbilityCondition},");

        Write("                 ", " },");
    }

    private static void Display(PgCombatModEffectEx pgCombatModEffectEx)
    {
        Write("                 ", $" new {pgCombatModEffectEx.GetType().Name}()");
        Write("                 ", " {");
        Write("                     ", $" Keyword = {pgCombatModEffectEx.Keyword.GetType().Name}.{pgCombatModEffectEx.Keyword},");
        Write("                     ", $" AbilityList = new List<AbilityKeyword>() {{ {AbilityKeywordListToString(pgCombatModEffectEx.AbilityList)} }},");
        Write("                     ", $" Data = {NumericValueToString(pgCombatModEffectEx.Data)},");

        if (pgCombatModEffectEx.DamageType != GameDamageType.Internal_None)
            Write("                     ", $" DamageType = GameDamageType.{pgCombatModEffectEx.DamageType},");

        if (pgCombatModEffectEx.CombatSkill != GameCombatSkill.Internal_None)
            Write("                     ", $" CombatSkill = GameCombatSkill.{pgCombatModEffectEx.CombatSkill},");

        if (!float.IsNaN(pgCombatModEffectEx.RandomChance))
            Write("                     ", $" RandomChance = {pgCombatModEffectEx.RandomChance.ToString(CultureInfo.InvariantCulture)}F,");

        if (!float.IsNaN(pgCombatModEffectEx.DelayInSeconds))
            Write("                     ", $" DelayInSeconds = {pgCombatModEffectEx.DelayInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgCombatModEffectEx.DurationInSeconds))
            Write("                     ", $" DurationInSeconds = {pgCombatModEffectEx.DurationInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgCombatModEffectEx.RecurringDelay))
            Write("                     ", $" RecurringDelay = {pgCombatModEffectEx.RecurringDelay.ToString(CultureInfo.InvariantCulture)},");

        if (pgCombatModEffectEx.Target != CombatTarget.Internal_None)
            Write("                     ", $" Target = CombatTarget.{pgCombatModEffectEx.Target},");

        if (!float.IsNaN(pgCombatModEffectEx.TargetRange))
            Write("                     ", $" TargetRange = {pgCombatModEffectEx.TargetRange.ToString(CultureInfo.InvariantCulture)},");

        if (pgCombatModEffectEx.TargetAbility != AbilityKeyword.Internal_None)
            Write("                     ", $" TargetAbility = AbilityKeyword.{pgCombatModEffectEx.TargetAbility},");

        if (pgCombatModEffectEx.Condition != CombatCondition.Internal_None)
            Write("                     ", $" Condition = CombatCondition.{pgCombatModEffectEx.Condition},");

        if (pgCombatModEffectEx.ActiveAbilityCondition != AbilityKeyword.Internal_None)
            Write("                     ", $" ActiveAbilityCondition = AbilityKeyword.{pgCombatModEffectEx.ActiveAbilityCondition},");

        if (!float.IsNaN(pgCombatModEffectEx.ConditionPercentage))
            Write("                     ", $" ConditionPercentage = {pgCombatModEffectEx.ConditionPercentage.ToString(CultureInfo.InvariantCulture)},");

        Write("                 ", " },");
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

    private static void Write(string indent, string text, bool newLine = false)
    {
        if (DebugMode)
            Debug.WriteLine($"{indent}{text}");
        else if (newLine)
            Debug.WriteLine(text);
        else
            Debug.Write(text);
    }
}
