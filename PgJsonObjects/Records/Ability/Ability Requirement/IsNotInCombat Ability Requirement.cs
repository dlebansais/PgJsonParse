namespace PgJsonObjects
{
    public class IsNotInCombatAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "IsNotInCombat");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Not In Combat");

                return Result;
            }
        }
        #endregion
    }
}
