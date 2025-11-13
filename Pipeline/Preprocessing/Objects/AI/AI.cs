namespace Preprocessor;

using System.Linq;
using FreeSql.DataAnnotations;

public class AI : IHasKey<string>
{
    public AI()
    {
        Key = string.Empty;
    }

    public AI(string key, RawAI rawAi)
    {
        Key = key;
        Abilities = rawAi.Abilities is null ? null : rawAi.Abilities.Values.ToArray();
        Comment = rawAi.Comment;
        Description = rawAi.Description;
        FlyOffset = rawAi.FlyOffset;
        Flying = rawAi.Flying;
        MinDelayBetweenAbilities = rawAi.MinDelayBetweenAbilities;
        MobilityType = rawAi.MobilityType;
        ServerDriven = rawAi.ServerDriven;
        Strategy = rawAi.Strategy;
        Swimming = rawAi.Swimming;
        UncontrolledPet = rawAi.UncontrolledPet;
        UseAbilitiesWithoutEnemyTarget = rawAi.UseAbilitiesWithoutEnemyTarget;
    }

    [Column(IsPrimary = true)]
    public string Key { get; set; }

    public AIAbility[]? Abilities { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public int? FlyOffset { get; set; }

    public bool? Flying { get; set; }

    public decimal? MinDelayBetweenAbilities { get; set; }

    public string? MobilityType { get; set; }

    public bool? ServerDriven { get; set; }

    public string? Strategy { get; set; }

    public bool? Swimming { get; set; }

    public bool? UncontrolledPet { get; set; }

    public bool? UseAbilitiesWithoutEnemyTarget { get; set; }

    public RawAI ToRawAI()
    {
        RawAI Result = new();

        if (Abilities is null)
            Result.Abilities = null;
        else
        {
            Result.Abilities = new();
            foreach (AIAbility Item in Abilities)
                Result.Abilities.Add(Item.JsonKey, Item);
        }

        Result.Comment = Comment;
        Result.Description = Description;
        Result.FlyOffset = FlyOffset;
        Result.Flying = Flying;
        Result.MinDelayBetweenAbilities = MinDelayBetweenAbilities;
        Result.MobilityType = MobilityType;
        Result.ServerDriven = ServerDriven;
        Result.Strategy = Strategy;
        Result.Swimming = Swimming;
        Result.UncontrolledPet = UncontrolledPet;
        Result.UseAbilitiesWithoutEnemyTarget = UseAbilitiesWithoutEnemyTarget;

        return Result;
    }
}
