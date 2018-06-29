﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveAnatomy : GenericPgObject<PgQuestObjectiveAnatomy>, IPgQuestObjectiveAnatomy
    {
        public PgQuestObjectiveAnatomy(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveAnatomy CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveAnatomy CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveAnatomy(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgSkill Skill { get { return GetObject(4, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
    }
}
