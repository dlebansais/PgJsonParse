using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IBackLinkable
    {
        string SortingName { get; }
        Dictionary<Type, List<IBackLinkable>> LinkBackTable { get; }
        bool HasLinkBackTableEntries { get; }
        void SortLinkBack();
        string GetSearchResultContentTemplateName();
        string GetSearchResultTitleTemplateName();
    }
}
