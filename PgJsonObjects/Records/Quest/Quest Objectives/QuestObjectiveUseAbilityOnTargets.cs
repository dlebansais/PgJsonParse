using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseAbilityOnTargets : QuestObjective, IPgQuestObjectiveUseAbilityOnTargets
    {
        #region Init
        public QuestObjectiveUseAbilityOnTargets(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, AbilityKeyword Keyword)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
        {
            this.Keyword = Keyword;
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
            { "AbilityKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Keyword != AbilityKeyword.Internal_None ? StringToEnumConversion<AbilityKeyword>.ToString(Keyword) : null } },
        }; } }
        #endregion

        #region Properties
        public IPgAbilityCollection AbilityList { get; private set; } = new PgAbilityCollection();
        public AbilityKeyword Keyword { get; private set; }
        private bool IsAbilityParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (Keyword != AbilityKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, StringToEnumConversion<AbilityKeyword>.ToString(Keyword));

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IJsonKey> AbilityTable = AllTables[typeof(Ability)];

            AbilityList = PgJsonObjects.Ability.ConnectByKeyword(ErrorInfo, AbilityTable, Keyword, AbilityList, ref IsAbilityParsed, ref IsConnected, Parent);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddObjectList(AbilityList, data, ref offset, BaseOffset, 0, StoredObjectListTable);
            AddEnum(Keyword, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 6, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
