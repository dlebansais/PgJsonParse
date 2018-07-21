using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgLoreBook : MainPgObject<PgLoreBook>, IPgLoreBook
    {
        public PgLoreBook(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 34;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgLoreBook CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgLoreBook CreateNew(byte[] data, ref int offset)
        {
            return new PgLoreBook(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Title { get { return GetString(4); } }
        public string LocationHint { get { return GetString(8); } }
        public List<LoreBookKeyword> KeywordList { get { return GetEnumList(12, ref _KeywordList); } } private List<LoreBookKeyword> _KeywordList;
        public LoreBookCategory Category { get { return GetEnum<LoreBookCategory>(16); } }
        public LoreBookVisibility Visibility { get { return GetEnum<LoreBookVisibility>(18); } }
        public string InternalName { get { return GetString(20); } }
        public string Text { get { return GetString(24); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get { return GetBool(32, 0); } }
        public bool IsKeywordListEmpty { get { return RawIsKeywordListEmpty.HasValue ? RawIsKeywordListEmpty.Value : false; } }
        public bool? RawIsKeywordListEmpty { get { return GetBool(32, 2); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Title", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Title } },
            { "LocationHint", new FieldParser() {
                Type = FieldType.String,
                GetString = () => LocationHint } },
            { "Category", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<LoreBookCategory>.ToString(Category, null, LoreBookCategory.Internal_None) } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<LoreBookKeyword>.ToStringList(KeywordList),
                GetArrayIsEmpty = () => IsKeywordListEmpty } },
            { "IsClientLocal", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsClientLocal  } },
            { "Visibility", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<LoreBookVisibility>.ToString(Visibility, null, LoreBookVisibility.Internal_None) } },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InternalName } },
            { "Text", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Text } },
        }; } }

        public override string SortingName { get { return Title; } }
        public string SearchResultIconFileName { get { return "icon_" + LoreBook.SearchResultIconId; } }

        public int? Id
        {
            get
            {
                if (Key.Length > 5)
                {
                    if (int.TryParse(Key.Substring(5), out int Result))
                        return Result;
                }

                return null;
            }
        }
    }
}
