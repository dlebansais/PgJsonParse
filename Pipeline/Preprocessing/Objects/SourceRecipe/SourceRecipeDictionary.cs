namespace Preprocessor;

using System.Collections.Generic;

public class SourceRecipeDictionary : Dictionary<int, SourceRecipe>, IDictionaryValueBuilderInt<SourceRecipe, RawSourceRecipe>
{
    public SourceRecipe FromRaw(int key, RawSourceRecipe rawSourceRecipe) => new(key, rawSourceRecipe);
    public RawSourceRecipe ToRaw(SourceRecipe sourceRecipe) => sourceRecipe.ToRawSourceRecipe();
}
