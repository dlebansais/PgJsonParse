using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveAnatomy : QuestObjective
    {
        #region Init
        public QuestObjectiveAnatomy(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, PowerSkill AnatomyType)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.AnatomyType = AnatomyType;
        }
        #endregion

        #region Properties
        public PowerSkill AnatomyType { get; private set; }
        public Skill ConnectedSkill { get; private set; }
        private bool IsSkillParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (ConnectedSkill != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkill.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, AnatomyType, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
