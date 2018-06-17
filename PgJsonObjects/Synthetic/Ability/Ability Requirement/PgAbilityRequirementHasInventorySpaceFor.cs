namespace PgJsonObjects
{
    public class PgAbilityRequirementHasInventorySpaceFor: GenericPgObject<PgAbilityRequirementHasInventorySpaceFor>, IPgAbilityRequirementHasInventorySpaceFor
    {
        public PgAbilityRequirementHasInventorySpaceFor(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementHasInventorySpaceFor CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementHasInventorySpaceFor(data, offset);
        }

        public Item Item { get { return GetObject(4, ref _Item); } } private Item _Item;
    }
}
