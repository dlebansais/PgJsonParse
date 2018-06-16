using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveRequirement : GenericJsonObject<QuestObjectiveRequirement>
    {
        #region Init
        public QuestObjectiveRequirement()
        {
        }

        public QuestObjectiveRequirement(string type)
        {
            Type = type;
        }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Type } },
            { "MinHour", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => MinHour } },
            { "MaxHour", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => MaxHour } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(Keyword, null, EffectKeyword.Internal_None) } },
        }; } }

        public void AddFieldTableOrder(string Field)
        {
            if (Key == null)
                base.Init("Requirements", 0, null, false, null);

            FieldTableOrder.Add(Field);
        }
        #endregion

        #region Properties
        public string Type { get; set; }
        public int? MinHour { get; set; }
        public int? MaxHour { get; set; }
        public EffectKeyword Keyword { get; set; }
        #endregion

        #region Indexing
        public override string TextContent { get { return ""; } }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Quest"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
        }
        #endregion
    }
}
