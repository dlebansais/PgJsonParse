using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AI : MainJsonObject<AI>, IPgAI
    {
        #region Direct Properties
        public IPgAIAbilitySet Abilities { get; private set; }
        public string Comment { get; private set; }
        public float? RawMinDelayBetweenAbilities { get; private set; }
        public bool? RawIsMelee { get; private set; }
        public bool? RawIsUncontrolledPet { get; private set; }
        public bool? RawIsStationary { get; private set; }
        public bool? RawIsServerDriven { get; private set; }
        public bool? RawUseAbilitiesWithoutEnemyTarget { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Abilities", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => Abilities = JsonObjectParser<AIAbilitySet>.Parse("Abilities", value, errorInfo),
                GetObject = () => Abilities as IObjectContentGenerator } },
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
            { "UseAbilitiesWithoutEnemyTarget", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawUseAbilitiesWithoutEnemyTarget = value,
                GetBool = () => RawUseAbilitiesWithoutEnemyTarget } },
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
                if (RawUseAbilitiesWithoutEnemyTarget.HasValue)
                    AddWithFieldSeparator(ref Result, "Use Abilities Without Enemy Target: " + (RawUseAbilitiesWithoutEnemyTarget.Value ? "Yes" : "No"));

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AI"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(Abilities as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddString(Comment, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddDouble(RawMinDelayBetweenAbilities, data, ref offset, BaseOffset, 12);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 16, StoredStringListTable);
            AddBool(RawIsMelee, data, ref offset, ref BitOffset, BaseOffset, 20, 0);
            AddBool(RawIsUncontrolledPet, data, ref offset, ref BitOffset, BaseOffset, 20, 2);
            AddBool(RawIsStationary, data, ref offset, ref BitOffset, BaseOffset, 20, 4);
            AddBool(RawIsServerDriven, data, ref offset, ref BitOffset, BaseOffset, 20, 6);
            AddBool(RawUseAbilitiesWithoutEnemyTarget, data, ref offset, ref BitOffset, BaseOffset, 20, 8);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 22, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
