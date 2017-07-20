namespace PgJsonObjects
{
    public class GiftSource : GenericSource
    {
        #region Init
        public GiftSource(string NpcName)
        {
            this.NpcName = NpcName;
        }
        #endregion

        #region Properties
        public string NpcName { get; private set; }
        #endregion
    }
}
