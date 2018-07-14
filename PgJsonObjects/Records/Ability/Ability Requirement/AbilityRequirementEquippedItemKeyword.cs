using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementEquippedItemKeyword : AbilityRequirement, IPgAbilityRequirementEquippedItemKeyword
    {
        public AbilityRequirementEquippedItemKeyword(AbilityKeyword keyword, int? rawMinCount, int? rawMaxCount, ParseErrorInfo ErrorInfo)
        {
            Keyword = keyword;
            RawMinCount = rawMinCount;
            RawMaxCount = rawMaxCount;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.EquippedItemKeyword; } }
        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get; private set; }
        public int MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public int? RawMaxCount { get; private set; }
        public AbilityKeyword Keyword { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => StringToEnumConversion<AbilityKeyword>.ToString(Keyword) } },
            { "MinCount", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMinCount } },
            { "MaxCount", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxCount } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Keyword != AbilityKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[Keyword]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            AddInt(RawMinCount, data, ref offset, BaseOffset, 0);
            AddInt(RawMaxCount, data, ref offset, BaseOffset, 4);
            AddEnum(Keyword, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 10, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
