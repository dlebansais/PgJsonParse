using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseRecipe : QuestObjective, IPgQuestObjectiveUseRecipe
    {
        #region Init
        public QuestObjectiveUseRecipe(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, PowerSkill RawSkill, RecipeKeyword RecipeTarget, ItemKeyword ResultItemKeyword)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
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
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "ResultItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ResultItemKeyword, TextMaps.ItemKeywordStringMap, ItemKeyword.Internal_None) } },
        }; } }
        #endregion

        #region Properties
        public IPgSkill Skill { get; private set; }
        public IPgRecipeCollection RecipeTargetList { get; private set; } = new RecipeCollection();
        public IPgItemCollection ResultItemList { get; private set; } = new ItemCollection();
        public RecipeKeyword RecipeTarget { get; private set; }
        public ItemKeyword ResultItemKeyword { get; private set; }

        private PowerSkill RawSkill;
        private bool IsSkillParsed;
        private bool IsRecipeTargetParsed;
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IJsonKey> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IJsonKey> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsSkillParsed, ref IsConnected, Parent);
            RecipeTargetList = PgJsonObjects.Recipe.ConnectByKeyword(ErrorInfo, RecipeTable, RecipeTarget, RecipeTargetList, ref IsRecipeTargetParsed, ref IsConnected, Parent);
            ResultItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ResultItemKeyword, ResultItemList, ref IsResultItemParsed, ref IsConnected, Parent);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddObjectList(RecipeTargetList, data, ref offset, BaseOffset, 4, StoredObjectListTable);
            AddObjectList(ResultItemList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddEnum(RecipeTarget, data, ref offset, BaseOffset, 12);
            AddEnum(ResultItemKeyword, data, ref offset, BaseOffset, 14);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
