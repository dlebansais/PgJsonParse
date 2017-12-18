namespace PgJsonObjects
{
    public class IsLongTimeAnimalQuestRequirement : QuestRequirement
    {
        public IsLongTimeAnimalQuestRequirement()
        {
        }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "IsLongtimeAnimal");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion
    }
}
