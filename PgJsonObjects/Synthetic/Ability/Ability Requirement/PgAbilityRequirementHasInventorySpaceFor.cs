namespace PgJsonObjects
{
    public class PgAbilityRequirementHasInventorySpaceFor: GenericPgObject<PgAbilityRequirementHasInventorySpaceFor>, IPgAbilityRequirementHasInventorySpaceFor
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

        public IPgItem Item { get { return GetObject(4, ref _Item, PgItem.CreateNew); } } private IPgItem _Item;
    }
}
