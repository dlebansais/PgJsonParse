using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ValueServerInfoEffect : ServerInfoEffect, IPgValueServerInfoEffect
    {
        public ValueServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int Value, bool sameValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.RawValue = Value;
            SameValue = sameValue;
        }

        public int Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public int? RawValue { get; private set; }

        public bool SameValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + Value + (SameValue ? ("," + Value) : "") + ")";
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddInt(RawValue, data, ref offset, BaseOffset, 4);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 8, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
