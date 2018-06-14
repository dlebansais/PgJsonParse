using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAbilityRequirementRace
    {
        List<Race> AllowedRaceList { get; }
    }
}
