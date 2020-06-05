namespace PgJsonObjects
{
    public class PgQuestObjectiveItem : PgQuestObjective
    {
        public PgItem QuestItem { get; set; }
        public PgItemCollection TargetItemList { get; } = new PgItemCollection();
        public ItemKeyword Target { get; set; }
    }
}
