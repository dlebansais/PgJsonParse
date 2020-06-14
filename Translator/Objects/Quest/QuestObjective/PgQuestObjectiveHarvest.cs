﻿namespace PgObjects
{
    public class PgQuestObjectiveHarvest : PgQuestObjective
    {
        public ItemKeyword ItemTarget { get; set; }
        public PgItem QuestItem { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
