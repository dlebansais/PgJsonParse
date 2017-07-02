namespace PgJsonObjects
{
    public class InGraveyardAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "InGraveyard");

            Generator.CloseObject();
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
