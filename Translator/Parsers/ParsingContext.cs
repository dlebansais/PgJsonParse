namespace Translator
{
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParsingContext
    {
        public ParsingContext(Parser parser, FieldTable fieldTable)
        {
            Parser = parser;
            FieldTable = fieldTable;
        }

        public Parser Parser { get; }
        public FieldTable FieldTable { get; }
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

        public bool FinishItem()
        {
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
    }
}
