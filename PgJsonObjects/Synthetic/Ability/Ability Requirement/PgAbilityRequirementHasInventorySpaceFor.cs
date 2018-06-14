namespace PgJsonObjects
{
    public class PgAbilityRequirementHasInventorySpaceFor: GenericPgObject, IPgAbilityRequirementHasInventorySpaceFor
    {
        public PgAbilityRequirementHasInventorySpaceFor(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item Item { get { return GetObject(0, ref _Item); } } private Item _Item;
    }
}
