﻿namespace PgBuilder
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PgJsonObjects;

    public class ItemInfo : INotifyPropertyChanged
    {
        public ItemInfo(IPgItem item)
        {
            Item = item;
        }

        public IPgItem Item { get; }
        public string ItemKey { get { return Item.Key; } }
        public string ItemName { get { return Item.Name; } }
        public string ItemDescription { get { return Item.Description; } }
        public IPgItemEffectCollection ItemEffectDescriptionList { get { return Item.EffectDescriptionList; } }

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
