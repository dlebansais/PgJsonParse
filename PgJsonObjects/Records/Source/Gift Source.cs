namespace PgJsonObjects
{
    public class GiftSource : GenericSource
    {
        #region Init
        public GiftSource(string NpcName, GameNpc Npc)
        {
            this.NpcName = NpcName;
            this.Npc = Npc;
        }
        #endregion

        #region Properties
        public string NpcName { get; private set; }
        public GameNpc Npc { get; private set; }
        #endregion
    }
}
