namespace PgJsonObjects
{
    public class XpTableLevel
    {
        public XpTableLevel(int Level, int Xp, int TotalXp)
        {
            this.Level = Level;
            this.Xp = Xp;
            this.TotalXp = TotalXp;
        }

        public int Level { get; private set; }
        public int Xp { get; private set; }
        public int TotalXp { get; private set; }
    }
}
