namespace PgJsonObjects
{
    public class PgQuestObjectiveAnatomy : GenericPgObject, IPgQuestObjectiveAnatomy
    {
        public PgQuestObjectiveAnatomy(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Skill ConnectedSkill { get { return GetObject(0, ref _ConnectedSkill); } } private Skill _ConnectedSkill;
    }
}
