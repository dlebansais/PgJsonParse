namespace Preprocessor;

using System.Collections.Generic;

public class PlayerTitleDictionary : Dictionary<int, PlayerTitle>, IDictionaryValueBuilder<PlayerTitle, RawPlayerTitle>
{
    public PlayerTitle FromRaw(RawPlayerTitle rawPlayerTitle) => new(rawPlayerTitle);
    public RawPlayerTitle ToRaw(PlayerTitle playerTitle) => playerTitle.ToRawPlayerTitle();
}
