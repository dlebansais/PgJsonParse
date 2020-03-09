namespace PgBuilder
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PgJsonObjects;

    public class Power : INotifyPropertyChanged
    {
        public Power(IPgPower source)
        {
            Source = source;
            SelectedTier = 0;
        }

        public IPgPower Source { get; }
        public int SelectedTier { get; private set; }

        public string Name
        {
            get
            {
                if (SelectedTier >= 0 && SelectedTier < Source.CombinedTierList.Count)
                    return Source.CombinedTierList[SelectedTier];
                else
                    return "<Unknown>";
            }
        }

        public void DecrementTier()
        {
            ChangeTier(-1);
        }

        public void IncrementTier()
        {
            ChangeTier(+1);
        }

        private void ChangeTier(int offset)
        {
            if (SelectedTier + offset >= 0 && SelectedTier + offset < Source.CombinedTierList.Count)
            {
                SelectedTier += offset;
                NotifyPropertyChanged(nameof(SelectedTier));
                NotifyPropertyChanged(nameof(Name));
            }
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
