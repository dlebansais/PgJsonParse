namespace PgObjects
{
    public class PgSkillAdvancementHint : PgSkillAdvancement
    {
        public string Hint { get; set; } = string.Empty;
        public PgNpcLocationCollection NpcList { get; set; } = new PgNpcLocationCollection();
    }
}
