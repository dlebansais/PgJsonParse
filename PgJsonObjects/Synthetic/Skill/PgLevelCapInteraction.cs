using System.Collections.Generic;

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

        public override string Key { get { return GetString(0); } }
        public int OtherLevel { get { return RawOtherLevel.Value; } }
        public int? RawOtherLevel { get { return GetInt(4); } }
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get { return GetInt(8); } }
        public IPgSkill Link { get { return GetObject(12, ref _Link, PgSkill.CreateNew); } } private IPgSkill _Link;
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
    }
}
