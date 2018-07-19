using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAbilityRequirement
    {
        OtherRequirementType Type { get; }
        IList<IBackLinkable> GetLinkBack();
    }
}
