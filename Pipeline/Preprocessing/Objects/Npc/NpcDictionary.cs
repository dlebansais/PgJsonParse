namespace Preprocessor;

using System.Collections.Generic;

public class NpcDictionary : Dictionary<string, Npc>, IDictionaryValueBuilderString<Npc, RawNpc>
{
    public Npc FromRaw(string key, RawNpc rawNpc) => new(key, rawNpc);
    public RawNpc ToRaw(Npc npc) => npc.ToRawNpc();
}
