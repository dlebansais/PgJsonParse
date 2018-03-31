namespace PgJsonObjects
{
    public class InGraveyardAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "InGraveyard");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "In Graveyard");

                return Result;
            }
        }
        #endregion
    }
}
