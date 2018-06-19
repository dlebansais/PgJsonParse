namespace PgJsonObjects
{
    public interface IPgLoreBookInfo
    {
        IPgLoreBookInfoCategory Gods { get; }
        IPgLoreBookInfoCategory Misc { get; }
        IPgLoreBookInfoCategory History { get; }
        IPgLoreBookInfoCategory Plot { get; }
        IPgLoreBookInfoCategory Stories { get; }
        IPgLoreBookInfoCategory GuideProgram { get; }
    }
}
