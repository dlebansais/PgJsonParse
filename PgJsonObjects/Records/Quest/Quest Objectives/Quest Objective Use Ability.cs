using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseAbility : QuestObjective
    {
        #region Init
        public QuestObjectiveUseAbility(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, AbilityKeyword AbilityTarget)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.AbilityTarget = AbilityTarget;
        }
        #endregion

        #region Properties
        public AbilityKeyword AbilityTarget { get; private set; }
        public List<Ability> AbilityTargetList { get; private set; } = new List<Ability>();
        private bool IsAbilityTargetParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (AbilityTarget != AbilityKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[AbilityTarget]);
                foreach (Ability Ability in AbilityTargetList)
                    AddWithFieldSeparator(ref Result, Ability.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, StorageVault> StorageVaultTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, StorageVaultTable, AbilitySourceTable);

            AbilityTargetList = PgJsonObjects.Ability.ConnectByKeyword(ErrorInfo, AbilityTable, AbilityTarget, AbilityTargetList, ref IsAbilityTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion
    }
}
