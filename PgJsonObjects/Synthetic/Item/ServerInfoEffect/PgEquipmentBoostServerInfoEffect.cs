using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgEquipmentBoostServerInfoEffect : GenericPgObject<PgEquipmentBoostServerInfoEffect>, IPgServerInfoEffect, IPgEquipmentBoostServerInfoEffect
    {
        public PgEquipmentBoostServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgEquipmentBoostServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgEquipmentBoostServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgEquipmentBoostServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgItemEffect Boost { get { return GetObject(4, ref _Boost, ItemEffectCollection.CreateNew); } } private IPgItemEffect _Boost;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get { return (float)GetDouble(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
    }
}
