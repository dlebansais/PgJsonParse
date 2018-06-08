using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveLoot : QuestObjective
    {
        #region Init
        public QuestObjectiveLoot(QuestObjectiveType Type, string Description, string RawItemName, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawItemName = RawItemName;
            this.MonsterTypeTag = MonsterTypeTag;
        }

        public QuestObjectiveLoot(QuestObjectiveType Type, string Description, ItemKeyword ItemTarget, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.ItemTarget = ItemTarget;
            this.MonsterTypeTag = MonsterTypeTag;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ItemTarget, null, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawItemName } },
            { "MonsterTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MonsterTypeTag>.ToString(MonsterTypeTag, null, MonsterTypeTag.Internal_None) } },
        }; } }
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            ItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ItemTarget, ItemList, ref IsTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion
    }
}
