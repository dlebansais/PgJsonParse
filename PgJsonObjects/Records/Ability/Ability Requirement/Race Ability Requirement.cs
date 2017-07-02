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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "Race");

            Generator.CloseObject();
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
