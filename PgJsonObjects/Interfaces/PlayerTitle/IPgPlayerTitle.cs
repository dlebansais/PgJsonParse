using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgPlayerTitle
    {
        string Title { get; }
        string RawTitle { get; }
        string Tooltip { get; }
        List<TitleKeyword> KeywordList { get; }
    }
}
