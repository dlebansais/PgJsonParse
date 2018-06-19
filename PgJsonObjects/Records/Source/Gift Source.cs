namespace PgJsonObjects
{
    public class GiftSource : GenericSource
    {
        #region Init
        public GiftSource(string NpcName, IPgGameNpc Npc)
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
