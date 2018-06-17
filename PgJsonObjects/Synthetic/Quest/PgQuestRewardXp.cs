namespace PgJsonObjects
{
    public class PgQuestRewardXp : GenericPgObject, IPgQuestRewardXp
    {
        public PgQuestRewardXp(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Skill Skill { get { return GetObject(0, ref _Skill); } } private Skill _Skill;
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get { return GetInt(4); } }
    }
}
