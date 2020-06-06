namespace Translator
{
    using System;
    using System.Collections.Generic;

    public static class Inserter<T>
    {
        public static bool SetItemByKey(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
        {
            return SetKeyedItem(MatchingByKey, setter, value, errorControl);
        }

        private static bool MatchingByKey(string key, ParsingContext context, string valueKey)
        {
            return key == valueKey;
        }

        public static bool SetItemByInternalName(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
        {
            return SetKeyedItem(MatchingByInternalName, setter, value, errorControl);
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

        public static bool SetItemByName(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
        {
            return SetKeyedItem(MatchingByName, setter, value, errorControl);
        }

        private static bool MatchingByName(string key, ParsingContext context, string valueKey)
        {
            Dictionary<string, object> ContentTable = context.ContentTable;

            if (!ContentTable.ContainsKey("Name"))
                return false;

            if (!(ContentTable["Name"] is string Name))
                return false;

            return Name == valueKey;
        }

        private static bool SetKeyedItem(Func<string, ParsingContext, string, bool> match, Action<T> setter, object value, ErrorControl errorControl)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure($"Value '{value}' was expected to be a string");

            Type LinkType = typeof(T);
            if (!ParsingContext.KeyedObjectTable.ContainsKey(LinkType))
                return Program.ReportFailure($"Type {LinkType} does not have items with keys");

            Dictionary<string, ParsingContext> KeyTable = ParsingContext.KeyedObjectTable[LinkType];

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
                if (errorControl == ErrorControl.IgnoreIfNotFound)
                    return false;
                else
                    return Program.ReportFailure($"Key '{ValueKey}' is not a known key");

            if (!(KeyTable[MatchingKey].Item is T AsLink))
                return Program.ReportFailure($"Key '{ValueKey}' was found but for the wrong object type");

            setter(AsLink);
            return true;
        }

        public static bool SetItemById(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
        {
            return SetKeylessItem(MatchingById, setter, value, errorControl);
        }

        private static bool MatchingById(ParsingContext context, int valueId)
        {
            Dictionary<string, object> ContentTable = context.ContentTable;

            if (!ContentTable.ContainsKey("Id"))
                return false;

            if (!(ContentTable["Id"] is int Id))
                return false;

            return Id == valueId;
        }

        private static bool SetKeylessItem(Func<ParsingContext, int, bool> match, Action<T> setter, object value, ErrorControl errorControl)
        {
            if (!(value is int ValueId))
                return Program.ReportFailure($"Value '{value}' was expected to be an Id");

            Type LinkType = typeof(T);
            if (!ParsingContext.KeylessObjectTable.ContainsKey(LinkType))
                return Program.ReportFailure($"Type {LinkType} does not have recorded items");

            List<ParsingContext> KeylessContextList = ParsingContext.KeylessObjectTable[LinkType];

            ParsingContext MatchingContext = null;
            foreach (ParsingContext Context in KeylessContextList)
                if (match(Context, ValueId))
                {
                    MatchingContext = Context;
                    break;
                }

            if (MatchingContext == null)
                if (errorControl == ErrorControl.IgnoreIfNotFound)
                    return false;
                else
                    return Program.ReportFailure($"'{ValueId}' is not a known ID");

            if (!(MatchingContext.Item is T AsLink))
                return Program.ReportFailure($"Key '{ValueId}' was found but for the wrong object type");

            setter(AsLink);
            return true;
        }

        public static bool AddArray(List<T> linkList, object value)
        {
            if (!(value is List<object> ArrayKey))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            Type LinkType = typeof(T);
            if (!ParsingContext.KeyedObjectTable.ContainsKey(LinkType))
                return Program.ReportFailure($"Type {LinkType} does not have items with keys");

            Dictionary<string, ParsingContext> KeyTable = ParsingContext.KeyedObjectTable[LinkType];

            foreach (object Item in ArrayKey)
            {
                if (!(Item is string ValueKey))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                if (!KeyTable.ContainsKey(ValueKey))
                    return Program.ReportFailure($"Key '{Item}' is not a known key");

                if (!(KeyTable[ValueKey].Item is T AsLink))
                    return Program.ReportFailure($"Key '{Item}' was found but for the wrong object type");

                linkList.Add(AsLink);
            }

            return true;
        }

        public static bool AddKeylessArray(List<T> linkList, object value)
        {
            if (value is List<object> ArrayObject)
            {
                foreach (object Item in ArrayObject)
                {
                    if (!(Item is ParsingContext ItemContext))
                        return Program.ReportFailure($"Value '{Item}' was expected to be a context");

                    if (ItemContext.Item == null)
                        ItemContext.FinishItem();

                    if (!(ItemContext.Item is T ValueObject))
                        return Program.ReportFailure($"Item was found but for the wrong object type");

                    linkList.Add(ValueObject);
                }
            }
            else if (value is ParsingContext ItemContext)
            {
                if (ItemContext.Item == null)
                    ItemContext.FinishItem();

                if (!(ItemContext.Item is T ValueObject))
                    return Program.ReportFailure($"Item was found but for the wrong object type");

                linkList.Add(ValueObject);
            }
            else
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

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
                return Program.ReportFailure($"Value '{value}' was expected to be a string");

            if (!StringToEnumConversion<T>.TryParse(EnumString, out T Parsed))
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

            if (!StringToEnumConversion<T>.TryParse(EnumString, defaultValue, emptyValue, out T Parsed))
                return false;

            setter(Parsed);
            return true;
        }

        public static bool SetItemProperty(Action<T> setter, object value)
        {
            if (!(value is ParsingContext Context))
                return Program.ReportFailure($"Value '{value}' was expected to be a context");

            if (Context.Item == null)
                Context.FinishItem();

            if (!(Context.Item is T AsItem))
                return Program.ReportFailure($"Item was found but for the wrong object type");

            setter(AsItem);
            return true;
        }
    }
}
