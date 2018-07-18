using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSkillAndLevelServerInfoEffect : GenericPgObject<PgSkillAndLevelServerInfoEffect>, IPgServerInfoEffect, IPgSkillAndLevelServerInfoEffect
    {
        public PgSkillAndLevelServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgSkillAndLevelServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSkillAndLevelServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgSkillAndLevelServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgSkill Skill { get { return GetObject(4, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }
    }
}
