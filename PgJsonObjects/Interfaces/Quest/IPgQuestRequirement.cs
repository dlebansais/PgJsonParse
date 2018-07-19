using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestRequirement
    {
        IList<IBackLinkable> GetLinkBack();
    }
}
