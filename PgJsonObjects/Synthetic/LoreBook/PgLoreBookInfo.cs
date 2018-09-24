using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgLoreBookInfo : MainPgObject<PgLoreBookInfo>, IPgLoreBookInfo
    {
        public PgLoreBookInfo(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 36;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgLoreBookInfo CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgLoreBookInfo CreateNew(byte[] data, ref int offset)
        {
            return new PgLoreBookInfo(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgLoreBookInfoCategory Gods { get { return GetObject(4, ref _Gods, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _Gods;
        public IPgLoreBookInfoCategory Misc { get { return GetObject(8, ref _Misc, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _Misc;
        public IPgLoreBookInfoCategory History { get { return GetObject(12, ref _History, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _History;
        public IPgLoreBookInfoCategory Plot { get { return GetObject(16, ref _Plot, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _Plot;
        public IPgLoreBookInfoCategory Stories { get { return GetObject(20, ref _Stories, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _Stories;
        public IPgLoreBookInfoCategory GuideProgram { get { return GetObject(24, ref _GuideProgram, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _GuideProgram;
        public IPgLoreBookInfoCategory NotesAndSigns { get { return GetObject(28, ref _NotesAndSigns, PgLoreBookInfoCategory.CreateNew); } } private IPgLoreBookInfoCategory _NotesAndSigns;
        protected override List<string> FieldTableOrder { get { return GetStringList(32, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Gods", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => Gods as IObjectContentGenerator } },
            { "Misc", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => Misc as IObjectContentGenerator } },
            { "History", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => History as IObjectContentGenerator } },
            { "Plot", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => Plot as IObjectContentGenerator } },
            { "Stories", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => Stories as IObjectContentGenerator } },
            { "GuideProgram", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => GuideProgram as IObjectContentGenerator } },
            { "NotesAndSigns", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => NotesAndSigns as IObjectContentGenerator } },
        }; } }

        public override string SortingName { get { return ""; } }
        public string SearchResultIconFileName { get { return "icon_" + LoreBookInfo.SearchResultIconId; } }
    }
}
