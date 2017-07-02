namespace PgJsonObjects
{
    public class IsHardcoreAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "IsHardcore");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Hardcore");

                return Result;
            }
        }
        #endregion
    }
}
