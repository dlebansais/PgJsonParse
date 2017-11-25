using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveLoot : QuestObjective
    {
        #region Init
        public QuestObjectiveLoot(string Description, string RawItemName, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawItemName = RawItemName;
            this.MonsterTypeTag = MonsterTypeTag;
        }

        public QuestObjectiveLoot(string Description, ItemKeyword ItemTarget, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.ItemTarget = ItemTarget;
            this.MonsterTypeTag = MonsterTypeTag;
        }
        #endregion

        #region Properties
        public Item QuestItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        public ItemKeyword ItemTarget { get; private set; }
        private bool IsTargetParsed;
        public List<Item> ItemList { get; private set; } = new List<Item>();
        public MonsterTypeTag MonsterTypeTag { get; private set; }
        public bool HasMonsterTypeTag { get { return MonsterTypeTag != MonsterTypeTag.Internal_None; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);
                if (ItemTarget != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[ItemTarget]);
                foreach (Item Item in ItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);
                if (MonsterTypeTag != MonsterTypeTag.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MonsterTypeTagTextMap[MonsterTypeTag]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            ItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ItemTarget, ItemList, ref IsTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion
    }
}
