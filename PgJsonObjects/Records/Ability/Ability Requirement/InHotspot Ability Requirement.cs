namespace PgJsonObjects
{
    public class InHotspotAbilityRequirement : AbilityRequirement
    {
        public InHotspotAbilityRequirement(string RawName)
        {
            Name = RawName;
        }

        public string Name { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "InHotspot");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);

                return Result;
            }
        }
        #endregion
    }
}
