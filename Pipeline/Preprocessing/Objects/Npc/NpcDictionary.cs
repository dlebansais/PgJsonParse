namespace Preprocessor;

using System.Collections.Generic;

public class NpcDictionary : Dictionary<string, Npc>, IDictionaryValueBuilder<Npc, RawNpc>
{
    public Npc FromRaw(RawNpc rawNpc) => new(rawNpc);
    public RawNpc ToRaw(Npc npc) => npc.ToRawNpc();
}
