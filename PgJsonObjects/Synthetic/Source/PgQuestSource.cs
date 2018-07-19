using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestSource : PgGenericSource<PgQuestSource>, IPgQuestSource
    {
        public PgQuestSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgQuestSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestSource CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
            Parent.AddLinkBack(Quest);
        }

        public IPgQuest Quest { get { return GetObject(PropertiesOffset + 0, ref _Quest, PgQuest.CreateNew); } } private IPgQuest _Quest;

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
