using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementRace: GenericPgObject<PgAbilityRequirementRace>, IPgAbilityRequirementRace
    {
        public PgAbilityRequirementRace(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementRace CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementRace(data, offset);
        }

        public List<Race> AllowedRaceList { get { return GetEnumList(4, ref _AllowedRaceList); } } private List<Race> _AllowedRaceList;
    }
}
