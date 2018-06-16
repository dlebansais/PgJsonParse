using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgItemUses
    {
        List<int> RecipesThatUseItemList { get; }
    }
}
