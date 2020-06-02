namespace Translator
{
    using System;
    using System.Collections.Generic;

    public class ItemFieldTable
    {
        public ItemFieldTable(Dictionary<string, Type> table)
        {
            Table = table;
        }

        public Dictionary<string, Type> Table { get; }
    }
}
