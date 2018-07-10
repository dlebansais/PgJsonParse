using System.Collections.Generic;

namespace PgJsonObjects
{
    /*
    public class SingleRaceAbilityRequirement : AbilityRequirement, IPgAbilityRequirementSingleRace
    {
        public SingleRaceAbilityRequirement(string RawAllowedRace, ParseErrorInfo ErrorInfo)
        {
            AllowedRace = StringToEnumConversion<Race>.Parse(RawAllowedRace, ErrorInfo);
        }

        public Race AllowedRace { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "AllowedRace", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Race>.ToString(AllowedRace) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (AllowedRace != Race.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[AllowedRace]);

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

            AddInt(((int)OtherRequirementType | 0x8000), data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 8, StoredStringListTable);
            AddEnum(AllowedRace, data, ref offset, BaseOffset, 12);

            FinishSerializing(data, ref offset, BaseOffset, 14, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
    */////////////////
}
