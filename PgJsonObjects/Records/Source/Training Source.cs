namespace PgJsonObjects
{
    public class TrainingSource : GenericSource
    {
        #region Init
        public TrainingSource(string NpcName)
        {
            this.NpcName = NpcName;
        }
        #endregion

        #region Properties
        public string NpcName { get; private set; }
        #endregion
    }
}
