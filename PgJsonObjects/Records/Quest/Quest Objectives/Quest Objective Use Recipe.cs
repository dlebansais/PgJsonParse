using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseRecipe : QuestObjective
    {
        #region Init
        public QuestObjectiveUseRecipe(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, PowerSkill Skill, RecipeKeyword RecipeTarget, ItemKeyword ResultItemKeyword)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.Skill = Skill;
            this.RecipeTarget = RecipeTarget;
            this.ResultItemKeyword = ResultItemKeyword;
        }
        #endregion

        #region Properties
        public PowerSkill Skill { get; private set; }
        public Skill ConnectedSkill { get; private set; }
        private bool IsSkillParsed;
        public RecipeKeyword RecipeTarget { get; private set; }
        public List<Recipe> RecipeTargetList { get; private set; } = new List<Recipe>();
        private bool IsRecipeTargetParsed;
        public ItemKeyword ResultItemKeyword { get; private set; }
        public List<Item> ResultItemList { get; private set; } = new List<Item>();
        private bool IsResultItemParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (ConnectedSkill != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkill.Name);
                if (RecipeTarget != RecipeKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeKeywordTextMap[RecipeTarget]);
                foreach (Recipe Recipe in RecipeTargetList)
                    AddWithFieldSeparator(ref Result, Recipe.Name);
                if (ResultItemKeyword != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[ResultItemKeyword]);
                foreach (Item Item in ResultItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);
            RecipeTargetList = PgJsonObjects.Recipe.ConnectByKeyword(ErrorInfo, RecipeTable, RecipeTarget, RecipeTargetList, ref IsRecipeTargetParsed, ref IsConnected, ParentQuest);
            ResultItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ResultItemKeyword, ResultItemList, ref IsResultItemParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion
    }
}
