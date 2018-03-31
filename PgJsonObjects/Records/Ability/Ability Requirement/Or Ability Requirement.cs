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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "Or");
        }
        #endregion

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
