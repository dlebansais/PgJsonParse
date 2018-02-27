namespace PgJsonObjects
{
    public class InteractionFlagSetAbilityRequirement : AbilityRequirement
    {
        public InteractionFlagSetAbilityRequirement(string RawInteractionFlag)
        {
            InteractionFlag = RawInteractionFlag;
        }

        public string InteractionFlag { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "InteractionFlagSet");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, InteractionFlag);

                return Result;
            }
        }
        #endregion
    }
}
