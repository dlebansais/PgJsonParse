namespace Preprocessor;

using System.Collections.Generic;

internal class NpcDictionary : Dictionary<string, Npc>, IDictionaryValueBuilder<Npc, Npc>
{
    public Npc ToItem(Npc fromRawNpc) => fromRawNpc;
    public Npc ToRawItem(Npc fromRawNpc) => fromRawNpc;
}
