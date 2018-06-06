using System.Collections.Generic;

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

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.DruidEventState) } },
            { "DisallowedStates", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<DisallowedState>.ToString(DisallowedState) } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "DruidEventState");
            Generator.AddEnum("DisallowedStates", DisallowedState);
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
