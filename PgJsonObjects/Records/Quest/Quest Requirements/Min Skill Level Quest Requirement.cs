using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinSkillLevelQuestRequirement : QuestRequirement, IPgMinSkillLevelQuestRequirement
    {
        public MinSkillLevelQuestRequirement(OtherRequirementType OtherRequirementType, PowerSkill RequirementSkill, int? RawRequirementSkillLevel)
            : base(OtherRequirementType)
        {
            RawSkill = RequirementSkill;
            RawSkillLevel = RawRequirementSkillLevel;
        }

        public IPgSkill Skill { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; private set; }
        private PowerSkill RawSkill;
        private bool IsConnectedSkillParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "Level", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawSkillLevel } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Skill != null)
                    AddWithFieldSeparator(ref Result, Skill.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsConnectedSkillParsed, ref IsConnected, Parent as IBackLinkable);

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
            AddInt(RawSkillLevel, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 8, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
