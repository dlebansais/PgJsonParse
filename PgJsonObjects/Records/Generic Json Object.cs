using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace PgJsonObjects
{
    public interface IGenericJsonObject
    {
        string Key { get; }
        bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables);
        void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo);
        void SortLinkBack();
        string TextContent { get; }
        void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey);
        void SerializeJsonMainObject(byte[] data, ref int offset);
        void SerializeJsonObject(byte[] data, ref int offset);
    }

    public abstract class GenericJsonObject
    {
        protected enum FieldType
        {
            Unknown,
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

            public Action<object, ParseErrorInfo> ParseUnknown;
            public Action<int, ParseErrorInfo> ParseInteger;
            public Action<bool, ParseErrorInfo> ParseBool;
            public Action<float, ParseErrorInfo> ParseFloat;
            public Action<string, ParseErrorInfo> ParseString;
            public Action<JsonObject, ParseErrorInfo> ParseObject;
            public Action<int, ParseErrorInfo> ParseSimpleIntegerArray;
            public Func<int, ParseErrorInfo, bool> ParseIntegerArray;
            public Action<string, ParseErrorInfo> ParseSimpleStringArray;
            public Func<string, ParseErrorInfo, bool> ParseStringArray;
            public Action<JsonObject, ParseErrorInfo> ParseObjectArray;
            public Action SetArrayIsEmpty;

            public Action<string, JsonGenerator> CustomGenerator;

            public Func<object> GetUnknown;
            public Func<int?> GetInteger;
            public Func<bool?> GetBool;
            public Func<double?> GetFloat;
            public Func<string> GetString;
            public Func<IGenericJsonObject> GetObject;
            public Func<List<int>> GetIntegerArray;
            public Func<List<string>> GetStringArray;
            public Func<IList> GetObjectArray;
            public Func<bool> GetArrayIsEmpty;

            public bool SimplifyArray;
            public Action SetArrayIsSimple;
            public Func<bool> GetArrayIsSimple;

            public Action SetArrayIsNested;
            public Func<bool> GetArrayIsNested;
        }

        public static string NullString = "{3125D9C5-C81F-4507-A422-C9749749CB15}";

        #region Comparison
        protected abstract string SortingName { get; }

        public static int SortByName(GenericJsonObject o1, GenericJsonObject o2)
        {
            string s1 = o1.SortingName;
            string s2 = o2.SortingName;
            return string.Compare(s1, s2, StringComparison.InvariantCulture);
        }
        #endregion

        #region Tools
        public static IList CreateSingleOrEmptyList(object item)
        {
            List<object> Result = new List<object>();
            if (item != null)
                Result.Add(item);

            return Result;
        }

        public static List<string> CreateSingleOrEmptyStringList(string item)
        {
            List<string> Result = new List<string>();
            if (item != null)
                Result.Add(item);

            return Result;
        }
        #endregion

        protected static Dictionary<object, int> SerializedObjectTable = new Dictionary<object, int>();

        public static void ResetSerializedObjectTable()
        {
            SerializedObjectTable.Clear();
        }

        public static bool IsObjectSerialized(IGenericJsonObject item)
        {
            return SerializedObjectTable.ContainsKey(item);
        }
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
        protected List<string> FieldTableOrder { get; private set; } = new List<string>();

        protected static Dictionary<string, bool> ParsedFields;

        protected virtual void InitializeKey(string key, int index, IJsonValue value, ParseErrorInfo ErrorInfo)
        {
            Key = key;
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
                FieldTableOrder.Add(key);

                switch (Parser.Type)
                {
                    default:
                        errorInfo.AddMissingField(FieldTableName + " Field Type: " + key);
                        break;

                    case FieldType.Unknown:
                        Parser.ParseUnknown(value, errorInfo);
                        break;

                    case FieldType.Integer:
                        ParseFieldValueInteger(value, errorInfo, key, Parser.ParseInteger);
                        break;

                    case FieldType.Bool:
                        ParseFieldValueBool(value, errorInfo, key, Parser.ParseBool);
                        break;

                    case FieldType.Float:
                        ParseFieldValueFloat(value, errorInfo, key, Parser.ParseFloat);
                        break;

                    case FieldType.String:
                        ParseFieldValueString(value, errorInfo, key, Parser.ParseString);
                        break;

                    case FieldType.Object:
                        ParseFieldValueObject(value, errorInfo, key, Parser.ParseObject);
                        break;

                    case FieldType.SimpleIntegerArray:
                        ParseFieldValueSimpleIntegerArray(value, errorInfo, key, Parser.ParseSimpleIntegerArray);
                        break;

                    case FieldType.IntegerArray:
                        ParseFieldValueIntegerArray(value, errorInfo, key, Parser.ParseIntegerArray);
                        break;

                    case FieldType.SimpleStringArray:
                        ParseFieldValueSimpleStringArray(value, errorInfo, key, Parser.ParseSimpleStringArray, Parser.SetArrayIsEmpty);
                        break;

                    case FieldType.StringArray:
                        ParseFieldValueStringArray(value, errorInfo, key, Parser.ParseStringArray, Parser.SetArrayIsEmpty);
                        break;

                    case FieldType.ObjectArray:
                        ParseFieldValueObjectArray(value, errorInfo, key, Parser.ParseObjectArray, Parser.SetArrayIsEmpty, Parser.SetArrayIsSimple, Parser.SetArrayIsNested);
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
            ParseFieldsInternal(ArrayFields, ErrorInfo);
        }

        private void ParseFieldsInternal(JsonArray ArrayFields, ParseErrorInfo ErrorInfo)
        {
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

                if ((AsArray = Field.Value as JsonArray) != null)
                {
                    if (FieldTable.ContainsKey(Field.Key))
                    {
                        ParsedFields[Field.Key] = true;
                        ParseField(Field.Key, AsArray, ErrorInfo);
                        //ParseFieldsInternal(AsArray, ErrorInfo);
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
                        ParseField(Field.Key, AsValue, ErrorInfo);
                    }
                    else
                        ErrorInfo.AddMissingField(FieldTableName + " Field: " + Field.Key);
                }

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

        protected static void ParseFieldValueSimpleIntegerArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<int, ParseErrorInfo> ParseValue)
        {
            JsonArray AsArray;
            if ((AsArray = Value as JsonArray) != null)
            {
                foreach (IJsonValue Item in AsArray)
                {
                    JsonInteger AsJsonInteger;
                    if ((AsJsonInteger = Item as JsonInteger) != null)
                        ParseValue(AsJsonInteger.Number, ErrorInfo);

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

        protected static void ParseFieldValueObjectArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<JsonObject, ParseErrorInfo> ParseValue, Action ParserSetObjectListEmpty, Action ParserSetArrayIsSimple)
        {
            if (Value is JsonObject AsJObject)
            {
                ParseValue(AsJObject, ErrorInfo);
                ParserSetArrayIsSimple?.Invoke();
            }

            else if (Value is JsonArray AsArray)
            {
                if (AsArray.Count == 0 && ParserSetObjectListEmpty != null)
                    ParserSetObjectListEmpty();

                else
                    foreach (IJsonValue Item in AsArray)
                        if (Item is JsonObject AsJObjectItem)
                            ParseValue(AsJObjectItem, ErrorInfo);
                        else
                        {
                            ErrorInfo.AddInvalidObjectFormat(FieldName);
                            break;
                        }
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }

        protected static void ParseFieldValueObjectArray(object Value, ParseErrorInfo ErrorInfo, string FieldName, Action<JsonObject, ParseErrorInfo> ParseValue, Action ParserSetObjectListEmpty, Action ParserSetArrayIsSimple, Action ParserSetArrayIsNested)
        {
            if (Value is JsonObject AsJObject)
            {
                ParseValue(AsJObject, ErrorInfo);
                ParserSetArrayIsSimple?.Invoke();
            }

            else if (Value is JsonArray AsArray)
            {
                if (AsArray.Count == 0 && ParserSetObjectListEmpty != null)
                    ParserSetObjectListEmpty();

                else
                    foreach (IJsonValue Item in AsArray)
                        if (Item is JsonObject AsJObjectItem)
                            ParseValue(AsJObjectItem, ErrorInfo);
                        else if (Item is JsonArray AsJArrayItem)
                        {
                            ParserSetArrayIsNested?.Invoke();
                            ParseFieldValueObjectArray(AsJArrayItem, ErrorInfo, FieldName, ParseValue, ParserSetObjectListEmpty, ParserSetArrayIsSimple, null);
                        }
                        else
                        {
                            ErrorInfo.AddInvalidObjectFormat(FieldName);
                            break;
                        }
            }
            else
                ErrorInfo.AddInvalidObjectFormat(FieldName);
        }
        #endregion

        #region Client Interface
        public virtual void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            OpenGeneratorKey(Generator, openWithKey, openWithNullKey);
            ListAllObjectContent(Generator);
            CloseGeneratorKey(Generator, openWithKey, openWithNullKey);
        }

        public virtual void OpenGeneratorKey(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            if (Key != null && openWithKey)
                Generator.OpenObject(Key);
            else if (openWithNullKey)
                Generator.OpenObject(null);
        }

        public virtual void ListAllObjectContent(JsonGenerator Generator)
        {
            foreach (string ParserKey in FieldTableOrder)
                ListObjectContent(Generator, ParserKey);
        }

        public virtual void ListObjectContent(JsonGenerator Generator, string ParserKey)
        {
            if (!FieldTable.ContainsKey(ParserKey))
                ParserKey = null;

            FieldParser Parser = FieldTable[ParserKey];

            IGenericJsonObject Subitem;
            List<int> IntegerList;
            List<string> StringList;

            switch (Parser.Type)
            {
                default:
                    break;

                case FieldType.Unknown:
                    break;

                case FieldType.Integer:
                    Generator.AddInteger(ParserKey, Parser.GetInteger());
                    break;

                case FieldType.Bool:
                    Generator.AddBoolean(ParserKey, Parser.GetBool());
                    break;

                case FieldType.Float:
                    Generator.AddDouble(ParserKey, Parser.GetFloat());
                    break;

                case FieldType.String:
                    Generator.AddString(ParserKey, Parser.GetString());
                    break;

                case FieldType.Object:
                    Subitem = Parser.GetObject();
                    if (Subitem != null)
                        Subitem.GenerateObjectContent(Generator, true, false);
                    break;

                case FieldType.SimpleIntegerArray:
                case FieldType.IntegerArray:
                    IntegerList = Parser.GetIntegerArray();

                    if (Parser.SimplifyArray && IntegerList.Count == 1 && (Parser.GetArrayIsSimple == null || Parser.GetArrayIsSimple()))
                        Generator.AddInteger(ParserKey, IntegerList[0]);
                    else
                        Generator.AddIntegerList(ParserKey, IntegerList, Parser.GetArrayIsEmpty != null && Parser.GetArrayIsEmpty());
                    break;

                case FieldType.SimpleStringArray:
                case FieldType.StringArray:
                    StringList = Parser.GetStringArray();
                    if (StringList == null)
                        StringList = null;

                    if (Parser.SimplifyArray && StringList.Count == 1 && (Parser.GetArrayIsSimple == null || Parser.GetArrayIsSimple()))
                        Generator.AddString(ParserKey, StringList[0]);
                    else
                        Generator.AddStringList(ParserKey, StringList, Parser.GetArrayIsEmpty != null && Parser.GetArrayIsEmpty());
                    break;

                case FieldType.ObjectArray:
                    IList ObjectArray = Parser.GetObjectArray();
                    bool IsListEmpty;
                    if (Parser.GetArrayIsEmpty != null)
                        IsListEmpty = Parser.GetArrayIsEmpty();
                    else
                        IsListEmpty = false;

                    if (ObjectArray == null)
                        ObjectArray = null;

                    if (ObjectArray.Count > 0 || IsListEmpty)
                    {
                        if (Parser.SimplifyArray && ObjectArray.Count == 1 && (Parser.GetArrayIsSimple == null || Parser.GetArrayIsSimple()) && ObjectArray[0] is IGenericJsonObject FirstItem)
                        {
                            Generator.OpenObject(ParserKey);

                            FirstItem.GenerateObjectContent(Generator, false, false);

                            Generator.CloseObject();
                        }

                        else if (IsListEmpty)
                            Generator.AddEmptyArray(ParserKey);

                        else
                        {
                            Generator.OpenArray(ParserKey);
                            if (Parser.GetArrayIsNested != null && Parser.GetArrayIsNested())
                                Generator.OpenNestedArray();

                            foreach (IGenericJsonObject Item in ObjectArray)
                                Item.GenerateObjectContent(Generator, false, true);

                            if (Parser.GetArrayIsNested != null && Parser.GetArrayIsNested())
                                Generator.CloseArray();
                            Generator.CloseArray();
                        }
                    }
                    break;
            }
        }

        public virtual void CloseGeneratorKey(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            if (Key != null && openWithKey)
                Generator.CloseObject();
            else if (openWithNullKey)
                Generator.CloseObject();
        }

        public string Key { get; private set; }
        public abstract string TextContent { get; }

        public virtual bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            CheckUnparsedFields(ErrorInfo);

            bool IsConnected;

            IsConnected = ConnectFields(ErrorInfo, Parent, AllTables);

            return IsConnected;
        }

        public virtual void Init(string key, int index, IJsonValue value, bool loadAsArray, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(key, index, value, ErrorInfo);

            if (value is JsonObject JObjectFields)
                ParseFields(JObjectFields, ErrorInfo);

            else if (ErrorInfo != null)
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

        #region Serializing
        public virtual void SerializeJsonMainObject(byte[] data, ref int offset)
        {
            if (IsObjectSerialized(this))
            {
                int ObjectOffset = SerializedObjectTable[this];

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(ObjectOffset);
                    Array.Copy(valueData, 0, data, offset, 4);
                }

                offset += 4;
            }
            else
            {
                SerializedObjectTable.Add(this, offset);

                if (data != null)
                {
                    byte[] valueData = new byte[4];
                    Array.Copy(valueData, 0, data, offset, 4);
                }

                offset += 4;

                SerializeJsonObjectInternal(data, ref offset);
            }
        }

        public virtual void SerializeJsonObject(byte[] data, ref int offset)
        {
            SerializedObjectTable.Add(this, offset);

            SerializeJsonObjectInternal(data, ref offset);
        }

        //protected abstract void SerializeJsonObjectInternal(byte[] data, ref int offset);
        protected virtual void SerializeJsonObjectInternal(byte[] data, ref int offset) { }

        protected void AddBool(bool? value, byte[] data, ref int offset, ref int bitOffset, int baseOffset, int expectedOffset, int expectedBitOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);
            Debug.Assert(bitOffset == expectedBitOffset);

            if (data != null)
            {
                int Mask = value.HasValue ? (0x01 | (value.Value ? 0x02 : 0)) : 0;
                Mask <<= bitOffset;

                data[offset] |= (byte)Mask;
            }

            bitOffset += 2;
            if (bitOffset == 16)
            {
                bitOffset = 0;
                offset += 2;
            }
        }

        protected void AddEnum<TObject>(TObject value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(typeof(TObject).IsEnum);
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes((UInt16)(int)(object)value);
                Array.Copy(valueData, 0, data, offset, 2);
            }

            offset += 2;
        }

        protected void AddInt(int? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                int StoredValue = value.HasValue ? value.Value : GenericPgObject.NoValueInt;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddUInt(uint? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                uint StoredValue = value.HasValue ? value.Value : GenericPgObject.NoValueInt;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddDouble(double? value, byte[] data, ref int offset, int baseOffset, int expectedOffset)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data != null)
            {
                float StoredValue = value.HasValue ? (float)value.Value : float.NaN;

                byte[] valueData = BitConverter.GetBytes(StoredValue);
                Array.Copy(valueData, 0, data, offset, 4);
            }

            offset += 4;
        }

        protected void AddString(string value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, string> StoredStringtable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredStringtable.Add(offset, value);

            offset += 4;
        }

        protected void AddObject(IGenericJsonObject value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, IGenericJsonObject> StoredObjectTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredObjectTable.Add(offset, value);

            offset += 4;
        }

        protected void AddBoolList(List<bool> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<bool>> StoredBoolListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredBoolListTable.Add(offset, value);

            offset += 4;
        }

        protected void AddEnumList<TObject>(List<TObject> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, IList> StoredEnumListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredEnumListTable.Add(offset, value);

            offset += 4;
        }

        protected void AddIntList(List<int> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<int>> StoredIntListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredIntListTable.Add(offset, value);

            offset += 4;
        }

        protected void AddUIntList(List<uint> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<uint>> StoredUIntListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredUIntListTable.Add(offset, value);

            offset += 4;
        }

        protected void AddStringList(List<string> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, List<string>> StoredStringListTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredStringListTable.Add(offset, value);

            offset += 4;
        }

        protected void AddObjectList<TObject>(List<TObject> value, byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, IList> StoredObjectistTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (data == null)
                StoredObjectistTable.Add(offset, value);

            offset += 4;
        }

        protected void FinishSerializing(byte[] data, ref int offset, int baseOffset, int expectedOffset, Dictionary<int, string> StoredStringtable, Dictionary<int, IGenericJsonObject> StoredObjectTable, Dictionary<int, List<bool>> StoredBoolListTable, Dictionary<int, IList> StoredEnumListTable, Dictionary<int, List<int>> StoredIntListTable, Dictionary<int, List<uint>> StoredUIntListTable, Dictionary<int, List<string>> StoredStringListTable, Dictionary<int, IList> StoredObjectistTable)
        {
            Debug.Assert(offset == baseOffset + expectedOffset);

            if (StoredStringtable != null)
                foreach (KeyValuePair<int, string> Entry in StoredStringtable)
                    FinishSerializingString(data, ref offset, Entry.Key, Entry.Value);

            if (StoredObjectTable != null)
                foreach (KeyValuePair<int, IGenericJsonObject> Entry in StoredObjectTable)
                    FinishSerializingObject(data, ref offset, Entry.Key, Entry.Value);

            if (StoredBoolListTable != null)
                foreach (KeyValuePair<int, List<bool>> Entry in StoredBoolListTable)
                    FinishSerializingBoolList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredEnumListTable != null)
                foreach (KeyValuePair<int, IList> Entry in StoredEnumListTable)
                    FinishSerializingEnumList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredIntListTable != null)
                foreach (KeyValuePair<int, List<int>> Entry in StoredIntListTable)
                    FinishSerializingIntList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredUIntListTable != null)
                foreach (KeyValuePair<int, List<uint>> Entry in StoredUIntListTable)
                    FinishSerializingUIntList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredStringListTable != null)
                foreach (KeyValuePair<int, List<string>> Entry in StoredStringListTable)
                    FinishSerializingStringList(data, ref offset, Entry.Key, Entry.Value);

            if (StoredObjectistTable != null)
                foreach (KeyValuePair<int, IList> Entry in StoredObjectistTable)
                    FinishSerializingObjectList(data, ref offset, Entry.Key, Entry.Value);
        }

        protected void FinishSerializingString(byte[] data, ref int offset, int redirectionOffset, string StringValue)
        {
            if (StringValue == null)
            {
                if (data != null)
                {
                    byte[] valueData = new byte[4];
                    Array.Copy(valueData, 0, data, redirectionOffset, 4);
                }
            }
            else
            {
                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(offset);
                    Array.Copy(valueData, 0, data, redirectionOffset, 4);
                }

                if (data != null)
                {
                    byte[] LengthData = BitConverter.GetBytes((UInt16)StringValue.Length);
                    Array.Copy(LengthData, 0, data, offset, 2);
                }

                offset += 2;

                for (int i = 0; i < StringValue.Length; i++)
                {
                    char CharacterValue = StringValue[i];

                    if (data != null)
                    {
                        byte[] CharacterData = BitConverter.GetBytes(CharacterValue);
                        Array.Copy(CharacterData, 0, data, offset, 2);
                    }

                    offset += 2;
                }
            }
        }

        protected void FinishSerializingObject(byte[] data, ref int offset, int redirectionOffset, IGenericJsonObject ObjectValue)
        {
            if (ObjectValue == null)
            {
                if (data != null)
                {
                    byte[] valueData = new byte[4];
                    Array.Copy(valueData, 0, data, redirectionOffset, 4);
                }
            }
            else
            {
                if (IsObjectSerialized(ObjectValue))
                {
                    if (data != null)
                    {
                        int ObjectOffset = SerializedObjectTable[ObjectValue];

                        byte[] valueData = BitConverter.GetBytes(ObjectOffset);
                        Array.Copy(valueData, 0, data, redirectionOffset, 4);
                    }
                }
                else
                {
                    if (data != null)
                    {
                        byte[] valueData = BitConverter.GetBytes(offset);
                        Array.Copy(valueData, 0, data, redirectionOffset, 4);
                    }

                    offset += 4;

                    ObjectValue.SerializeJsonObject(data, ref offset);
                }
            }
        }

        protected void FinishSerializingBoolList(byte[] data, ref int offset, int redirectionOffset, List<bool> BoolList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes((UInt16)BoolList.Count);
                Array.Copy(LengthData, 0, data, offset, 2);
            }

            offset += 2;

            for (int i = 0; i < BoolList.Count; i++)
            {
                UInt16 BoolValue = (UInt16)(BoolList[i] ? 1 : 0);

                if (data != null)
                {
                    byte[] BoolData = BitConverter.GetBytes(BoolValue);
                    Array.Copy(BoolData, 0, data, offset, 2);
                }

                offset += 2;
            }
        }

        protected void FinishSerializingEnumList(byte[] data, ref int offset, int redirectionOffset, IList EnumList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes((UInt16)EnumList.Count);
                Array.Copy(LengthData, 0, data, offset, 2);
            }

            offset += 2;

            for (int i = 0; i < EnumList.Count; i++)
            {
                UInt16 EnumValue = (UInt16)(int)EnumList[i];

                if (data != null)
                {
                    byte[] EnumData = BitConverter.GetBytes(EnumValue);
                    Array.Copy(EnumData, 0, data, offset, 2);
                }

                offset += 2;
            }
        }

        protected void FinishSerializingIntList(byte[] data, ref int offset, int redirectionOffset, List<int> IntList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(IntList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < IntList.Count; i++)
            {
                int IntValue = IntList[i];

                if (data != null)
                {
                    byte[] IntData = BitConverter.GetBytes(IntValue);
                    Array.Copy(IntData, 0, data, offset, 4);
                }

                offset += 4;
            }
        }

        protected void FinishSerializingUIntList(byte[] data, ref int offset, int redirectionOffset, List<uint> UIntList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(UIntList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < UIntList.Count; i++)
            {
                uint UIntValue = UIntList[i];

                if (data != null)
                {
                    byte[] IntData = BitConverter.GetBytes(UIntValue);
                    Array.Copy(IntData, 0, data, offset, 4);
                }

                offset += 4;
            }
        }

        protected void FinishSerializingStringList(byte[] data, ref int offset, int redirectionOffset, List<string> StringList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(StringList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < StringList.Count; i++)
            {
                string StringValue = StringList[i];

                if (data != null)
                {
                    byte[] LengthData = BitConverter.GetBytes((UInt16)StringValue.Length);
                    Array.Copy(LengthData, 0, data, offset, 2);
                }

                offset += 2;

                for (int j = 0; j < StringValue.Length; j++)
                {
                    char CharacterValue = StringValue[j];

                    if (data != null)
                    {
                        byte[] CharacterData = BitConverter.GetBytes(CharacterValue);
                        Array.Copy(CharacterData, 0, data, offset, 2);
                    }

                    offset += 2;
                }
            }
        }

        protected void FinishSerializingObjectList(byte[] data, ref int offset, int redirectionOffset, IList ObjectList)
        {
            if (data != null)
            {
                byte[] valueData = BitConverter.GetBytes(offset);
                Array.Copy(valueData, 0, data, redirectionOffset, valueData.Length);
            }

            if (data != null)
            {
                byte[] LengthData = BitConverter.GetBytes(ObjectList.Count);
                Array.Copy(LengthData, 0, data, offset, 4);
            }

            offset += 4;

            for (int i = 0; i < ObjectList.Count; i++)
            {
                IGenericJsonObject ObjectValue = ObjectList[i] as IGenericJsonObject;

                if (IsObjectSerialized(ObjectValue))
                {
                    int ObjectOffset = SerializedObjectTable[ObjectValue];

                    if (data != null)
                    {
                        byte[] valueData = BitConverter.GetBytes(ObjectOffset);
                        Array.Copy(valueData, 0, data, offset, 4);
                    }

                    offset += 4;
                }
                else
                {
                    if (data != null)
                    {
                        byte[] valueData = new byte[4];
                        Array.Copy(valueData, 0, data, offset, 4);
                    }

                    offset += 4;

                    ObjectValue.SerializeJsonObject(data, ref offset);
                }
            }
        }

        protected void CloseBool(ref int offset, ref int bitOffset)
        {
            if (bitOffset > 0)
                offset += 2;

            bitOffset = 0;
        }

        protected void AlignSerializedLength(ref int offset)
        {
            offset = ((offset + 3) / 4) * 4;
        }
        #endregion
    }
}
