namespace Preprocessor;

using System.Collections.Generic;

internal class NpcDictionary : Dictionary<string, Npc>, IDictionaryValueBuilder<Npc, Npc>
{
    public Npc FromRaw(Npc npc) => npc;
    public Npc ToRaw(Npc npc) => npc;
}
