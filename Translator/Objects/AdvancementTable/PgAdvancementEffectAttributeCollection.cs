namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAdvancementEffectAttributeCollection : List<PgAdvancementEffectAttribute>
    {
        public void SetLevelAndAdvancement(int level, PgAdvancement advancement)
        {
            LevelAdvancementTable.Add(level, advancement);
        }

        public Dictionary<int, PgAdvancement> GetLevelAdvancementTable()
        {
            return LevelAdvancementTable;
        }

        private Dictionary<int, PgAdvancement> LevelAdvancementTable = new();
    }
}
