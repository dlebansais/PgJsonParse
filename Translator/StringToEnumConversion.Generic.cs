﻿namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class StringToEnumConversion<T>
{
    #region Single
    public static bool TryParse(string stringValue, out T enumValue, ErrorControl errorControl = ErrorControl.Normal)
    {
        return TryParse(stringValue, default(T)!, default(T)!, out enumValue, errorControl);
    }

    public static bool SetEnum(Action<T> setter, object value)
    {
        string EnumString;

        if (value is string ValueString)
        {
            if (ValueString == "0")
            {
                setter(default(T)!);
                return true;
            }

            EnumString = ValueString;
        }
        else if (value is List<object> ValueList && ValueList.Count == 1 && ValueList[0] is string ItemString)
            EnumString = ItemString;
        else
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        if (!TryParse(EnumString, out T Parsed))
            return false;

        setter(Parsed);
        return true;
    }

    public static bool SetEnum(Action<T> setter, T defaultValue, T emptyValue, object value)
    {
        string EnumString;

        if (value is string ValueString)
            EnumString = ValueString;
        else if (value is List<object> ValueList && ValueList.Count == 1 && ValueList[0] is string ItemString)
            EnumString = ItemString;
        else
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        if (!TryParse(EnumString, defaultValue, emptyValue, out T Parsed))
            return false;

        setter(Parsed);
        return true;
    }

    private static bool TryParse(string stringValue, T defaultValue, T emptyValue, out T enumValue, ErrorControl errorControl = ErrorControl.Normal)
    {
        bool[] ParsedEnums;
        if (!StringToEnumConversion.KnownParsedEnumtable.ContainsKey(typeof(T)))
        {
            string[] EnumNames = Enum.GetNames(typeof(T));
            ParsedEnums = new bool[EnumNames.Length];
            StringToEnumConversion.KnownParsedEnumtable.Add(typeof(T), ParsedEnums);
        }
        else
            ParsedEnums = StringToEnumConversion.KnownParsedEnumtable[typeof(T)];

        int EnumIndex;

        if (stringValue.Length == 0 && emptyValue != null && !emptyValue.Equals(defaultValue) && TryFindIndex(emptyValue, out EnumIndex))
        {
            ParsedEnums[EnumIndex] = true;
            enumValue = emptyValue;
            return true;
        }

        if (TryParseEnum(stringValue, out enumValue, out EnumIndex))
        {
            ParsedEnums[EnumIndex] = true;
            return true;
        }

        if (errorControl == ErrorControl.Normal)
        {
            if (!StringToEnumConversion.MissingEnumTable.ContainsKey(typeof(T)))
                StringToEnumConversion.MissingEnumTable.Add(typeof(T), new List<string>());

            List<string> MissingEnumList = StringToEnumConversion.MissingEnumTable[typeof(T)];
            if (!MissingEnumList.Contains(stringValue))
                MissingEnumList.Add(stringValue);

            return true;
        }

        string Warning = $"Enum '{stringValue}' not found for {typeof(T)}";
        return Program.ReportFailure(Warning, errorControl);
    }

    public static void SetCustomParsedEnum(T enumValue)
    {
        bool[] ParsedEnums;
        if (!StringToEnumConversion.KnownParsedEnumtable.ContainsKey(typeof(T)))
        {
            string[] EnumNames = Enum.GetNames(typeof(T));
            ParsedEnums = new bool[EnumNames.Length];
            StringToEnumConversion.KnownParsedEnumtable.Add(typeof(T), ParsedEnums);
        }
        else
            ParsedEnums = StringToEnumConversion.KnownParsedEnumtable[typeof(T)];

        if (TryFindIndex(enumValue, out int EnumIndex))
            ParsedEnums[EnumIndex] = true;
    }
    #endregion

    #region List
    public static bool TryParseList(object value, ICollection<T> list)
    {
        if (!(value is List<object> StringArray))
            return Program.ReportFailure($"Value {value} was expected to be a list");

        foreach (object Item in StringArray)
        {
            if (!(Item is string StringItem))
                return Program.ReportFailure($"Value {Item} was expected to be a string");

            if (!TryParse(StringItem, out T Parsed))
                return false;

            list.Add(Parsed);
        }

        return true;
    }
    #endregion

    #region Enum Management
    public static bool TryParseEnum(string s, out T enumValue, out int enumIndex)
    {
        if (!string.IsNullOrEmpty(s))
        {
            string[] EnumNames = Enum.GetNames(typeof(T));
            Array EnumValues = Enum.GetValues(typeof(T));

            for (int i = 0; i < EnumNames.Length; i++)
                if (s == EnumNames[i])
                {
                    enumValue = (T)EnumValues.GetValue(i);
                    enumIndex = i;
                    return true;
                }
        }

        enumValue = default(T)!;
        enumIndex = -1;
        return false;
    }

    public static bool TryFindIndex(T enumValue, out int enumIndex)
    {
        Array EnumValues = Enum.GetValues(typeof(T));

        for (int i = 0; i < EnumValues.Length; i++)
            if ((int)EnumValues.GetValue(i) == (int)(object)enumValue!)
            {
                enumIndex = i;
                return true;
            }

        enumIndex = -1;
        return false;
    }
    #endregion
}
