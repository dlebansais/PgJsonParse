namespace PgJsonObjects
{
    public class IsLongtimeAnimalAbilityRequirement : AbilityRequirement
    {
        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "IsLongtimeAnimal");
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Long Time Animal");

                return Result;
            }
        }
        #endregion
    }
}
