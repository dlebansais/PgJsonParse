namespace Translator
{
    using System;
    using System.Collections.Generic;

    public static class Inserter<T>
    {
        public static bool SetItemByKey(Action<T> setter, object value)
        {
            return SetItem(MatchingByKey, setter, value);
        }

        private static bool MatchingByKey(string key, ParsingContext context, string valueKey)
        {
            return key == valueKey;
        }

        public static bool SetItemByInternalName(Action<T> setter, object value)
        {
            return SetItem(MatchingByInternalName, setter, value);
        }

        private static bool MatchingByInternalName(string key, ParsingContext context, string valueKey)
        {
            Dictionary<string, object> ContentTable = context.ContentTable;

            if (!ContentTable.ContainsKey("InternalName"))
                return false;

            if (!(ContentTable["InternalName"] is string InternalName))
                return false;

            return InternalName == valueKey;
        }

        private static bool SetItem(Func<string, ParsingContext, string, bool> match, Action<T> setter, object value)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure($"Value {value} was expected to be a string");

            Type LinkType = typeof(T);
            if (!ParsingContext.ObjectKeyTable.ContainsKey(LinkType))
                return Program.ReportFailure($"Type {LinkType} does not have items with keys");

            Dictionary<string, ParsingContext> KeyTable = ParsingContext.ObjectKeyTable[LinkType];

            string MatchingKey = string.Empty;

            foreach (KeyValuePair<string, ParsingContext> Entry in KeyTable)
            {
                string Key = Entry.Key;
                ParsingContext Context = Entry.Value;

                if (match(Key, Context, ValueKey))
                {
                    MatchingKey = Key;
                    break;
                }
            }

            if (MatchingKey.Length == 0)
                return Program.ReportFailure($"Key {ValueKey} is not a known key");

            if (!(KeyTable[MatchingKey].Item is T AsLink))
                return Program.ReportFailure($"Key {ValueKey} was found but for the wrong object type");

            setter(AsLink);
            return true;
        }

        public static bool AddArray(List<T> linkList, object value)
        {
            if (!(value is List<object> ArrayKey))
                return Program.ReportFailure($"Value {value} was expected to be a list");

            Type LinkType = typeof(T);
            if (!ParsingContext.ObjectKeyTable.ContainsKey(LinkType))
                return Program.ReportFailure($"Type {LinkType} does not have items with keys");

            Dictionary<string, ParsingContext> KeyTable = ParsingContext.ObjectKeyTable[LinkType];

            foreach (object Item in ArrayKey)
            {
                if (!(Item is string ValueKey))
                    return Program.ReportFailure($"Value {Item} was expected to be a string");

                if (!KeyTable.ContainsKey(ValueKey))
                    return Program.ReportFailure($"Key {Item} is not a known key");

                if (!(KeyTable[ValueKey].Item is T AsLink))
                    return Program.ReportFailure($"Key {Item} was found but for the wrong object type");

                linkList.Add(AsLink);
            }

            return true;
        }

        public static bool AddKeylessArray(List<T> linkList, object value)
        {
            if (!(value is List<object> ArrayObject))
                return Program.ReportFailure($"Value {value} was expected to be a list");

            foreach (object Item in ArrayObject)
            {
                if (!(Item is T ValueObject))
                    return Program.ReportFailure($"Value {Item} was expected to be a string");

                linkList.Add(ValueObject);
            }

            return true;
        }

        public static bool SetEnum(Action<T> setter, object value)
        {
            string EnumString;

            if (value is string ValueString)
                EnumString = ValueString;
            else if (value is List<object> ValueList && ValueList.Count == 1 && ValueList[0] is string ItemString)
                EnumString = ItemString;
            else
                return Program.ReportFailure($"Value {value} was expected to be a string");

            if (!StringToEnumConversion<T>.TryParse(EnumString, out T Parsed))
                return false;

            setter(Parsed);
            return true;
        }
    }
}
