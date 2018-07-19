using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPlayerTitle : MainPgObject<PgPlayerTitle>, IPgPlayerTitle
    {
        public PgPlayerTitle(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 24;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgPlayerTitle CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPlayerTitle CreateNew(byte[] data, ref int offset)
        {
            return new PgPlayerTitle(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Title { get { return GetString(4); } }
        public string RawTitle { get { return GetString(8); } }
        public string Tooltip { get { return GetString(12); } }
        public List<TitleKeyword> KeywordList { get { return GetEnumList(16, ref _KeywordList); } } private List<TitleKeyword> _KeywordList;
        protected override List<string> FieldTableOrder { get { return GetStringList(20, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        public int? Id
        {
            get
            {
                int Result;
                if (Key.Length > 6 && int.TryParse(Key.Substring(6), out Result))
                    return Result;
                else
                    return null;
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Title", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawTitle } },
            { "Tooltip", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Tooltip } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<TitleKeyword>.ToStringList(KeywordList) } },
        }; } }

        public override string SortingName { get { return Title; } }
        public string SearchResultIconFileName { get { return "icon_" + PlayerTitle.SearchResultIconId; } }
    }
}
