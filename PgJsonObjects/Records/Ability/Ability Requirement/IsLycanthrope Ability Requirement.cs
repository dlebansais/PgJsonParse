namespace PgJsonObjects
{
    public class IsLycanthropeAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "IsLycanthrope");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Lycanthrope");

                return Result;
            }
        }
        #endregion
    }
}
