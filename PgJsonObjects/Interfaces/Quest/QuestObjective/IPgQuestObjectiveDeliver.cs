namespace PgJsonObjects
{
    interface IPgQuestObjectiveDeliver
    {
        IPgGameNpc DeliverNpc { get; }
        IPgItem QuestItem { get; }
        int NumToDeliver { get; }
        int? RawNumToDeliver { get; }
        string DeliverNpcId { get; }
        string DeliverNpcName { get; }
        MapAreaName DeliverNpcArea { get; }
    }
}
