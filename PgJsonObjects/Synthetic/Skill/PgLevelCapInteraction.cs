namespace PgJsonObjects
{
    public class PgLevelCapInteraction : GenericPgObject<PgLevelCapInteraction>, IPgLevelCapInteraction
    {
        public PgLevelCapInteraction(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgLevelCapInteraction CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgLevelCapInteraction CreateNew(byte[] data, ref int offset)
        {
            return new PgLevelCapInteraction(data, ref offset);
        }

        public int OtherLevel { get { return RawOtherLevel.Value; } }
        public int? RawOtherLevel { get { return GetInt(0); } }
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get { return GetInt(4); } }
        public IPgSkill Link { get { return GetObject(8, ref _Link, PgSkill.CreateNew); } } private IPgSkill _Link;
    }
}
