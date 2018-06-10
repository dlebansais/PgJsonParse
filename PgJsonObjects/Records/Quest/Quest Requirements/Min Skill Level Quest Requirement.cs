using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinSkillLevelQuestRequirement : QuestRequirement
    {
        public MinSkillLevelQuestRequirement(OtherRequirementType OtherRequirementType, PowerSkill RequirementSkill, int? RawRequirementSkillLevel)
            : base(OtherRequirementType)
        {
            Skill = RequirementSkill;
            RawSkillLevel = RawRequirementSkillLevel;
        }

        private PowerSkill Skill;
        public Skill ConnectedSkill { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        private bool IsConnectedSkillParsed;
        private int? RawSkillLevel;

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

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsConnectedSkillParsed, ref IsConnected, Parent as GenericJsonObject);

            return IsConnected;
        }
        #endregion
    }
}
