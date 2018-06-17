using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemUses : MainPgObject<PgItemUses>, IPgItemUses
    {
        public PgItemUses(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgItemUses CreateItem(byte[] data, int offset)
        {
            return new PgItemUses(data, offset);
        }

        public List<int> RecipesThatUseItemList { get { return GetIntList(0, ref _RecipesThatUseItemList); } } private List<int> _RecipesThatUseItemList;
    }
}
