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
        protected delegate void FieldValueHandler(T This, object Value, ParseErrorInfo ErrorInfo);

        protected abstract Dictionary<string, FieldValueHandler> FieldTable { get; }
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
                foreach (KeyValuePair<string, FieldValueHandler> Field in FieldTable)
                    ParsedFields.Add(Field.Key, false);
            }
        }

        protected virtual bool IsCustomFieldParsed(string FieldKey, object FieldValue, ParseErrorInfo ErrorInfo)
        {
            return false;
        }

        protected virtual void ParseFields(Dictionary<string, object> Fields, ParseErrorInfo ErrorInfo)
        {
            InitParsedFields();

            foreach (KeyValuePair<string, object> Field in Fields)
            {
                if (IsCustomFieldParsed(Field.Key, Field.Value, ErrorInfo))
                    continue;

                else if (FieldTable.ContainsKey(Field.Key))
                {
                    ParsedFields[Field.Key] = true;
                    FieldTable[Field.Key](this as T, Field.Value, ErrorInfo);
                }
                else
                    ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
            }
        }

        protected virtual void ParseFields(JsonArray ArrayFields, ParseErrorInfo ErrorInfo)
        {
            InitParsedFields();

            foreach (object ArrayField in ArrayFields)
            {
                JsonObject Fields;
                if ((Fields = ArrayField as JsonObject) != null)
                {
                    foreach (KeyValuePair<string, IJsonValue> Field in Fields)
                    {
                        if (Field.Value == null)
                            continue;

                        else if (IsCustomFieldParsed(Field.Key, Field.Value, ErrorInfo))
                            continue;

                        else if (FieldTable.ContainsKey(Field.Key))
                        {
                            ParsedFields[Field.Key] = true;
                            FieldTable[Field.Key](this as T, Field.Value, ErrorInfo);
                        }
                        else
                            ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
                    }
                }
                else
                    ErrorInfo.AddInvalidObjectFormat(FieldTableName + ": " + Key);
            }
        }

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
                        FieldTable[Field.Key](this as T, AsArray, ErrorInfo);
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
                        FieldTable[Field.Key](this as T, AsValue, ErrorInfo);
                    }
                    else
                        ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
                }

                /*
                else if ((AsToken = Field.Value as JToken) != null)
                {
                    if (FieldTable.ContainsKey(Field.Key))
                    {
                        ParsedFields[Field.Key] = true;
                        FieldTable[Field.Key](this as T, AsToken, ErrorInfo);
                    }
                    else
                        ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
                }*/

                else if (Field.Value != null)
                    ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
            }
        }

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

        protected static void ParseFieldValueStringArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Func<string, ParseErrorInfo, bool> ParseValue)
        {
            JsonArray AsArray;
            if ((AsArray = Value as JsonArray) != null)
            {
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

        protected static void ParseFieldValueLong(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<long, ParseErrorInfo> ParseValue)
        {
            JsonInteger AsJsonInteger;
            JsonString AsJsonString;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                ParseValue(AsJsonInteger.Number, ErrorInfo);
            else if ((AsJsonString = Value as JsonString) != null)
            {
                if (long.TryParse(AsJsonString.String, out long LongValue))
                    ParseValue(LongValue, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat(FieldName);
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueLongArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Func<long, ParseErrorInfo, bool> ParseValue)
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

        protected static void ParseFieldValueBool(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<bool, ParseErrorInfo> ParseValue)
        {
            JsonBool AsJsonBool;

            if ((AsJsonBool = Value as JsonBool) != null)
                ParseValue(AsJsonBool.Value, ErrorInfo);
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

        protected static void ParseFieldValueStringObjectOrArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<JsonObject, ParseErrorInfo> ParseValue)
        {
            JsonObject AsJObject;
            JsonArray AsArray;

            if ((AsJObject = Value as JsonObject) != null)
                ParseValue(AsJObject, ErrorInfo);

            else if ((AsArray = Value as JsonArray) != null)
            {
                foreach (IJsonValue Item in AsArray)
                    ParseFieldValueStringObjectOrArray(Item, ErrorInfo, FieldName, ParseValue);
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
