using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SingleRaceAbilityRequirement : AbilityRequirement, IPgAbilityRequirementSingleRace
    {
        public SingleRaceAbilityRequirement(string RawAllowedRace, ParseErrorInfo ErrorInfo)
        {
            AllowedRace = StringToEnumConversion<Race>.Parse(RawAllowedRace, ErrorInfo);
        }

        public Race AllowedRace { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Race) } },
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

            AddEnum(AllowedRace, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 2, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
