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

        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get { return GetInt(0); } }
        public List<Race> RaceRestrictionList { get { return GetEnumList(4, ref _RaceRestrictionList); } } private List<Race> _RaceRestrictionList;
        public IPgAbility Ability { get { return GetObject(8, ref _Ability, PgAbility.CreateNew); } } private IPgAbility _Ability;
        public string Notes { get { return GetString(12); } }
        public IPgRecipe Recipe { get { return GetObject(16, ref _Recipe, PgRecipe.CreateNew); } } private IPgRecipe _Recipe;
        public IPgSkill BonusSkill { get { return GetObject(20, ref _BonusSkill, PgSkill.CreateNew); } } private IPgSkill _BonusSkill;
    }
}
