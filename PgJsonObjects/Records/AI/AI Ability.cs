using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AIAbility : GenericJsonObject<AIAbility>, IPgAIAbility
    {
        #region Direct Properties
        public int? RawMinLevel { get; private set; }
        public int? RawMaxLevel { get; private set; }
        public int? RawMinDistance { get; private set; }
        public int? RawMinRange { get; private set; }
        public int? RawMaxRange { get; private set; }
        public int? RawCueValue { get; private set; }
        public AbilityCue Cue { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "minLevel", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMinLevel = value,
                GetInteger = () => RawMinLevel } },
            { "maxLevel", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMaxLevel = value,
                GetInteger = () => RawMaxLevel } },
            { "minDistance", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMinDistance = value,
                GetInteger = () => RawMinDistance } },
            { "minRange", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMinRange = value,
                GetInteger = () => RawMinRange } },
            { "maxRange", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMaxRange = value,
                GetInteger = () => RawMaxRange } },
            { "cue", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Cue = StringToEnumConversion<AbilityCue>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityCue>.ToString(Cue, null, AbilityCue.Internal_None) } },
            { "cueVal", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCueValue = value,
                GetInteger = () => RawCueValue } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AI Ability"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddInt(RawMinLevel, data, ref offset, BaseOffset, 0);
            AddInt(RawMaxLevel, data, ref offset, BaseOffset, 4);
            AddInt(RawMinDistance, data, ref offset, BaseOffset, 8);
            AddInt(RawMinRange, data, ref offset, BaseOffset, 12);
            AddInt(RawMaxRange, data, ref offset, BaseOffset, 16);
            AddInt(RawCueValue, data, ref offset, BaseOffset, 20);
            AddEnum(Cue, data, ref offset, BaseOffset, 24);

            FinishSerializing(data, ref offset, BaseOffset, 26, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
