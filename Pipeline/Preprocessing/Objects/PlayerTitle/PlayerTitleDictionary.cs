namespace Preprocessor;

using System.Collections.Generic;

public class PlayerTitleDictionary : Dictionary<int, PlayerTitle>, IDictionaryValueBuilderInt<PlayerTitle, RawPlayerTitle>
{
    public PlayerTitle FromRaw(int key, RawPlayerTitle rawPlayerTitle) => new(key, rawPlayerTitle);
    public RawPlayerTitle ToRaw(PlayerTitle playerTitle) => playerTitle.ToRawPlayerTitle();
}
