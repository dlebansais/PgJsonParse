namespace Preprocessor;

using System.Collections.Generic;

public class RecipeDictionary : Dictionary<int, Recipe>, IDictionaryValueBuilderInt<Recipe, RawRecipe>
{
    public Recipe FromRaw(int key, RawRecipe rawRecipe) => new(key, rawRecipe);
    public RawRecipe ToRaw(Recipe recipe) => recipe.ToRawRecipe();
}
