namespace PgJsonObjects
{
    public class IsVolunteerGuideAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "IsVolunteerGuide");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Volunteer Guide");

                return Result;
            }
        }
        #endregion
    }
}
