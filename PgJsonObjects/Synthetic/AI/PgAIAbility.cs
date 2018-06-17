namespace PgJsonObjects
{
    public class PgAIAbility : GenericPgObject, IPgAIAbility
    {
        public PgAIAbility(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAIAbility(data, offset);
        }

        public int? RawMinLevel { get { return GetInt(0); } }
        public int? RawMaxLevel { get { return GetInt(4); } }
        public int? RawMinDistance { get { return GetInt(8); } }
        public int? RawMinRange { get { return GetInt(12); } }
        public int? RawMaxRange { get { return GetInt(16); } }
        public int? RawCueValue { get { return GetInt(20); } }
        public AbilityCue Cue { get { return GetEnum<AbilityCue>(24); } }
    }
}
