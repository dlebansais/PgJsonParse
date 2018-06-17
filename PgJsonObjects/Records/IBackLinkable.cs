using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IBackLinkable
    {
        Dictionary<Type, List<ISearchableObject>> LinkBackTable { get; }
        bool HasLinkBackTableEntries { get; }
    }
}
