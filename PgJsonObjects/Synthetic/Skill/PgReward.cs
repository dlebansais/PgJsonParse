using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgReward : GenericPgObject<PgReward>, IPgReward
    {
        public PgReward(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgReward CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgReward CreateNew(byte[] data, ref int offset)
        {
            return new PgReward(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get { return GetInt(4); } }
        public List<Race> RaceRestrictionList { get { return GetEnumList(8, ref _RaceRestrictionList); } } private List<Race> _RaceRestrictionList;
        public IPgAbility Ability { get { return GetObject(12, ref _Ability, PgAbility.CreateNew); } } private IPgAbility _Ability;
        public string Notes { get { return GetString(16); } }
        public IPgRecipe Recipe { get { return GetObject(20, ref _Recipe, PgRecipe.CreateNew); } } private IPgRecipe _Recipe;
        public IPgSkill BonusSkill { get { return GetObject(24, ref _BonusSkill, PgSkill.CreateNew); } } private IPgSkill _BonusSkill;
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
