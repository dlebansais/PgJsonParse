namespace Translator
{
    using PgJsonReader;
    using PgObjects;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParsingContext
    {
        public ParsingContext(Parser parser, Type objectType, FieldTable fieldTable, string objectKey)
        {
            Parser = parser;
            ObjectType = objectType;
            FieldTable = fieldTable;
            ObjectKey = objectKey;
            Item = Parser.CreateItem();

            ParsedFile = Program.LastParsedFile;
            Debug.Assert(ParsedFile.Length > 0);
            ParsedKey = Program.LastParsedKey;
            Debug.Assert(ParsedKey.Length > 0);
        }

        public Parser Parser { get; }
        public Type ObjectType { get; }
        public FieldTable FieldTable { get; }
        public string ObjectKey { get; }
        public object Item { get; private set; }
        public string ParsedFile { get; }
        public string ParsedKey { get; }
        public Dictionary<string, object> ContentTable { get; } = new Dictionary<string, object>();
        public Dictionary<string, Json.Token> ContentTypeTable { get; } = new Dictionary<string, Json.Token>();
        public List<object> ItemCollection { get; } = new List<object>();
        public Json.Token LastItemType { get; private set; } = Json.Token.Null;

        public bool SetContent(string key, object value, Json.Token jsonType)
        {
            if (ContentTable.ContainsKey(key))
                return Program.ReportFailure($"Key '{key}' already parsed for object");

            Debug.Assert(!ContentTypeTable.ContainsKey(key));

            ContentTable.Add(key, value);
            ContentTypeTable.Add(key, jsonType);

            return true;
        }

        public bool RecordContext()
        {
            ContextList.Add(this);

            if (ObjectKey.Length > 0)
            {
                if (!RecordContextInStringTable(ObjectKeyTable, ObjectKey, ErrorControl.Normal))
                    return false;

                if (ContentTable.ContainsKey("Name") && FieldTableStore.IsTypeWithName(ObjectType))
                    if (!RecordContextInStringTable(ObjectNameTable, ContentTable["Name"], ErrorControl.IgnoreIfFound))
                        return false;

                if (ContentTable.ContainsKey("InternalName") && FieldTableStore.IsTypeWithInternalName(ObjectType))
                    if (!RecordContextInStringTable(ObjectInternalNameTable, ContentTable["InternalName"], ErrorControl.Normal))
                        return false;
            }
            else
            {
                if (!KeylessObjectTable.ContainsKey(ObjectType))
                    KeylessObjectTable.Add(ObjectType, new List<ParsingContext>());

                List<ParsingContext> TypedContextList = KeylessObjectTable[ObjectType];
                TypedContextList.Add(this);
            }

            if (ContentTable.ContainsKey("Id"))
                if (!RecordContextInIntTable(ObjectIdTable, ContentTable["Id"]))
                    return false;

            return true;
        }

        public bool RecordContextInStringTable(Dictionary<Type, Dictionary<string, ParsingContext>> objectTable, object key, ErrorControl errorControl)
        {
            if (!(key is string KeyString))
                return Program.ReportFailure($"Key '{key}' was expected to be a string");

            if (!objectTable.ContainsKey(ObjectType))
                objectTable.Add(ObjectType, new Dictionary<string, ParsingContext>());

            Dictionary<string, ParsingContext> KeyTable = objectTable[ObjectType];
            if (KeyTable.ContainsKey(KeyString))
            {
                if (errorControl == ErrorControl.IgnoreIfFound)
                    return true;

                return Program.ReportFailure($"Key '{KeyString}' already used for type '{ObjectType}'");
            }

            KeyTable.Add(KeyString, this);

            return true;
        }

        public bool RecordContextInIntTable(Dictionary<Type, Dictionary<int, ParsingContext>> objectTable, object key)
        {
            if (!(key is int KeyInt))
                return Program.ReportFailure($"Key '{key}' was expected to be an int");

            if (!objectTable.ContainsKey(ObjectType))
                objectTable.Add(ObjectType, new Dictionary<int, ParsingContext>());

            Dictionary<int, ParsingContext> KeyTable = objectTable[ObjectType];
            if (KeyTable.ContainsKey(KeyInt))
                return Program.ReportFailure($"Key '{key}' already used for type '{ObjectType}'");

            KeyTable.Add(KeyInt, this);

            return true;
        }

        public bool FinishItem()
        {
            object UpdatedItem = Item;

            if (!Parser.FinishItem(ref UpdatedItem, ObjectKey, ContentTable, ContentTypeTable, ItemCollection, LastItemType, ParsedFile, ParsedKey))
                return false;

            Item = UpdatedItem;
            return true;
        }

        public void StartArray()
        {
            ItemCollection.Clear();
        }

        public bool FinishArrayItem()
        {
            if (ContentTable.Count > 1)
                return Program.ReportFailure("Unexpected failure");

            Debug.Assert(ContentTypeTable.Count == ContentTable.Count);

            foreach (KeyValuePair<string, object> Entry in ContentTable)
            {
                string Key = Entry.Key;
                Debug.Assert(ContentTypeTable.ContainsKey(Key));

                if (ItemCollection.Count > 0)
                    if (ContentTypeTable[Key] != LastItemType)
                        return Program.ReportFailure("Mixed array types");

                ItemCollection.Add(Entry.Value);
                LastItemType = ContentTypeTable[Key];
            }

            ContentTable.Clear();
            ContentTypeTable.Clear();
            return true;
        }

        public bool FinishArray(string key, ParsingContext arrayContext)
        {
            if (ContentTable.ContainsKey(key))
                return Program.ReportFailure($"Key '{key}' already parsed for object");

            Debug.Assert(!ContentTypeTable.ContainsKey(key));

            ContentTable.Add(key, arrayContext.ItemCollection);
            ContentTypeTable.Add(key, arrayContext.LastItemType);

            return true;
        }

        public static List<ParsingContext> ContextList { get; } = new List<ParsingContext>();
        public static Dictionary<Type, Dictionary<string, ParsingContext>> ObjectKeyTable { get; } = new Dictionary<Type, Dictionary<string, ParsingContext>>();
        public static Dictionary<Type, Dictionary<string, ParsingContext>> ObjectNameTable { get; } = new Dictionary<Type, Dictionary<string, ParsingContext>>();
        public static Dictionary<Type, Dictionary<string, ParsingContext>> ObjectInternalNameTable { get; } = new Dictionary<Type, Dictionary<string, ParsingContext>>();
        public static Dictionary<Type, Dictionary<int, ParsingContext>> ObjectIdTable { get; } = new Dictionary<Type, Dictionary<int, ParsingContext>>();
        public static Dictionary<Type, List<ParsingContext>> KeylessObjectTable { get; } = new Dictionary<Type, List<ParsingContext>>();
        private static List<object> SuplementaryObjectList { get; } = new List<object>();

        public static bool FinalizeParsing()
        {
            foreach (ParsingContext Context in ContextList)
                if (!Context.FinishItem())
                    return false;

            return true;
        }

        public static void AddSuplementaryObject(object item)
        {
            SuplementaryObjectList.Add(item);
        }

        public static List<object> GetParsedObjectList()
        {
            List<object> Result = new List<object>();

            foreach (ParsingContext Context in ContextList)
                if (Context.Item is IEnumerable<object> AsObjects)
                    Result.AddRange(AsObjects);
                else
                    Result.Add(Context.Item);

            Result.AddRange(SuplementaryObjectList);

            return Result;
        }
    }
}
