using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveAnatomy : QuestObjective, IPgQuestObjectiveAnatomy
    {
        #region Init
        public QuestObjectiveAnatomy(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, PowerSkill AnatomyType)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.AnatomyType = AnatomyType;
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
            { "AnatomyType", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None).Substring(8) : null } },
        }; } }
        #endregion

        #region Properties
        public IPgSkill Skill { get; private set; }
        public PowerSkill AnatomyType { get; private set; }
        private bool IsSkillParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (Skill != null)
                    AddWithFieldSeparator(ref Result, Skill.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, AnatomyType, Skill, ref IsSkillParsed, ref IsConnected, this);

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

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
