using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementHasEffectKeyword : AbilityRequirement, IPgAbilityRequirementHasEffectKeyword
    {
        public AbilityRequirementHasEffectKeyword(AbilityKeyword keyword)
        {
            Keyword = keyword;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.HasEffectKeyword; } }
        public AbilityKeyword Keyword { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => StringToEnumConversion<AbilityKeyword>.ToString(Keyword) } },
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

            AddEnum(Keyword, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 2, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
