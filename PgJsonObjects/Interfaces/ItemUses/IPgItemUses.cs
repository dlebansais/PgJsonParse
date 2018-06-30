using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgItemUses : IJsonKey, IObjectContentGenerator
    {
        List<int> RecipesThatUseItemList { get; }
    }
}
