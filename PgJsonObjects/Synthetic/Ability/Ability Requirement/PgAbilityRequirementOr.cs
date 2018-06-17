﻿namespace PgJsonObjects
{
    public class PgAbilityRequirementOr: GenericPgObject<PgAbilityRequirementOr>, IPgAbilityRequirementOr
    {
        public PgAbilityRequirementOr(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementOr CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementOr(data, offset);
        }

        public AbilityRequirementCollection OrList { get { return GetObjectList(4, ref _OrList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OrList;
        public string ErrorMsg { get { return GetString(8); } }
    }
}
