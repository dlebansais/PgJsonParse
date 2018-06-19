namespace PgJsonObjects
{
    interface IPgQuestObjectiveDeliver
    {
        IPgGameNpc DeliverNpc { get; }
        IPgItem QuestItem { get; }
        int NumToDeliver { get; }
        int? RawNumToDeliver { get; }
    }
}
