using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAdvancementTable : MainPgObject<PgAdvancementTable>, IPgAdvancementTable
    {
        public PgAdvancementTable(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 12;
            int Count = FieldTableOrder.Count;
            offset += Count * 4;

            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAdvancementTable CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAdvancementTable CreateNew(byte[] data, ref int offset)
        {
            PgAdvancementTable Result = new PgAdvancementTable(data, ref offset);
            Result.InitLevels();

            return Result;
        }

        public void InitLevels()
        {
            Levels = new PgAdvancement[FieldTableOrder.Count];

            for (int i = 0; i < FieldTableOrder.Count; i++)
            {
                string Field = FieldTableOrder[i];

                FieldParser NewFieldParser = new FieldParser();
                NewFieldParser.Type = FieldType.Object;
                NewFieldParser.GetObject = GetCreatorHandler(i);
                FieldTable.Add(Field, NewFieldParser);
            }
        }

        private Func<IObjectContentGenerator> GetCreatorHandler(int index)
        {
            return () => GetLevel(index);
        }

        private IObjectContentGenerator GetLevel(int index)
        {
            return GetObject(12 + index * 4, ref Levels[index], PgAdvancement.CreateNew);
        }

        public PgAdvancement[] Levels;
        public override string Key { get { return GetString(0); } }
        public string InternalName { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable
        {
            get { return _FieldTable; }
        }

        private Dictionary<string, FieldParser> _FieldTable = new Dictionary<string, FieldParser>();
    }
}
