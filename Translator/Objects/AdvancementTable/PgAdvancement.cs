namespace PgObjects
{
    public class PgAdvancement
    {
        public PgAdvancementEffectAttributeCollection EffectAttributeList { get; set; } = new PgAdvancementEffectAttributeCollection();

        public void SetLevel(int level)
        {
            Level = level;
        }

        public int GetLevel()
        {
            return Level;
        }

        private int Level;
    }
}
