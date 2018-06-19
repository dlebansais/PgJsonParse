namespace PgJsonObjects
{
    public class TrainingSource : GenericSource
    {
        #region Init
        public TrainingSource(string NpcName, IPgGameNpc Npc)
        {
            this.NpcName = NpcName;
            this.Npc = Npc;
        }
        #endregion

        #region Properties
        public string NpcName { get; private set; }
        public IPgGameNpc Npc { get; private set; }
        #endregion
    }
}
