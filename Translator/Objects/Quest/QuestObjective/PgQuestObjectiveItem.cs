namespace PgJsonObjects
{
    public class PgQuestObjectiveItem : PgQuestObjective
    {
        public PgItem QuestItem { get; set; }
        public PgItemCollection TargetItemList { get; set; } = new PgItemCollection();
        public ItemKeyword Target { get; set; }
    }
}
