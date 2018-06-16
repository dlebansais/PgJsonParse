namespace PgJsonObjects
{
    public interface IPgLoreBookInfo
    {
        LoreBookInfoCategory Gods { get; }
        LoreBookInfoCategory Misc { get; }
        LoreBookInfoCategory History { get; }
        LoreBookInfoCategory Plot { get; }
        LoreBookInfoCategory Stories { get; }
        LoreBookInfoCategory GuideProgram { get; }
    }
}
