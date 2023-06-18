namespace Preprocessor;

using System.Collections.Generic;

internal class PlayerTitleDictionary : Dictionary<int, PlayerTitle>, IDictionaryValueBuilder<PlayerTitle, PlayerTitle>
{
    public PlayerTitle FromRaw(PlayerTitle fromRawPlayerTitle) => fromRawPlayerTitle;
    public PlayerTitle ToRaw(PlayerTitle fromRawPlayerTitle) => fromRawPlayerTitle;
}
