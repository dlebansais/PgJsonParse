using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PetCountAbilityRequirement : AbilityRequirement, IPgAbilityRequirementPetCount
    {
        public PetCountAbilityRequirement(string RawPetTypeTag, double? RawMaxCount, ParseErrorInfo ErrorInfo)
        {
            RecipeKeyword ParsedPetTypeTag;
            StringToEnumConversion<RecipeKeyword>.TryParse(RawPetTypeTag, out ParsedPetTypeTag, ErrorInfo);
            PetTypeTag = ParsedPetTypeTag;

            this.RawMaxCount = RawMaxCount;
        }

        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public double? RawMaxCount { get; private set; }
        public RecipeKeyword PetTypeTag { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.PetCount) } },
            { "PetTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeKeyword>.ToString(PetTypeTag) } },
            { "MaxCount", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxCount } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.RecipeKeywordTextMap[PetTypeTag]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddDouble(RawMaxCount, data, ref offset, BaseOffset, 8);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 12, StoredStringListTable);
            AddEnum(PetTypeTag, data, ref offset, BaseOffset, 16);

            FinishSerializing(data, ref offset, BaseOffset, 18, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
