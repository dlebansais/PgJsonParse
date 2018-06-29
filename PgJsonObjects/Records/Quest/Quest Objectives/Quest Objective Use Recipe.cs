using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseRecipe : QuestObjective, IPgQuestObjectiveUseRecipe
    {
        #region Init
        public QuestObjectiveUseRecipe(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, PowerSkill RawSkill, RecipeKeyword RecipeTarget, ItemKeyword ResultItemKeyword)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawSkill = RawSkill;
            this.RecipeTarget = RecipeTarget;
            this.ResultItemKeyword = ResultItemKeyword;
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
                GetString = () => StringToEnumConversion<RecipeKeyword>.ToString(RecipeTarget) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
            { "ResultItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ResultItemKeyword, null, ItemKeyword.Internal_None) } },
        }; } }
        #endregion

        #region Properties
        public IPgSkill Skill { get; private set; }
        public RecipeCollection RecipeTargetList { get; private set; } = new RecipeCollection();
        public ItemCollection ResultItemList { get; private set; } = new ItemCollection();

        private PowerSkill RawSkill;
        private bool IsSkillParsed;
        private RecipeKeyword RecipeTarget;
        private bool IsRecipeTargetParsed;
        private ItemKeyword ResultItemKeyword;
        private bool IsResultItemParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (Skill != null)
                    AddWithFieldSeparator(ref Result, Skill.Name);
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsSkillParsed, ref IsConnected, this);
            RecipeTargetList = PgJsonObjects.Recipe.ConnectByKeyword(ErrorInfo, RecipeTable, RecipeTarget, RecipeTargetList, ref IsRecipeTargetParsed, ref IsConnected, ParentQuest);
            ResultItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ResultItemKeyword, ResultItemList, ref IsResultItemParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObjectList(RecipeTargetList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddObjectList(ResultItemList, data, ref offset, BaseOffset, 12, StoredObjectListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, StoredObjectTable, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
