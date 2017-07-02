namespace PgJsonObjects
{
    public class DruidEventStateAbilityRequirement : AbilityRequirement
    {
        public DruidEventStateAbilityRequirement(string RawDisallowedState, ParseErrorInfo ErrorInfo)
        {
            DisallowedState ParsedDisallowedState;
            StringToEnumConversion<DisallowedState>.TryParse(RawDisallowedState, out ParsedDisallowedState, ErrorInfo);
            DisallowedState = ParsedDisallowedState;
        }

        public DisallowedState DisallowedState { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "DruidEventState");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.DisallowedStateTextMap[DisallowedState]);

                return Result;
            }
        }
        #endregion
    }
}
