namespace PgJsonObjects
{
    public class PgAbilityRequirementOr: GenericPgObject<PgAbilityRequirementOr>, IPgAbilityRequirementOr
    {
        public PgAbilityRequirementOr(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementOr CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementOr CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementOr(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public AbilityRequirementCollection OrList { get { return GetObjectList(8, ref _OrList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OrList;
        public string ErrorMsg { get { return GetString(12); } }
    }
}
