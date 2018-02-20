namespace PgJsonObjects
{
    public class InteractionFlagSetQuestRequirement : QuestRequirement
    {
        public InteractionFlagSetQuestRequirement(string InteractionFlag)
        {
            this.InteractionFlag = InteractionFlag;
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

                if (InteractionFlag != null)
                    Result = InteractionFlag;

                return Result;
            }
        }
        #endregion
    }
}
