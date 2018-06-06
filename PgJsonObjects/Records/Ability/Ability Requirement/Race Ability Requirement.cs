using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RaceAbilityRequirement : AbilityRequirement
    {
        public RaceAbilityRequirement(string RawAllowedRace, List<string> RawAllowedRaceList, ParseErrorInfo ErrorInfo)
        {
            List<string> MixedList = new List<string>();
            if (RawAllowedRace != null)
                MixedList.Add(RawAllowedRace);
            else
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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "Race");

            if (AllowedRaceList.Count > 1)
            {
                Generator.OpenArray("AllowedRace");

                foreach (Race Item in AllowedRaceList)
                    Generator.AddEnum(null, Item);

                Generator.CloseArray();
            }
            else if (AllowedRaceList.Count > 0)
                Generator.AddEnum("AllowedRace", AllowedRaceList[0]);
        }
        #endregion

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
    }
}
