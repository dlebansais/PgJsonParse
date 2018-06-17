using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementAppearance: GenericPgObject<PgAbilityRequirementAppearance>, IPgAbilityRequirementAppearance
    {
        public PgAbilityRequirementAppearance(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementAppearance CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementAppearance(data, offset);
        }

        public List<Appearance> AppearanceList { get { return GetEnumList(4, ref _AppearanceList); } } private List<Appearance> _AppearanceList;
    }
}
