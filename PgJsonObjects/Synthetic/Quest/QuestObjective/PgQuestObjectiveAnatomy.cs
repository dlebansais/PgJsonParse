namespace PgJsonObjects
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

        public IPgSkill ConnectedSkill { get { return GetObject(0, ref _ConnectedSkill, PgSkill.CreateNew); } } private IPgSkill _ConnectedSkill;
    }
}
