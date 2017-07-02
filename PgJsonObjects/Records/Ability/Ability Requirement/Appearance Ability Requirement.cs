using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AppearanceAbilityRequirement : AbilityRequirement
    {
        public AppearanceAbilityRequirement(string RawAppearance, List<string> RawAppearanceList, ParseErrorInfo ErrorInfo)
        {
            List<string> MixedList = new List<string>();
            if (RawAppearance != null)
                MixedList.Add(RawAppearance);
            else
                MixedList.AddRange(RawAppearanceList);

            foreach (string Item in MixedList)
            {
                Appearance ParsedAppearance;
                StringToEnumConversion<Appearance>.TryParse(Item, out ParsedAppearance, ErrorInfo);
                AppearanceList.Add(ParsedAppearance);
            }
        }

        public List<Appearance> AppearanceList { get; } = new List<Appearance>();

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "Appearance");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Appearance Item in AppearanceList)
                    AddWithFieldSeparator(ref Result, TextMaps.AppearanceTextMap[Item]);

                return Result;
            }
        }
        #endregion
    }
}
