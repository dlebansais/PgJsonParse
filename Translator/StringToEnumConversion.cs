namespace Translator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class StringToEnumConversion
    {
        public static Dictionary<Type, bool[]> KnownParsedEnumtable { get; } = new Dictionary<Type, bool[]>();
        public static Dictionary<Type, List<string>> MissingEnumTable { get; } = new Dictionary<Type, List<string>>();

        public static bool FinalizeParsing()
        {
            if (!FinalizeMissing())
                return false;

            if (!FinalizeKnownParsed())
                return false;

            return true;
        }

        private static bool FinalizeKnownParsed()
        {
            if (KnownParsedEnumtable.Count != 74)
            {
                List<string> NameList = new List<string>();
                foreach (KeyValuePair<Type, bool[]> Entry in KnownParsedEnumtable)
                {
                    string Name = Entry.Key.Name;
                    NameList.Add(Name);
                }

                NameList.Sort();

                foreach (string Name in NameList)
                    Debug.WriteLine(Name);

                return Program.ReportFailure("Some enum types are not used anymore");
            }

            string Result = string.Empty;

            foreach (KeyValuePair<Type, bool[]> Entry in KnownParsedEnumtable)
                FinalizeKnownParsed(Entry.Key, Entry.Value, ref Result);

            if (Result.Length > 0)
                return Program.ReportFailure($"The following enums are not used.\n\n{Result}");

            return true;
        }

        private static void FinalizeKnownParsed(Type type, bool[] usedValues, ref string text)
        {
            Debug.Assert(type.IsEnum);

            string[] EnumNames = type.GetEnumNames();
            Debug.Assert(EnumNames.Length == usedValues.Length);

            string EnumText = string.Empty;
            for (int i = 1; i < usedValues.Length; i++)
                if (!usedValues[i])
                    EnumText += $"    {EnumNames[i]}\n";

            if (EnumText.Length > 0)
            {
                if (text.Length > 0)
                    text += "\n";

                text += $"  {type.Name}:\n{EnumText}";
            }
        }

        private static bool FinalizeMissing()
        {
            string Result = string.Empty;

            foreach (KeyValuePair<Type, List<string>> Entry in MissingEnumTable)
                FinalizeMissing(Entry.Key, Entry.Value, ref Result);

            if (Result.Length > 0)
                return Program.ReportFailure($"The following enums are missing.\n\n{Result}");

            return true;
        }

        private static void FinalizeMissing(Type type, List<string> missingValues, ref string text)
        {
            Debug.Assert(type.IsEnum);

            string EnumText = string.Empty;
            for (int i = 0; i < missingValues.Count; i++)
            {
                string EnumName = missingValues[i];
                EnumText += $"    {EnumName},\n";
            }

            if (EnumText.Length > 0)
            {
                if (text.Length > 0)
                    text += "\n";

                text += $"  {type.Name}:\n{EnumText}";
            }
        }
    }

    public static class StringToEnumConversion<T>
    {
        #region Single
        public static bool TryParse(string stringValue, out T enumValue, ErrorControl errorControl = ErrorControl.Normal)
        {
            Dictionary<T, string> stringMap = EnumStringMap.Tables.ContainsKey(typeof(T)) ? (Dictionary<T, string>)EnumStringMap.Tables[typeof(T)] : null;

            return TryParse(stringValue, stringMap, default(T), default(T), out enumValue, errorControl);
        }

        public static bool TryParse(string stringValue, T defaultValue, T emptyValue, out T enumValue, ErrorControl errorControl = ErrorControl.Normal)
        {
            Dictionary<T, string> stringMap = EnumStringMap.Tables.ContainsKey(typeof(T)) ? (Dictionary<T, string>)EnumStringMap.Tables[typeof(T)] : null;

            return TryParse(stringValue, stringMap, defaultValue, emptyValue, out enumValue, errorControl);
        }

        public static bool SetEnum(Action<T> setter, object value)
        {
            string EnumString;

            if (value is string ValueString)
                EnumString = ValueString;
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

        private static bool TryParse(string stringValue, Dictionary<T, string> stringMap, T defaultValue, T emptyValue, out T enumValue, ErrorControl errorControl)
        {
            enumValue = defaultValue;

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

            if (stringValue.Length == 0 && !emptyValue.Equals(defaultValue) && TryFindIndex(emptyValue, out EnumIndex))
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

            if (stringMap != null)
                foreach (KeyValuePair<T, string> Entry in stringMap)
                    if (stringValue == Entry.Value)
                    {
                        enumValue = Entry.Key;
                        if (TryFindIndex(enumValue, out EnumIndex))
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

        public static void SetCustomParsedEnum(T EnumValue)
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

            if (TryFindIndex(EnumValue, out int EnumIndex))
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

            enumValue = default(T);
            enumIndex = -1;
            return false;
        }

        public static bool TryFindIndex(T enumValue, out int enumIndex)
        {
            Array EnumValues = Enum.GetValues(typeof(T));

            for (int i = 0; i < EnumValues.Length; i++)
                if ((int)EnumValues.GetValue(i) == (int)(object)enumValue)
                {
                    enumIndex = i;
                    return true;
                }

            enumIndex = -1;
            return false;
        }
        #endregion
    }
}
