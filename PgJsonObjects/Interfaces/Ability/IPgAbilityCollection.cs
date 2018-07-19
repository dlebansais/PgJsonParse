using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAbilityCollection : IList<IPgAbility>, IPgCollection, IPgBackLinkableCollection<IPgAbility>
    {
    }
}
