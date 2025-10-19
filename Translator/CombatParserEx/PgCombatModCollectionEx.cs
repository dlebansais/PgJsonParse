namespace PgObjects;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

public class PgCombatModCollectionEx : List<PgCombatModEx>
{
    public static bool DebugMode { get; set; } = false;

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
        Write("             ", $" Description = \"{Encoded(pgCombatModEx.Description)}\",");
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

        if (pgPermanentModEffectEx.DamageType != GameDamageType.Internal_None)
            Write("                     ", $" DamageType = {DamageTypeToString(pgPermanentModEffectEx.DamageType)},");

        if (!float.IsNaN(pgPermanentModEffectEx.RandomChance))
            Write("                     ", $" RandomChance = {pgPermanentModEffectEx.RandomChance.ToString(CultureInfo.InvariantCulture)}F,");

        if (!float.IsNaN(pgPermanentModEffectEx.DelayInSeconds))
            Write("                     ", $" DelayInSeconds = {pgPermanentModEffectEx.DelayInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgPermanentModEffectEx.DurationInSeconds))
            Write("                     ", $" DurationInSeconds = {pgPermanentModEffectEx.DurationInSeconds.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgPermanentModEffectEx.RecurringDelay))
            Write("                     ", $" RecurringDelay = {pgPermanentModEffectEx.RecurringDelay.ToString(CultureInfo.InvariantCulture)},");

        Write("                     ", $" Target = CombatTarget.{pgPermanentModEffectEx.Target},");

        if (pgPermanentModEffectEx.ConditionList.Count > 0)
            Write("                     ", $" ConditionList = new PgCombatConditionCollectionEx() {{ {ConditionListToString(pgPermanentModEffectEx.ConditionList)} }},");

        if (pgPermanentModEffectEx.ConditionAbilityList.Count > 0)
            Write("                     ", $" ConditionAbilityList = new List<AbilityKeyword>() {{ {AbilityKeywordListToString(pgPermanentModEffectEx.ConditionAbilityList)} }},");

        Write("                 ", " },");
    }

    private static void Display(PgCombatModEffectEx pgCombatModEffectEx)
    {
        Write("                 ", $" new {pgCombatModEffectEx.GetType().Name}()");
        Write("                 ", " {");
        Write("                     ", $" Keyword = {pgCombatModEffectEx.Keyword.GetType().Name}.{pgCombatModEffectEx.Keyword},");

        if (pgCombatModEffectEx.AbilityList.Count > 0)
            Write("                     ", $" AbilityList = new List<AbilityKeyword>() {{ {AbilityKeywordListToString(pgCombatModEffectEx.AbilityList)} }},");

        Write("                     ", $" Data = {NumericValueToString(pgCombatModEffectEx.Data)},");

        if (pgCombatModEffectEx.DamageType != GameDamageType.Internal_None)
            Write("                     ", $" DamageType = {DamageTypeToString(pgCombatModEffectEx.DamageType)},");

        if (pgCombatModEffectEx.DamageCategory != GameDamageCategory.Internal_None)
            Write("                     ", $" DamageCategory = {DamageCategoryToString(pgCombatModEffectEx.DamageCategory)},");

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

        if (pgCombatModEffectEx.TargetAbilityList.Count > 0)
            Write("                     ", $" TargetAbilityList = new List<AbilityKeyword>() {{ {AbilityKeywordListToString(pgCombatModEffectEx.TargetAbilityList)} }},");

        if (pgCombatModEffectEx.ConditionList.Count > 0)
            Write("                     ", $" ConditionList = new PgCombatConditionCollectionEx() {{ {ConditionListToString(pgCombatModEffectEx.ConditionList)} }},");

        if (pgCombatModEffectEx.ConditionAbilityList.Count > 0)
            Write("                     ", $" ConditionAbilityList = new List<AbilityKeyword>() {{ {AbilityKeywordListToString(pgCombatModEffectEx.ConditionAbilityList)} }},");

        if (!float.IsNaN(pgCombatModEffectEx.ConditionValue))
            Write("                     ", $" ConditionValue = {pgCombatModEffectEx.ConditionValue.ToString(CultureInfo.InvariantCulture)},");

        if (!float.IsNaN(pgCombatModEffectEx.ConditionPercentage))
            Write("                     ", $" ConditionPercentage = {pgCombatModEffectEx.ConditionPercentage.ToString(CultureInfo.InvariantCulture)},");

        if (pgCombatModEffectEx.IsEveryOtherUse)
            Write("                     ", $" IsEveryOtherUse = true,");

        Write("                 ", " },");
    }

    private static string Encoded(string text)
    {
        return text.Replace("\"", "\\\"")
                   .Replace("\r", string.Empty)
                   .Replace("\n", " ");
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

    private static string ConditionListToString(PgCombatConditionCollectionEx list)
    {
        string Result = string.Empty;

        foreach (CombatCondition Condition in list)
        {
            if (Result.Length > 0)
                Result += ", ";

            Result += $"CombatCondition.{Condition}";
        }

        return Result;
    }

    private static string DamageTypeToString(GameDamageType damageType)
    {
        string Result = string.Empty;

        foreach (GameDamageType EnumValue in Enum.GetValues(typeof(GameDamageType)))
            if (EnumValue != GameDamageType.Internal_None && (damageType & EnumValue) == EnumValue)
            {
                if (Result.Length > 0)
                    Result += " | ";
                Result += $"GameDamageType.{EnumValue}";
            }

        return Result;
    }

    private static string DamageCategoryToString(GameDamageCategory damageCategory)
    {
        string Result = string.Empty;

        foreach (GameDamageCategory EnumValue in Enum.GetValues(typeof(GameDamageCategory)))
            if (EnumValue != GameDamageCategory.Internal_None && (damageCategory & EnumValue) == EnumValue)
            {
                if (Result.Length > 0)
                    Result += " | ";
                Result += $"GameDamageCategory.{EnumValue}";
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
