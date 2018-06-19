namespace PgJsonObjects
{
    public class QuestSource : GenericSource
    {
        #region Init
        public QuestSource(IPgQuest Quest)
        {
            this.Quest = Quest;
        }
        #endregion

        #region Properties
        public IPgQuest Quest { get; private set; }
        #endregion
    }
}
