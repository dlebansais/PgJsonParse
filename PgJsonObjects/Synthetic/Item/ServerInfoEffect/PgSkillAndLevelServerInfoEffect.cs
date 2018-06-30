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

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
