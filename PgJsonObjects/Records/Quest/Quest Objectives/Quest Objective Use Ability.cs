using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseAbility : QuestObjective, IPgQuestObjectiveUseAbility
    {
        #region Init
        public QuestObjectiveUseAbility(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, AbilityKeyword AbilityTarget)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.AbilityTarget = AbilityTarget;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityKeyword>.ToString(AbilityTarget) } },
        }; } }
        #endregion

        #region Properties
        public AbilityCollection AbilityTargetList { get; private set; } = new AbilityCollection();
        public AbilityKeyword AbilityTarget { get; private set; }

        private bool IsAbilityTargetParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (AbilityTarget != AbilityKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[AbilityTarget]);
                foreach (Ability Ability in AbilityTargetList)
                    AddWithFieldSeparator(ref Result, Ability.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];

            AbilityTargetList = PgJsonObjects.Ability.ConnectByKeyword(ErrorInfo, AbilityTable, AbilityTarget, AbilityTargetList, ref IsAbilityTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObjectList(AbilityTargetList, data, ref offset, BaseOffset, 4, StoredObjectListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 8, StoredStringListTable);
            AddEnum(AbilityTarget, data, ref offset, BaseOffset, 12);

            FinishSerializing(data, ref offset, BaseOffset, 14, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
