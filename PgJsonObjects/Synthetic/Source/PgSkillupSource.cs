using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSkillupSource : PgGenericSource<PgSkillupSource>, IPgSkillupSource
    {
        public PgSkillupSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgSkillupSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSkillupSource CreateNew(byte[] data, ref int offset)
        {
            return new PgSkillupSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
            Parent.AddLinkBack(Skill);
        }

        public IPgSkill Skill { get { return GetObject(PropertiesOffset + 0, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
