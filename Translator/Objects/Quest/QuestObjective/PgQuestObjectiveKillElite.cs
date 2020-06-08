﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveKillElite : PgQuestObjective
    {
        public string AbilityKeyword { get; set; } = string.Empty;
        public QuestObjectiveKillTarget Target { get; set; }
        public EffectKeyword EffectRequirement { get; set; }
    }
}
