namespace PgJsonObjects
{
    public class CurHealthAbilityRequirement : AbilityRequirement
    {
        public CurHealthAbilityRequirement(double? RawHealth)
        {
            Health = RawHealth.Value;
        }

        public double Health { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "CurHealth");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "CurHealth");

                return Result;
            }
        }
        #endregion
    }
}
