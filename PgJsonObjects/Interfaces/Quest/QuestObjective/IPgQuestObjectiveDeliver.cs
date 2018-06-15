namespace PgJsonObjects
{
    interface IPgQuestObjectiveDeliver
    {
        GameNpc DeliverNpc { get; }
        Item QuestItem { get; }
        int NumToDeliver { get; }
        int? RawNumToDeliver { get; }
    }
}
