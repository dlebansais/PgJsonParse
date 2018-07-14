using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementPetCount : AbilityRequirement, IPgAbilityRequirementPetCount
    {
        public AbilityRequirementPetCount(RecipeKeyword petTypeTag, double? rawMaxCount)
        {
            PetTypeTag = petTypeTag;
            RawMaxCount = rawMaxCount;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.PetCount; } }
        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public double? RawMaxCount { get; private set; }
        public RecipeKeyword PetTypeTag { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
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

                if (PetTypeTag != RecipeKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeKeywordTextMap[PetTypeTag]);

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

            AddDouble(RawMaxCount, data, ref offset, BaseOffset, 0);
            AddEnum(PetTypeTag, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 6, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
