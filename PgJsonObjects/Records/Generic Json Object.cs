using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class GenericJsonObject<T> where T: class
    {
        #region Descendant Interface
        protected delegate void FieldValueHandler(T This, object Value, ParseErrorInfo ErrorInfo);

        protected abstract Dictionary<string, FieldValueHandler> FieldTable { get; }
        protected abstract string FieldTableName { get; }
        protected abstract bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable);

        protected static Dictionary<string, bool> ParsedFields;

        protected virtual void InitializeKey(KeyValuePair<string, object> EntryRaw)
        {
            Key = EntryRaw.Key;
        }

        protected virtual void InitializeFields()
        {
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

        protected virtual bool IsCustomFieldParsed(KeyValuePair<string, object> Field, ParseErrorInfo ErrorInfo)
        {
            return false;
        }

        protected virtual void ParseFields(Dictionary<string, object> Fields, ParseErrorInfo ErrorInfo)
        {
            InitParsedFields();

            foreach (KeyValuePair<string, object> Field in Fields)
            {
                if (IsCustomFieldParsed(Field, ErrorInfo))
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

        protected void ParseStringTable(ArrayList RawArray, List<string> RawList, string FieldName, ParseErrorInfo ErrorInfo, out bool IsListEmpty)
        {
            foreach (object Item in RawArray)
            {
                string AsString;
                if ((AsString = Item as string) != null)
                    RawList.Add(AsString);
                else
                    ErrorInfo.AddInvalidObjectFormat(FieldTableName + " Field: " + FieldName);
            }

            IsListEmpty = (RawList.Count == 0);
        }

        protected void ParseIntTable(ArrayList RawArray, List<int> RawList, string FieldName, ParseErrorInfo ErrorInfo, out bool IsListEmpty)
        {
            foreach (object Item in RawArray)
            {
                if (Item is int)
                    RawList.Add((int)Item);
                else
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
        #endregion

        #region Client Interface
        public abstract void GenerateObjectContent(JsonGenerator Generator);

        public string Key { get; private set; }
        public abstract string TextContent { get; }

        public virtual bool Connect(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            CheckUnparsedFields(ErrorInfo);

            bool IsConnected;

            IsConnected = ConnectFields(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            return IsConnected;
        }

        public virtual void Init(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(EntryRaw);
            InitializeFields();

            Dictionary<string, object> Fields;
            if ((Fields = EntryRaw.Value as Dictionary<string, object>) != null)
                ParseFields(Fields, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat(FieldTableName + ": " + Key);
        }
        #endregion
    }
}
