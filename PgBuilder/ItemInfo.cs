namespace PgBuilder
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PgJsonObjects;

    public class ItemInfo : INotifyPropertyChanged
    {
        public static ItemInfo NoItem = new ItemInfo();
        private static IPgItemEffectCollection EmptyEffectList = new PgItemEffectCollection();

        private ItemInfo()
        {
            Item = null;
        }

        public ItemInfo(IPgItem item)
        {
            Item = item;
        }

        public IPgItem Item { get; }
        public string ItemKey { get { return Item?.Key; } }
        public string ItemName { get { return Item?.Name; } }
        public string ItemDescription { get { return Item?.Description; } }
        public IPgItemEffectCollection ItemEffectDescriptionList { get { return Item != null ? Item.EffectDescriptionList : EmptyEffectList; } }

        public override string ToString()
        {
            return Item?.Name;
        }

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        /// Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
