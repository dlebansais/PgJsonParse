﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAttributeCollection : IList<IPgAttribute>, IPgCollection
    {
        List<string> ToKeyList { get; }
    }
}