using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinSkillLevelQuestRequirement : QuestRequirement
    {
        public MinSkillLevelQuestRequirement(PowerSkill RequirementSkill, int? RawRequirementSkillLevel)
        {
            Skill = RequirementSkill;
            RawSkillLevel = RawRequirementSkillLevel;
        }

        private PowerSkill Skill;
        public Skill ConnectedSkill { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        private bool IsConnectedSkillParsed;
        private int? RawSkillLevel;

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "MinSkillLevel");

            Generator.CloseObject();
        }
        #endregion

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
