namespace Translator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class StringToEnumConversion
    {
        public static Dictionary<Type, bool[]> KnownParsedEnumtable { get; } = new Dictionary<Type, bool[]>();
        public static Dictionary<Type, List<string>> MissingEnumTable { get; } = new Dictionary<Type, List<string>>();
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
                if (MissingEnumList.Contains(stringValue))
                    MissingEnumList.Add(stringValue);

                //TODO: finalize
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
