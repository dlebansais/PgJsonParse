using System.Collections.Generic;

namespace PgJsonObjects
{
    public class EquippedItemKeywordAbilityRequirement : AbilityRequirement, IPgAbilityRequirementEquippedItemKeyword
    {
        public EquippedItemKeywordAbilityRequirement(string RawKeyword, int? rawMinCount, int? rawMaxCount, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword ParsedKeyword;
            StringToEnumConversion<AbilityKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo);
            Keyword = ParsedKeyword;
            RawMinCount = rawMinCount;
            RawMaxCount = rawMaxCount;
        }

        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get; private set; }
        public int MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public int? RawMaxCount { get; private set; }
        public AbilityKeyword Keyword { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.EquippedItemKeyword) } },
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

                AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[Keyword]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddDouble(RawMinCount, data, ref offset, BaseOffset, 0);
            AddDouble(RawMaxCount, data, ref offset, BaseOffset, 4);
            AddEnum(Keyword, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 10, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
