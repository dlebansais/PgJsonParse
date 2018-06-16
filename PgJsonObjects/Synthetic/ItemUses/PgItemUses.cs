using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemUses : GenericPgObject, IPgItemUses
    {
        public PgItemUses(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<int> RecipesThatUseItemList { get { return GetIntList(0, ref _RecipesThatUseItemList); } } private List<int> _RecipesThatUseItemList;
    }
}
