using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillAndLevelServerInfoEffect : ServerInfoEffect
    {
        public SkillAndLevelServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, PowerSkill Skill, int SkillLevel)
            : base(ServerInfoEffect, RawLevel)
        {
            this.Skill = Skill;
            this.SkillLevel = SkillLevel;
        }

        private PowerSkill Skill;
        private bool IsSkillParsed;
        public Skill ConnectedSkill { get; private set; }
        public int SkillLevel { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + StringToEnumConversion<PowerSkill>.ToString(Skill) + "," + SkillLevel.ToString() + ")";
            }
        }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (Skill != PowerSkill.Internal_None)
                    Result += TextMaps.PowerSkillTextMap[Skill];

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, LinkBack);

            return IsConnected;
        }
        #endregion
    }
}
