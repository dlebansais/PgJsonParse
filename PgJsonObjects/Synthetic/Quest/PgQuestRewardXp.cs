namespace PgJsonObjects
{
    public class PgQuestRewardXp : GenericPgObject<PgQuestRewardXp>, IPgQuestRewardXp
    {
        public PgQuestRewardXp(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestRewardXp CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestRewardXp CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestRewardXp(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgSkill Skill { get { return GetObject(0, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get { return GetInt(4); } }
    }
}
