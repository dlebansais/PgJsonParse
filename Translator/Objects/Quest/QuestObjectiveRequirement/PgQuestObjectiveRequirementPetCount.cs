namespace PgObjects
{
    public class PgQuestObjectiveRequirementPetCount : PgQuestObjectiveRequirement
    {
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public RecipeKeyword PetTypeTag { get; set; }
    }
}
