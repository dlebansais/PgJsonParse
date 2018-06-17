using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SimpleServerInfoEffect : ServerInfoEffect, IPgSimpleServerInfoEffect
    {
        public SimpleServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, string EffectParameter)
            : base(ServerInfoEffect, RawLevel)
        {
            this.EffectParameter = EffectParameter;
        }

        public string EffectParameter { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + (EffectParameter != null ? "(" + EffectParameter + ")" : "");
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddString(EffectParameter, data, ref offset, BaseOffset, 4, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 8, StoredStringtable, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
