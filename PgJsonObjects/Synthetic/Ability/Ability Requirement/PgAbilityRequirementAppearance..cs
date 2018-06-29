using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementAppearance: GenericPgObject<PgAbilityRequirementAppearance>, IPgAbilityRequirementAppearance
    {
        public PgAbilityRequirementAppearance(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementAppearance CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementAppearance CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementAppearance(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public List<Appearance> AppearanceList { get { return GetEnumList(8, ref _AppearanceList); } } private List<Appearance> _AppearanceList;
    }
}
