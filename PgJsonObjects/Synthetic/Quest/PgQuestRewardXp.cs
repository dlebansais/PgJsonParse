using System.Collections.Generic;

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
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
