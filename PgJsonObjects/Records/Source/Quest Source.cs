namespace PgJsonObjects
{
    public class QuestSource : GenericSource
    {
        #region Init
        public QuestSource(Quest Quest)
        {
            this.Quest = Quest;
        }
        #endregion

        #region Properties
        public Quest Quest { get; private set; }
        #endregion
    }
}
