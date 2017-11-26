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
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, StorageVault> StorageVaultTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, StorageVaultTable, AbilitySourceTable);

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, LinkBack);

            return IsConnected;
        }
        #endregion
    }
}
