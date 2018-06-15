using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseRecipe
    {
        Skill ConnectedSkill { get; }
        List<Recipe> RecipeTargetList { get; }
        List<Item> ResultItemList { get; }
    }
}
