namespace Preprocessor;

using System.Collections.Generic;

internal class NpcDictionary : Dictionary<string, Npc>, IDictionaryValueBuilder<Npc, Npc>
{
    public Npc FromRaw(Npc fromRawNpc) => fromRawNpc;
    public Npc ToRaw(Npc fromRawNpc) => fromRawNpc;
}
