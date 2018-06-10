using System.Collections.Generic;

namespace PgJsonObjects
{
    public class OrAbilityRequirement : AbilityRequirement
    {
        public OrAbilityRequirement(List<AbilityRequirement> OrList, string RawErrorMsg)
        {
            this.OrList = OrList;
            ErrorMsg = RawErrorMsg;
        }

        public List<AbilityRequirement> OrList { get; private set; }
        public string ErrorMsg { get; private set; }
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Or) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OrList } },
            { "ErrorMsg", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ErrorMsg } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (AbilityRequirement Item in OrList)
                    AddWithFieldSeparator(ref Result, Item.TextContent);

                return Result;
            }
        }
        #endregion
    }
}
