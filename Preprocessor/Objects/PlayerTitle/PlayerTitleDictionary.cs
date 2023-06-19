namespace Preprocessor;

using System.Collections.Generic;

internal class PlayerTitleDictionary : Dictionary<int, PlayerTitle>, IDictionaryValueBuilder<PlayerTitle, PlayerTitle>
{
    public PlayerTitle FromRaw(PlayerTitle playerTitle) => playerTitle;
    public PlayerTitle ToRaw(PlayerTitle playerTitle) => playerTitle;
}
