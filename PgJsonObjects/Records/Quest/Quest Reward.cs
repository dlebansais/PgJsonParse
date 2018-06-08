using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestReward : GenericJsonObject<QuestReward>
    {
        #region Init
        public QuestReward()
        {
        }

        public QuestReward(string type)
        {
            Type = type;
        }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Type } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RewardSkill, null, PowerSkill.Internal_None) } },
            { "Xp", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RewardXp } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RewardRecipe } },
            { "Credits", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RewardGuildCredits } },
        }; } }

        public void AddFieldTableOrder(string Field)
        {
            FieldTableOrder.Add(Field);
        }
        #endregion

        #region Properties
        public string Type { get; set; }
        public PowerSkill RewardSkill { get; set; }
        public int? RewardXp { get; set; }
        public string RewardRecipe { get; set; }
        public int? RewardGuildCredits { get; set; }
        #endregion

        #region Indexing
        public override string TextContent { get { return ""; } }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Quest"; } }
        #endregion
    }
}
