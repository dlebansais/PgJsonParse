using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTableLevel : GenericJsonObject<XpTableLevel>, IPgXpTableLevel
    {
        public XpTableLevel(int Level, int Xp, int TotalXp)
        {
            RawLevel = Level;
            RawXp = Xp;
            RawTotalXp = TotalXp;
        }

        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get; private set; }
        public int Xp { get { return RawXp.Value; } }
        public int? RawXp { get; private set; }
        public int TotalXp { get { return RawTotalXp.Value; } }
        public int? RawTotalXp { get; private set; }

        protected override string SortingName { get { return null; } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
        }; } }

        public override string TextContent { get { return ""; } }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }

        #region Debugging
        protected override string FieldTableName { get { return "XpTableLevel"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddInt(RawLevel, data, ref offset, BaseOffset, 0);
            AddInt(RawXp, data, ref offset, BaseOffset, 4);
            AddInt(RawTotalXp, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
