using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AI : GenericJsonObject<AI>
    {
        #region Direct Properties
        public AIAbilitySet Abilities { get; private set; }
        public bool? RawIsMelee { get; private set; }
        public bool? RawIsUncontrolledPet { get; private set; }
        public bool? RawIsStationary { get; private set; }
        public bool? RawIsServerDriven { get; private set; }
        public string Comment { get; private set; }
        public float? RawMinDelayBetweenAbilities { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Abilities", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Abilities = JsonObjectParser<AIAbilitySet>.Parse("Abilities", value, errorInfo),
                GetObject = () => Abilities } },
            { "Melee", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsMelee = value,
                GetBool = () => RawIsMelee } },
            { "Comment", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Comment = value,
                GetString = () => Comment } },
            { "Stationary", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsStationary = value,
                GetBool = () => RawIsStationary } },
            { "UncontrolledPet", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsUncontrolledPet = value,
                GetBool = () => RawIsUncontrolledPet } },
            { "ServerDriven", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsServerDriven = value,
                GetBool = () => RawIsServerDriven } },
            { "MinDelayBetweenAbilities", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMinDelayBetweenAbilities = value,
                GetFloat = () => RawMinDelayBetweenAbilities } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (RawIsMelee.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Melee: " + (RawIsMelee.Value ? "Yes" : "No"));
                AddWithFieldSeparator(ref Result, Comment);
                if (RawIsStationary.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Stationary: " + (RawIsStationary.Value ? "Yes" : "No"));
                if (RawIsUncontrolledPet.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Uncontrolled Pet: " + (RawIsUncontrolledPet.Value ? "Yes" : "No"));
                if (RawIsServerDriven.HasValue)
                    AddWithFieldSeparator(ref Result, "Is ServerDriven: " + (RawIsServerDriven.Value ? "Yes" : "No"));

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
        protected override string FieldTableName { get { return "AI"; } }
        #endregion
    }
}
