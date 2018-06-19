﻿namespace PgJsonObjects
{
    public class PgItemSkillLink : GenericPgObject<PgItemSkillLink>, IPgItemSkillLink
    {
        public PgItemSkillLink(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgItemSkillLink CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItemSkillLink CreateNew(byte[] data, ref int offset)
        {
            return new PgItemSkillLink(data, ref offset);
        }

        public string SkillName { get { return GetString(0); } }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(4); } }
        public IPgSkill Link { get { return GetObject(8, ref _Link, PgSkill.CreateNew); } } private IPgSkill _Link;
    }
}
