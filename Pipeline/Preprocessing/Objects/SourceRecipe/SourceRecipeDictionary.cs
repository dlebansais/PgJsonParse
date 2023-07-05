namespace Preprocessor;

using System.Collections.Generic;

public class SourceRecipeDictionary : Dictionary<int, SourceRecipe>, IDictionaryValueBuilder<SourceRecipe, RawSourceRecipe>
{
    public SourceRecipe FromRaw(RawSourceRecipe rawSourceRecipe) => new(rawSourceRecipe);
    public RawSourceRecipe ToRaw(SourceRecipe sourceRecipe) => sourceRecipe.ToRawSourceRecipe();
}
