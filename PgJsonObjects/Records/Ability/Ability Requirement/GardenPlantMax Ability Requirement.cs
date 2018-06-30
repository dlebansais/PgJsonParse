using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GardenPlantMaxAbilityRequirement : AbilityRequirement, IPgAbilityRequirementGardenPlantMax
    {
        public GardenPlantMaxAbilityRequirement(string RawTypeTag, int? RawMax, ParseErrorInfo ErrorInfo)
        {
            AbilityTypeTag ParsedTypeTag;
            StringToEnumConversion<AbilityTypeTag>.TryParse(RawTypeTag, out ParsedTypeTag, ErrorInfo);
            TypeTag = ParsedTypeTag;
            this.RawMax = RawMax;
        }

        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        public int? RawMax { get; private set; }
        public AbilityTypeTag TypeTag { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
/*            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.GardenPlantMax) } },*/
            { "TypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityTypeTag>.ToString(TypeTag) } },
            { "Max", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => Max } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.AbilityTypeTagTextMap[TypeTag]);

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
            AddInt(RawMax, data, ref offset, BaseOffset, 8);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 12, StoredStringListTable);
            AddEnum(TypeTag, data, ref offset, BaseOffset, 16);

            FinishSerializing(data, ref offset, BaseOffset, 20, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
