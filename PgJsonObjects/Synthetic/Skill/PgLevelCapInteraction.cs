namespace PgJsonObjects
{
    public class PgLevelCapInteraction : GenericPgObject, IPgLevelCapInteraction
    {
        public PgLevelCapInteraction(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgLevelCapInteraction(data, offset);
        }

        public int OtherLevel { get { return RawOtherLevel.Value; } }
        public int? RawOtherLevel { get { return GetInt(0); } }
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get { return GetInt(4); } }
        public Skill Link { get { return GetObject(8, ref _Link); } } private Skill _Link;
    }
}
