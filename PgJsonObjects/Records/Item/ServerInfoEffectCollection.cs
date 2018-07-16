using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfoEffectCollection : List<IPgServerInfoEffect>, IPgServerInfoEffectCollection, ISerializableJsonObjectCollection
    {
    }
}
