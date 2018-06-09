using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AIAbility : GenericJsonObject<AIAbility>
    {
        #region Direct Properties
        public int? RawMinLevel { get; private set; }
        public int? RawMaxLevel { get; private set; }
        public int? RawMinDistance { get; private set; }
        public int? RawMinRange { get; private set; }
        public int? RawMaxRange { get; private set; }
        public AbilityCue Cue { get; private set; }
        public int? RawCueValue { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
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
    }
}
