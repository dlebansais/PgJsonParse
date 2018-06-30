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
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsConnectedSkillParsed, ref IsConnected, Parent as IBackLinkable);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddInt(RawSkillLevel, data, ref offset, BaseOffset, 12);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
