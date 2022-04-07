namespace PgObjects
{
    public class PgQuestPreGiveEffectItem : PgQuestPreGiveEffect
    {
        public string? Item_Key { get; set; }
        public QuestGroup QuestGroup { get; set; }
    }
}
