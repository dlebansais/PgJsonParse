using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class StringToEnumConversion
    {
        public static Dictionary<Type, bool[]> KnownParsedEnumtable { get; private set; } = new Dictionary<Type, bool[]>();
    }

    public static class StringToEnumConversion<T>
    {
        public static bool TryParse(string StringValue, out T EnumValue, ParseErrorInfo ErrorInfo)
        {
            return TryParse(StringValue, null, out EnumValue, ErrorInfo);
        }

        public static bool TryParse(string StringValue, Dictionary<T, string> StringMap, out T EnumValue, ParseErrorInfo ErrorInfo)
        {
            return TryParse(StringValue, StringMap, default(T), out EnumValue, ErrorInfo);
        }

        public static bool TryParse(string StringValue, Dictionary<T, string> StringMap, T DefaultValue, out T EnumValue, ParseErrorInfo ErrorInfo)
        {
            return TryParse(StringValue, StringMap, DefaultValue, DefaultValue, out EnumValue, ErrorInfo);
        }

        public static bool TryParse(string StringValue, Dictionary<T, string> StringMap, T DefaultValue, T EmptyValue, out T EnumValue, ParseErrorInfo ErrorInfo)
        {
            EnumValue = DefaultValue;
            string TrimmedStringValue = StringValue;
            string[] EnumNames = typeof(T).GetEnumNames();
            Array EnumValues = typeof(T).GetEnumValues();

            if (StringValue == null)
                return false;

            bool[] ParsedEnums;
            if (!StringToEnumConversion.KnownParsedEnumtable.ContainsKey(typeof(T)))
            {
                ParsedEnums = new bool[EnumNames.Length];
                StringToEnumConversion.KnownParsedEnumtable.Add(typeof(T), ParsedEnums);
            }
            else
                ParsedEnums = StringToEnumConversion.KnownParsedEnumtable[typeof(T)];

            if (StringValue.Length == 0 && !EmptyValue.Equals(DefaultValue))
            {
                for (int i = 0; i < EnumValues.Length; i++)
                    if ((int)EnumValues.GetValue(i) == (int)(object)EmptyValue)
                    {
                        ParsedEnums[i] = true;
                        break;
                    }

                EnumValue = EmptyValue;
                return true;
            }

            bool Found = false;

            for (int i = 0; i < EnumNames.Length; i++)
            {
                string EnumName = EnumNames[i];
                if (TrimmedStringValue == EnumName)
                {
                    EnumValue = (T)EnumValues.GetValue(i);
                    ParsedEnums[i] = true;
                    Found = true;
                    break;
                }
            }

            if (!Found && StringMap != null)
            {
                foreach (KeyValuePair<T, string> Entry in StringMap)
                {
                    if (TrimmedStringValue == Entry.Value)
                    {
                        EnumValue = Entry.Key;
                        Found = true;

                        for (int i = 0; i < EnumValues.Length; i++)
                            if ((int)EnumValues.GetValue(i) == (int)(object)EnumValue)
                            {
                                ParsedEnums[i] = true;
                                break;
                            }

                        break;
                    }
                }
            }

            if (Found)
                return true;

            if (ErrorInfo != null)
                ErrorInfo.AddMissingEnum(typeof(T).Name, StringValue);

            return false;
        }

        public static void ParseList(JsonArray StringArray, List<T> EnumList, ParseErrorInfo ErrorInfo)
        {
            ParseList(StringArray, null, EnumList, ErrorInfo);
        }

        public static void ParseList(string[] StringArray, List<T> EnumList, ParseErrorInfo ErrorInfo)
        {
            ParseList(StringArray, null, EnumList, ErrorInfo);
        }

        public static void ParseList(JsonArray StringArray, Dictionary<T, string> StringMap, List<T> EnumList, ParseErrorInfo ErrorInfo)
        {
            if (StringArray == null)
                return;

            foreach (object Item in StringArray)
            {
                JsonString AsJsonString;
                if ((AsJsonString = Item as JsonString) != null)
                {
                    T ParsedValue;
                    if (TryParse(AsJsonString.String, StringMap, out ParsedValue, ErrorInfo))
                        EnumList.Add(ParsedValue);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Enum List");
            }
        }

        public static void ParseList(string[] StringArray, Dictionary<T, string> StringMap, List<T> EnumList, ParseErrorInfo ErrorInfo)
        {
            if (StringArray == null)
                return;

            foreach (string s in StringArray)
            {
                T ParsedValue;
                if (StringToEnumConversion<T>.TryParse(s, StringMap, out ParsedValue, ErrorInfo))
                    EnumList.Add(ParsedValue);
            }
        }

        public static string ToString(T EnumValue)
        {
            return EnumValue.ToString();
        }

        public static string ToString(T EnumValue, Dictionary<T, string> StringMap)
        {
            if (StringMap != null)
                foreach (KeyValuePair<T, string> Entry in StringMap)
                    if (EnumValue.Equals(Entry.Key))
                        return Entry.Value;

            return ToString(EnumValue);
        }

        public static string ToString(T EnumValue, Dictionary<T, string> StringMap, T DefaultValue)
        {
            if (EnumValue.Equals(DefaultValue))
                return null;

            return ToString(EnumValue, StringMap);
        }

        public static string ToString(T EnumValue, Dictionary<T, string> StringMap, T DefaultValue, T EmptyValue)
        {
            if (EnumValue.Equals(EmptyValue))
                return "";

            return ToString(EnumValue, StringMap, DefaultValue);
        }

        public static void ListToString(JsonGenerator Generator, string ArrayName, List<T> EnumList)
        {
            ListToString(Generator, ArrayName, EnumList, null);
        }

        public static void ListToString(JsonGenerator Generator, string ArrayName, List<T> EnumList, Dictionary<T, string> StringMap)
        {
            if (EnumList.Count > 0)
            {
                Generator.OpenArray(ArrayName);

                foreach (T Item in EnumList)
                    Generator.AddString(null, ToString(Item, StringMap));

                Generator.CloseArray();
            }
        }
    }
}
