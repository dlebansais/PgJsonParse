using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgTrainingSource : PgGenericSource<PgTrainingSource>, IPgTrainingSource
    {
        public PgTrainingSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgTrainingSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgTrainingSource CreateNew(byte[] data, ref int offset)
        {
            return new PgTrainingSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
            Parent.AddLinkBack(Npc);
        }

        public string NpcName { get { return GetString(PropertiesOffset + 0); } }
        public IPgGameNpc Npc { get { return GetObject(PropertiesOffset + 4, ref _Npc, PgGameNpc.CreateNew); } } private IPgGameNpc _Npc;

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
