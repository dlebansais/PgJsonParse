﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveGuildGiveItem : PgQuestObjective
    {
        public PgItem QuestItem { get; set; }
        public PgNpcLocation DeliverNpc { get; set; }
        public ItemKeyword ItemKeyword { get; set; }
        public PgItemCollection ItemList { get; } = new PgItemCollection();
        public string DeliverNpcId { get; set; } = string.Empty;
        public string DeliverNpcName { get; set; } = string.Empty;
        public MapAreaName DeliverNpcArea { get; set; }
    }
}
