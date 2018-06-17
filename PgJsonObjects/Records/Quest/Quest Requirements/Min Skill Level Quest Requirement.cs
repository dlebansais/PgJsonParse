using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinSkillLevelQuestRequirement : QuestRequirement, IPgMinSkillLevelQuestRequirement
    {
        public MinSkillLevelQuestRequirement(OtherRequirementType OtherRequirementType, PowerSkill RequirementSkill, int? RawRequirementSkillLevel)
            : base(OtherRequirementType)
        {
            Skill = RequirementSkill;
            RawSkillLevel = RawRequirementSkillLevel;
        }

        public Skill ConnectedSkill { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; private set; }
        private PowerSkill Skill;
        private bool IsConnectedSkillParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(Skill, null, PowerSkill.Internal_None) } },
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

                if (ConnectedSkill != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkill.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsConnectedSkillParsed, ref IsConnected, Parent as IBackLinkable);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddObject(ConnectedSkill, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddInt(RawSkillLevel, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
