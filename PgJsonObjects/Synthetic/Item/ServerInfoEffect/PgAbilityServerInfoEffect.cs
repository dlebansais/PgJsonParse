using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityServerInfoEffect : GenericPgObject<PgAbilityServerInfoEffect>, IPgServerInfoEffect, IPgAbilityServerInfoEffect
    {
        public PgAbilityServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgAbility BestowAbility { get { return GetObject(4, ref _BestowAbility, PgAbility.CreateNew); } } private IPgAbility _BestowAbility;
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }
    }
}
