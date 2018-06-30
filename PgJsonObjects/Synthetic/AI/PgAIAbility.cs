using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAIAbility : GenericPgObject<PgAIAbility>, IPgAIAbility
    {
        public PgAIAbility(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAIAbility CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAIAbility CreateNew(byte[] data, ref int offset)
        {
            return new PgAIAbility(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int? RawMinLevel { get { return GetInt(4); } }
        public int? RawMaxLevel { get { return GetInt(8); } }
        public int? RawMinDistance { get { return GetInt(12); } }
        public int? RawMinRange { get { return GetInt(16); } }
        public int? RawMaxRange { get { return GetInt(20); } }
        public int? RawCueValue { get { return GetInt(24); } }
        public AbilityCue Cue { get { return GetEnum<AbilityCue>(28); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
