using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveDeliver : QuestObjective
    {
        #region Indexing
        public QuestObjectiveDeliver(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, MapAreaName DeliverNpcArea, string DeliverNpcName, string RawItemName)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.DeliverNpcArea = DeliverNpcArea;
            this.DeliverNpcName = DeliverNpcName;
            this.RawItemName = RawItemName;
        }
        #endregion

        #region Properties
        public MapAreaName DeliverNpcArea { get; private set; }
        public string DeliverNpcName { get; private set; }
        public Item QuestItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[DeliverNpcArea]);
                AddWithFieldSeparator(ref Result, DeliverNpcName);
                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
