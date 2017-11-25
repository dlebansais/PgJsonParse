using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityServerInfoEffect : ServerInfoEffect
    {
        public AbilityServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, string RawBestowAbility)
            : base(ServerInfoEffect, RawLevel)
        {
            this.RawBestowAbility = RawBestowAbility;
            IsRawBestowAbilityParsed = false;
        }

        private string RawBestowAbility;
        private bool IsRawBestowAbilityParsed;
        public Ability BestowAbility { get; private set; }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (BestowAbility != null)
                    Result += BestowAbility.Name;

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            BestowAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawBestowAbility, BestowAbility, ref IsRawBestowAbilityParsed, ref IsConnected, LinkBack);

            return IsConnected;
        }
        #endregion
    }
}
