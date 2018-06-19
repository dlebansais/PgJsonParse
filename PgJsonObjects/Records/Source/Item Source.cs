namespace PgJsonObjects
{
    public class ItemSource : GenericSource
    {
        #region Init
        public ItemSource(IPgItem Item)
        {
            this.Item = Item;
        }
        #endregion

        #region Properties
        public IPgItem Item { get; private set; }
        #endregion
    }
}
