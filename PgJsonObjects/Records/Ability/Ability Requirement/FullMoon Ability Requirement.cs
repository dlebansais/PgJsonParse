namespace PgJsonObjects
{
    public class FullMoonAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "FullMoon");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Full Moon");

                return Result;
            }
        }
        #endregion
    }
}
