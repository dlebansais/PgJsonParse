using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardLevel : GenericPgObject<PgQuestRewardLevel>, IPgQuestRewardLevel
    {
        public PgQuestRewardLevel(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestRewardLevel CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestRewardLevel CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestRewardLevel(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgSkill Skill { get { return GetObject(0, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }
    }
}
