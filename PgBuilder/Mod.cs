using PgJsonObjects;

namespace PgBuilder
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public class Mod : INotifyPropertyChanged
    {
        public Mod(GearSlot parentSlot, List<Power> availablePowerList)
        {
            ParentSlot = parentSlot;
            AvailablePowerList = availablePowerList;
            SelectedPowerIndex = -1;
        }

        public Mod(GearSlot parentSlot, List<Power> availablePowerList, string key, int tier)
        {
            ParentSlot = parentSlot;
            AvailablePowerList = availablePowerList;

            foreach (Power Item in availablePowerList)
                if (Item.Source.Key == key)
                {
                    SelectedPowerIndex = availablePowerList.IndexOf(Item);
                    Item.SetTier(tier);
                    break;
                }
        }

        public GearSlot ParentSlot { get; }
        public List<Power> AvailablePowerList { get; }
        public int SelectedPowerIndex { get; private set; }
        public Power SelectedPower { get { return (SelectedPowerIndex >= 0 && SelectedPowerIndex < AvailablePowerList.Count) ? AvailablePowerList[SelectedPowerIndex] : null; } }
        public IPgPowerTier SelectedTier { get { return (SelectedPowerIndex >= 0 && SelectedPowerIndex < AvailablePowerList.Count) ? AvailablePowerList[SelectedPowerIndex].Tier : null; } }

        public void SetSelectedPower(int index)
        {
            SelectedPowerIndex = index;
            NotifyPropertyChanged(nameof(SelectedPowerIndex));
        }

        public void IncrementTier()
        {
            if (SelectedPowerIndex >= 0 && SelectedPowerIndex < AvailablePowerList.Count)
                AvailablePowerList[SelectedPowerIndex].IncrementTier();
        }

        public void DecrementTier()
        {
            if (SelectedPowerIndex >= 0 && SelectedPowerIndex < AvailablePowerList.Count)
                AvailablePowerList[SelectedPowerIndex].DecrementTier();
        }

        public void MoveDown()
        {
            MovePosition(+1);
        }

        public void MoveUp()
        {
            MovePosition(-1);
        }

        private void MovePosition(int offset)
        {
            ParentSlot.MoveModPosition(this, offset);
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
