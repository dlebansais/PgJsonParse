namespace PgJsonObjects
{
    public class ItemSource : GenericSource
    {
        #region Init
        public ItemSource(Item Item)
        {
            this.Item = Item;
        }
        #endregion

        #region Properties
        public Item Item { get; private set; }
        #endregion
    }
}
