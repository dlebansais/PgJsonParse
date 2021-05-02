namespace PgObjects
{
    public class PgQuestPreGiveEffectItem : PgQuestPreGiveEffect
    {
        public PgItem Item { get; set; } = null!;
        public QuestGroup QuestGroup { get; set; }
    }
}
