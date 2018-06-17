using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgReward : GenericPgObject, IPgReward
    {
        public PgReward(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgReward(data, offset);
        }

        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get { return GetInt(0); } }
        public List<Race> RaceRestrictionList { get { return GetEnumList(4, ref _RaceRestrictionList); } } private List<Race> _RaceRestrictionList;
        public Ability Ability { get { return GetObject(8, ref _Ability); } } private Ability _Ability;
        public string Notes { get { return GetString(12); } }
        public Recipe Recipe { get { return GetObject(16, ref _Recipe); } } private Recipe _Recipe;
        public Skill BonusSkill { get { return GetObject(20, ref _BonusSkill); } } private Skill _BonusSkill;
    }
}
