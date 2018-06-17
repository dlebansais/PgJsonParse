namespace PgJsonObjects
{
    public class IntervalServerInfoEffect : ServerInfoEffect, IPgIntervalServerInfoEffect
    {
        public IntervalServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int LowValue, int HighValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.RawLowValue = LowValue;
            this.RawHighValue = HighValue;
        }

        public int LowValue { get { return RawLowValue.HasValue ? RawLowValue.Value : 0; } }
        public int? RawLowValue { get; private set; }
        public int HighValue { get { return RawHighValue.HasValue ? RawHighValue.Value : 0; } }
        public int? RawHighValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + LowValue + "," + HighValue + ")";
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddInt(RawLowValue, data, ref offset, BaseOffset, 4);
            AddInt(RawHighValue, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
