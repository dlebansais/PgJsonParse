namespace PgJsonObjects
{
    public class PgLoreBookInfo : MainPgObject, IPgLoreBookInfo
    {
        public PgLoreBookInfo(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgLoreBookInfo(data, offset);
        }

        public LoreBookInfoCategory Gods { get { return GetObject(0, ref _Gods); } } private LoreBookInfoCategory _Gods;
        public LoreBookInfoCategory Misc { get { return GetObject(4, ref _Misc); } } private LoreBookInfoCategory _Misc;
        public LoreBookInfoCategory History { get { return GetObject(8, ref _History); } } private LoreBookInfoCategory _History;
        public LoreBookInfoCategory Plot { get { return GetObject(12, ref _Plot); } } private LoreBookInfoCategory _Plot;
        public LoreBookInfoCategory Stories { get { return GetObject(16, ref _Stories); } } private LoreBookInfoCategory _Stories;
        public LoreBookInfoCategory GuideProgram { get { return GetObject(20, ref _GuideProgram); } } private LoreBookInfoCategory _GuideProgram;
    }
}
