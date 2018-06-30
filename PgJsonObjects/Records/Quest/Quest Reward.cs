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

        #region Properties
        public string Type { get; set; }
        public int RewardXp { get { return RawRewardXp.HasValue ? RawRewardXp.Value : 0; } }
        public int? RawRewardXp { get; set; }
        public string RewardRecipe { get; set; }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get; set; }
        public PowerSkill RewardSkill { get; set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
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
                GetInteger = () => RawRewardXp } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RewardRecipe } },
            { "Credits", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardGuildCredits } },
        }; } }

        public void AddFieldTableOrder(string Field)
        {
            FieldTableOrder.Add(Field);
        }
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Type, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddInt(RawRewardXp, data, ref offset, BaseOffset, 8);
            AddString(RewardRecipe, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddInt(RawRewardGuildCredits, data, ref offset, BaseOffset, 16);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 20, StoredStringListTable);
            AddEnum(RewardSkill, data, ref offset, BaseOffset, 24);

            FinishSerializing(data, ref offset, BaseOffset, 26, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
