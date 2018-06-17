namespace PgJsonObjects
{
    public class PoetryServerInfoEffect : ServerInfoEffect, IPgPoetryServerInfoEffect
    {
        public PoetryServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int PoetryXpValue, int RecitalXpValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.RawPoetryXpValue = PoetryXpValue;
            this.RawRecitalXpValue = RecitalXpValue;
        }

        public int PoetryXpValue { get { return RawPoetryXpValue.HasValue ? RawPoetryXpValue.Value : 0; } }
        public int? RawPoetryXpValue { get; private set; }
        public int RecitalXpValue { get { return RawRecitalXpValue.HasValue ? RawRecitalXpValue.Value : 0; } }
        public int? RawRecitalXpValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + PoetryXpValue + "," + RecitalXpValue + ")";
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddInt(RawPoetryXpValue, data, ref offset, BaseOffset, 4);
            AddInt(RawRecitalXpValue, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
