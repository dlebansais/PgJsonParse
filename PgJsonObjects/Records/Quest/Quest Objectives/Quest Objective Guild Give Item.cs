using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveGuildGiveItem : QuestObjective
    {
        #region Init
        public QuestObjectiveGuildGiveItem(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string DeliverNpcId, string DeliverNpcName, string RawItemName)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.RawItemName = RawItemName;
        }

        public QuestObjectiveGuildGiveItem(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string DeliverNpcId, string DeliverNpcName, ItemKeyword ItemKeyword)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.ItemKeyword = ItemKeyword;
        }
        #endregion

        #region Properties
        public Item QuestItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        public string DeliverNpcId { get; private set; }
        public string DeliverNpcName { get; private set; }
        private bool IsDeliverNpcParsed;
        public GameNpc DeliverNpc { get; private set; }
        public ItemKeyword ItemKeyword { get; private set; }
        public List<Item> ItemList { get; private set; } = new List<Item>();
        private bool IsTargetParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);
                AddWithFieldSeparator(ref Result, DeliverNpcName);
                if (ItemKeyword != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[ItemKeyword]);
                foreach (Item Item in ItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, GameNpcTable, AbilitySourceTable);

            DeliverNpc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, DeliverNpcId, DeliverNpc, ref IsDeliverNpcParsed, ref IsConnected, this);
            if (DeliverNpcId != null && DeliverNpc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(DeliverNpcId, out ParsedSpecialNpc, ErrorInfo))
                    DeliverNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            ItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ItemKeyword, ItemList, ref IsTargetParsed, ref IsConnected, ParentQuest);
            return IsConnected;
        }
        #endregion
    }
}
