using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DrinkEffectServerInfoEffect : ServerInfoEffect, IPgDrinkEffectServerInfoEffect
    {
        public DrinkEffectServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int DrinkATValue, int AlcoholPowerValue)
            : base(ServerInfoEffect, RawLevel)
        {
            RawDrinkATValue = DrinkATValue;
            RawAlcoholPowerValue = AlcoholPowerValue;
        }

        public int DrinkATValue { get { return RawDrinkATValue.HasValue ? RawDrinkATValue.Value : 0; } }
        public int? RawDrinkATValue { get; private set; }
        public int AlcoholPowerValue { get { return RawAlcoholPowerValue.HasValue ? RawAlcoholPowerValue.Value : 0; } }
        public int? RawAlcoholPowerValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + DrinkATValue + "," + AlcoholPowerValue + ")";
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddInt(RawDrinkATValue, data, ref offset, BaseOffset, 4);
            AddInt(RawAlcoholPowerValue, data, ref offset, BaseOffset, 8);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 12, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, null, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
