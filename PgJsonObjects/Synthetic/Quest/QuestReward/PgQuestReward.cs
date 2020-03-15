﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestReward : GenericPgObject<PgQuestReward>, IPgQuestReward
    {
        public PgQuestReward(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestReward CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestReward CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestReward(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Type { get { return GetString(4); } }
        public int RewardXp { get { return RawRewardXp.HasValue ? RawRewardXp.Value : 0; } }
        public int? RawRewardXp { get { return GetInt(8); } }
        public string RewardRecipe { get { return GetString(12); } }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get { return GetInt(16); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(20, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public PowerSkill RewardSkill { get { return GetEnum<PowerSkill>(24); } }

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

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}