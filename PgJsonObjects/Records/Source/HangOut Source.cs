namespace PgJsonObjects
{
    public class HangOutSource : GenericSource
    {
        #region Init
        public HangOutSource(string NpcName)
        {
            this.NpcName = NpcName;
        }
        #endregion

        #region Properties
        public string NpcName { get; private set; }
        #endregion
    }
}
