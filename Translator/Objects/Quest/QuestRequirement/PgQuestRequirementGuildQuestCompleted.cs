﻿namespace PgObjects
{
    public class PgQuestRequirementGuildQuestCompleted : PgQuestRequirement
    {
        public PgQuest Quest { get; set; } = null!;
    }
}
