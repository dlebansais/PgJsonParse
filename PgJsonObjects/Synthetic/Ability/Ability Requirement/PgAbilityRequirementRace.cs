using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementRace: GenericPgObject, IPgAbilityRequirementRace
    {
        public PgAbilityRequirementRace(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<Race> AllowedRaceList { get { return GetEnumList(0, ref _AllowedRaceList); } } private List<Race> _AllowedRaceList;
    }
}
