namespace PgJsonParse
{
    public class ParserVersion
    {
        #region Init
        public ParserVersion()
        {
            Version = "261";
        }
        #endregion

        #region Properties
        public string Version { get; private set; }
        #endregion

        #region Client Interface
        public void SetTo(string NewVersion)
        {
            if (NewVersion != null && NewVersion.Length > 0)
                Version = NewVersion;
        }
        #endregion
    }
}
