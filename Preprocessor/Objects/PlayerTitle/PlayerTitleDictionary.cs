namespace Preprocessor;

using System.Collections.Generic;

internal class PlayerTitleDictionary : Dictionary<int, PlayerTitle>, IDictionaryValueBuilder<PlayerTitle, PlayerTitle>
{
    public PlayerTitle ToItem(PlayerTitle fromRawPlayerTitle) => fromRawPlayerTitle;
    public PlayerTitle ToRawItem(PlayerTitle fromRawPlayerTitle) => fromRawPlayerTitle;
}
