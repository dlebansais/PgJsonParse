using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgLoreBook : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        string Title { get; }
        string LocationHint { get; }
        List<LoreBookKeyword> KeywordList { get; }
        LoreBookCategory Category { get; }
        LoreBookVisibility Visibility { get; }
        string InternalName { get; }
        string Text { get; }
        bool IsClientLocal { get; }
        bool? RawIsClientLocal { get; }
        bool IsKeywordListEmpty { get; }
        bool? RawIsKeywordListEmpty { get; }
    }
}
