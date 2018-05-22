using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IGenericJsonObject
    {
        string Key { get; }
        bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables);
        void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo);
        void SortLinkBack();
        string TextContent { get; }
    }

    public abstract class GenericJsonObject
    {
        protected enum FieldType
        {
            Integer,
            Bool,
            Float,
            String,
            Object,
            SimpleIntegerArray,
            IntegerArray,
            SimpleStringArray,
            StringArray,
            ObjectArray,
        };

        protected struct FieldParser
        {
            public FieldType Type;
            public Action<int, ParseErrorInfo> ParserInteger;
            public Action<bool, ParseErrorInfo> ParserBool;
            public Action<float, ParseErrorInfo> ParserFloat;
            public Action<string, ParseErrorInfo> ParserString;
            public Action<JsonObject, ParseErrorInfo> ParserObject;
            public Action<int, ParseErrorInfo> ParserSimpleIntegerArray;
            public Func<int, ParseErrorInfo, bool> ParserIntegerArray;
            public Action<string, ParseErrorInfo> ParserSimpleStringArray;
            public Func<string, ParseErrorInfo, bool> ParserStringArray;
            public Action<JsonObject, ParseErrorInfo> ParserObjectArray;
            public Action ParserSetArrayIsEmpty;
        }

        #region Comparison
        protected abstract string SortingName { get; }

        public static int SortByName(GenericJsonObject o1, GenericJsonObject o2)
        {
            string s1 = o1.SortingName;
            string s2 = o2.SortingName;
            return string.Compare(s1, s2, StringComparison.InvariantCulture);
        }
        #endregion
    }

    public abstract class GenericJsonObject<T>: GenericJsonObject, IGenericJsonObject
        where T: class
    {
        #region Init
        public GenericJsonObject()
        {
            LinkBackTable = new Dictionary<Type, List<GenericJsonObject>>();
        }
        #endregion

        #region Descendant Interface
        protected abstract Dictionary<string, FieldParser> FieldTable { get; }
        protected abstract string FieldTableName { get; }
        protected abstract bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables);

        protected static Dictionary<string, bool> ParsedFields;

        protected virtual void InitializeKey(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            Key = EntryRaw.Key;
        }

        protected virtual void InitParsedFields()
        {
            if (ParsedFields == null)
            {
                ParsedFields = new Dictionary<string, bool>();
                foreach (KeyValuePair<string, FieldParser> Field in FieldTable)
                    ParsedFields.Add(Field.Key, false);
            }
        }

        protected virtual bool IsCustomFieldParsed(string FieldKey, object FieldValue, ParseErrorInfo ErrorInfo)
        {
            return false;
        }

        protected virtual void ParseField(string key, object value, ParseErrorInfo errorInfo)
        {
            if (value == null)
                return;

            else if (IsCustomFieldParsed(key, value, errorInfo))
                return;

            else if (FieldTable.ContainsKey(key))
            {
                ParsedFields[key] = true;
                FieldParser Parser = FieldTable[key];

                switch (Parser.Type)
                {
                    default:
                        errorInfo.AddMissingField(FieldTableName + " Field Type: " + key);
                        break;

                    case FieldType.Integer:
                        ParseFieldValueInteger(value, errorInfo, key, Parser.ParserInteger);
                        break;

                    case FieldType.Bool:
                        ParseFieldValueBool(value, errorInfo, key, Parser.ParserBool);
                        break;

                    case FieldType.Float:
                        ParseFieldValueFloat(value, errorInfo, key, Parser.ParserFloat);
                        break;

                    case FieldType.String:
                        ParseFieldValueString(value, errorInfo, key, Parser.ParserString);
                        break;

                    case FieldType.Object:
                        ParseFieldValueObject(value, errorInfo, key, Parser.ParserObject);
                        break;

                    case FieldType.IntegerArray:
                        ParseFieldValueIntegerArray(value, errorInfo, key, Parser.ParserIntegerArray);
                        break;

                    case FieldType.SimpleStringArray:
                        ParseFieldValueSimpleStringArray(value, errorInfo, key, Parser.ParserSimpleStringArray, Parser.ParserSetStringListEmpty);
                        break;

                    case FieldType.StringArray:
                        ParseFieldValueStringArray(value, errorInfo, key, Parser.ParserStringArray);
                        break;

                    case FieldType.ObjectArray:
                        ParseFieldValueObjectArray(value, errorInfo, key, Parser.ParserObjectArray);
                        break;
                }
            }
            else
                errorInfo.AddMissingField(FieldTableName + " Field: " + key);
        }

        protected virtual void ParseFields(Dictionary<string, object> Fields, ParseErrorInfo ErrorInfo)
        {
            InitParsedFields();

            foreach (KeyValuePair<string, object> Entry in Fields)
                ParseField(Entry.Key, Entry.Value, ErrorInfo);
        }

        protected virtual void ParseFields(JsonArray ArrayFields, ParseErrorInfo ErrorInfo)
        {
            InitParsedFields();

            foreach (object ArrayField in ArrayFields)
            {
                JsonObject Fields;
                if ((Fields = ArrayField as JsonObject) != null)
                {
                    foreach (KeyValuePair<string, IJsonValue> Entry in Fields)
                        ParseField(Entry.Key, Entry.Value, ErrorInfo);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat(FieldTableName + ": " + Key);
            }
        }
        /*
        protected virtual void ParseFields(JsonObject JObjectFields, ParseErrorInfo ErrorInfo)
        {
            InitParsedFields();
            ParseFieldsInternal(JObjectFields, ErrorInfo);
        }

        private void ParseFieldsInternal(JsonObject JObjectFields, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> Field in JObjectFields)
            {
                JsonArray AsArray;
                IJsonValue AsValue;
                //JToken AsToken;

                if ((AsArray = Field.Value as JsonArray) != null)
                {
                    if (FieldTable.ContainsKey(Field.Key))
                    {
                        ParsedFields[Field.Key] = true;
                        FieldTable[Field.Key](this as T, Field.Key, AsArray, ErrorInfo);
                    }
                    else
                        ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
                }

                else if ((AsValue = Field.Value as IJsonValue) != null)
                {
                    if (IsCustomFieldParsed(Field.Key, AsValue, ErrorInfo))
                        continue;

                    if (FieldTable.ContainsKey(Field.Key))
                    {
                        ParsedFields[Field.Key] = true;
                        FieldTable[Field.Key](this as T, Field.Key, AsValue, ErrorInfo);
                    }
                    else
                        ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
                }

                else if (Field.Value != null)
                    ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
            }
        }
        */

        protected void ParseStringTable(JsonArray RawArray, List<string> RawList, string FieldName, ParseErrorInfo ErrorInfo, out bool IsListEmpty)
        {
            foreach (object Item in RawArray)
            {
                JsonString AsJsonString;

                if ((AsJsonString = Item as JsonString) != null)
                    RawList.Add(AsJsonString.String);
                else
                    ErrorInfo.AddInvalidObjectFormat(FieldTableName + " Field: " + FieldName);
            }

            IsListEmpty = (RawList.Count == 0);
        }

        protected void ParseIntTable(JsonArray RawArray, List<int> RawList, string FieldName, ParseErrorInfo ErrorInfo, out bool IsListEmpty)
        {
            foreach (object Item in RawArray)
            {
                JsonInteger AsJsonInteger;
                if ((AsJsonInteger = Item as JsonInteger) != null)
                    RawList.Add(AsJsonInteger.Number);
                else if (Item != null)
                    ErrorInfo.AddInvalidObjectFormat(FieldTableName + " Field: " + FieldName);
            }

            IsListEmpty = (RawList.Count == 0);
        }

        protected virtual void CheckUnparsedFields(ParseErrorInfo ErrorInfo)
        {
            if (ParsedFields != null)
            {
                foreach (KeyValuePair<string, bool> Field in ParsedFields)
                    if (!Field.Value)
                        ErrorInfo.AddUnparsedField(FieldTableName + " Field: " + Field.Key);

                ParsedFields = null;
            }
        }

        protected virtual void AddWithFieldSeparator(ref string Result, string s)
        {
            if (s != null)
                Result += s + JsonGenerator.FieldSeparator;
        }

        public Dictionary<Type, List<GenericJsonObject>> LinkBackTable { get; private set; }
        public bool HasLinkBackTableEntries { get { return LinkBackTable.Count > 0; } }

        static List<Type> LinkBackTypeList = new List<Type>();

        protected void AddLinkBack(GenericJsonObject LinkBack)
        {
            if (LinkBack == null)
                return;

            if (LinkBack is RecipeItem)
                LinkBack = (LinkBack as RecipeItem).ParentRecipe;
            else if (LinkBack is QuestObjective)
                LinkBack = (LinkBack as QuestObjective).ParentQuest;
            else if (LinkBack is AbilityRequirement)
                return;
            else if (LinkBack is PowerTier)
                return;
            else if (LinkBack is QuestRewardItem)
                LinkBack = (LinkBack as QuestRewardItem).ParentQuest;
            else if (LinkBack is Reward)
                LinkBack = (LinkBack as Reward).ParentSkill;

            Type ObjectType = LinkBack.GetType();
            if (!LinkBackTable.ContainsKey(ObjectType))
                LinkBackTable.Add(ObjectType, new List<GenericJsonObject>());

            List<GenericJsonObject> LinkBackList = LinkBackTable[ObjectType];
            if (!LinkBackList.Contains(LinkBack))
                LinkBackList.Add(LinkBack);
        }

        protected static void ParseFieldValueString(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<string, ParseErrorInfo> ParseValue)
        {
            JsonString AsJsonString;

            if ((AsJsonString = Value as JsonString) != null)
                ParseValue(AsJsonString.String, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueSimpleStringArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<string, ParseErrorInfo> ParseValue, Action ParserSetStringListEmpty)
        {
            JsonArray AsArray;
            if ((AsArray = Value as JsonArray) != null)
            {
                if (AsArray.Count == 0 && ParserSetStringListEmpty != null)
                    ParserSetStringListEmpty();

                else
                    foreach (IJsonValue Item in AsArray)
                    {
                        JsonString AsJsonString;
                        if ((AsJsonString = Item as JsonString) != null)
                            ParseValue(AsJsonString.String, ErrorInfo);
                        else
                        {
                            ErrorInfo.AddInvalidObjectFormat(FieldName);
                            break;
                        }
                    }
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueStringArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Func<string, ParseErrorInfo, bool> ParseValue, Action ParserSetStringListEmpty)
        {
            JsonArray AsArray;
            if ((AsArray = Value as JsonArray) != null)
            {
                if (AsArray.Count == 0 && ParserSetStringListEmpty != null)
                    ParserSetStringListEmpty();

                else
                    foreach (IJsonValue Item in AsArray)
                    {
                        JsonString AsJsonString;
                        if ((AsJsonString = Item as JsonString) != null)
                        {
                            if (!ParseValue(AsJsonString.String, ErrorInfo))
                                break;
                        }
                        else
                        {
                            ErrorInfo.AddInvalidObjectFormat(FieldName);
                            break;
                        }
                    }
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueInteger(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<int, ParseErrorInfo> ParseValue)
        {
            JsonInteger AsJsonInteger;
            JsonString AsJsonString;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                ParseValue(AsJsonInteger.Number, ErrorInfo);
            else if ((AsJsonString = Value as JsonString) != null)
            {
                if (int.TryParse(AsJsonString.String, out int IntValue))
                    ParseValue(IntValue, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat(FieldName);
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueIntegerArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Func<int, ParseErrorInfo, bool> ParseValue)
        {
            JsonArray AsArray;
            if ((AsArray = Value as JsonArray) != null)
            {
                foreach (IJsonValue Item in AsArray)
                {
                    JsonInteger AsJsonInteger;
                    if ((AsJsonInteger = Item as JsonInteger) != null)
                    {
                        if (!ParseValue(AsJsonInteger.Number, ErrorInfo))
                            break;
                    }
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat(FieldName);
                        break;
                    }
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueFloat(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<float, ParseErrorInfo> ParseValue)
        {
            JsonInteger AsJsonInteger;
            JsonFloat AsJsonFloat;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                ParseValue(AsJsonInteger.Number, ErrorInfo);

            else if ((AsJsonFloat = Value as JsonFloat) != null)
                ParseValue(AsJsonFloat.Number, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueBool(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<bool, ParseErrorInfo> ParseValue)
        {
            JsonBool AsJsonBool;

            if ((AsJsonBool = Value as JsonBool) != null)
                ParseValue(AsJsonBool.Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueObject(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<JsonObject, ParseErrorInfo> ParseValue)
        {
            JsonObject AsJObject;
            if ((AsJObject = Value as JsonObject) != null)
                ParseValue(AsJObject, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<JsonObject, ParseErrorInfo> ParseValue)
        {
            JsonArray AsArray;
            if ((AsArray = Value as JsonArray) != null)
            {
                foreach (IJsonValue Item in AsArray)
                {
                    JsonObject AsJObject;
                    if ((AsJObject = Item as JsonObject) != null)
                        ParseValue(AsJObject, ErrorInfo);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat(FieldName);
                        break;
                    }
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueObjectArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<JsonObject, ParseErrorInfo> ParseValue)
        {
            JsonObject AsJObject;
            JsonArray AsArray;

            if ((AsJObject = Value as JsonObject) != null)
                ParseValue(AsJObject, ErrorInfo);

            else if ((AsArray = Value as JsonArray) != null)
            {
                foreach (IJsonValue Item in AsArray)
                    ParseFieldValueObjectArray(Item, ErrorInfo, FieldName, ParseValue);
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }
        #endregion

        #region Client Interface
        public abstract void GenerateObjectContent(JsonGenerator Generator);

        public string Key { get; private set; }
        public abstract string TextContent { get; }

        public virtual bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            CheckUnparsedFields(ErrorInfo);

            bool IsConnected;

            IsConnected = ConnectFields(ErrorInfo, Parent, AllTables);

            return IsConnected;
        }

        public virtual void Init(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(EntryRaw, ErrorInfo);

            Dictionary<string, object> Fields;
            JsonArray JArrayFields;
            JsonObject JObjectFields;


            if ((Fields = EntryRaw.Value as Dictionary<string, object>) != null)
                ParseFields(Fields, ErrorInfo);
            else if ((JArrayFields = EntryRaw.Value as JsonArray) != null)
                ParseFields(JArrayFields, ErrorInfo);
            else if ((JObjectFields = EntryRaw.Value as JsonObject) != null)
                ParseFields(JObjectFields, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat(FieldTableName + ": " + Key);
        }

        public virtual void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {

        }

        public void SortLinkBack()
        {
            foreach (KeyValuePair<Type, List<GenericJsonObject>> Entry in LinkBackTable)
                Entry.Value.Sort(GenericJsonObject.SortByName);
        }
        #endregion
    }
}
