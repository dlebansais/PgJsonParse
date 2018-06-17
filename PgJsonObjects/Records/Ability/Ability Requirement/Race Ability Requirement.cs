using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RaceAbilityRequirement : AbilityRequirement, IPgAbilityRequirementRace
    {
        public RaceAbilityRequirement(List<string> RawAllowedRaceList, ParseErrorInfo ErrorInfo)
        {
            List<string> MixedList = new List<string>();
            MixedList.AddRange(RawAllowedRaceList);

            foreach (string Item in MixedList)
            {
                Race ParsedAllowedRace;
                StringToEnumConversion<Race>.TryParse(Item, out ParsedAllowedRace, ErrorInfo);
                AllowedRaceList.Add(ParsedAllowedRace);
            }
        }

        public List<Race> AllowedRaceList { get; } = new List<Race>();

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Race) } },
            { "AllowedRace", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => StringToEnumConversion<Race>.ToStringList(AllowedRaceList) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Race Item in AllowedRaceList)
                    AddWithFieldSeparator(ref Result, TextMaps.RaceTextMap[Item]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddEnumList(AllowedRaceList, data, ref offset, BaseOffset, 4, StoredEnumListTable);

            FinishSerializing(data, ref offset, BaseOffset, 8, null, null, null, StoredEnumListTable, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
