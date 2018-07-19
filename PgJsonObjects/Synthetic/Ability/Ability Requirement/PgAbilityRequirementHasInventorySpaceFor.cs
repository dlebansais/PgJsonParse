using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementHasInventorySpaceFor: PgAbilityRequirement<PgAbilityRequirementHasInventorySpaceFor>, IPgAbilityRequirementHasInventorySpaceFor
    {
        public PgAbilityRequirementHasInventorySpaceFor(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementHasInventorySpaceFor CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementHasInventorySpaceFor CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementHasInventorySpaceFor(data, ref offset);
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.HasInventorySpaceFor; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgItem Item { get { return GetObject(12, ref _Item, PgItem.CreateNew); } } private IPgItem _Item;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => Item != null ? Item.InternalName : null} },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return new List<IBackLinkable>() { Item };
        }
    }
}
