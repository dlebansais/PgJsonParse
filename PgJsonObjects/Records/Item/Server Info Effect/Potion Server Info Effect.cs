using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PotionServerInfoEffect : ServerInfoEffect, IPgPotionServerInfoEffect
    {
        public PotionServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int? HealthGainInstant, int? ArmorGainInstant, int? PowerGainInstant)
            : base(ServerInfoEffect, RawLevel)
        {
            EffectString = HealthGainInstant.Value.ToString();

            if (ArmorGainInstant.HasValue)
                EffectString += "," + ArmorGainInstant.Value.ToString();

            if (PowerGainInstant.HasValue)
                EffectString += "," + PowerGainInstant.Value.ToString();
        }

        public PotionServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int? HealthGainPerSecond, int? ArmorGainPerSecond, int? PowerGainPerSecond, int? TotalGainDuration)
            : base(ServerInfoEffect, RawLevel)
        {
            EffectString = HealthGainPerSecond.Value + "," + ArmorGainPerSecond.Value + "," + PowerGainPerSecond.Value + ",0," + TotalGainDuration.Value;
        }

        public string EffectString { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + EffectString + ")";
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddString(EffectString, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 8, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
