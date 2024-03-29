﻿namespace Preprocessor;

using System.Collections.Generic;

public class RecipeDictionary : Dictionary<int, Recipe>, IDictionaryValueBuilder<Recipe, RawRecipe>
{
    public Recipe FromRaw(RawRecipe rawRecipe) => new(rawRecipe);
    public RawRecipe ToRaw(Recipe recipe) => recipe.ToRawRecipe();
}
