namespace PgJsonObjects
{
    public interface IPgQuestObjectiveKill
    {
        string AbilityKeyword { get; }
        QuestObjectiveKillTarget Target { get; }
        EffectKeyword EffectRequirement { get; }
    }
}
