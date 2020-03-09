﻿namespace PgBuilder
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public class Mod : INotifyPropertyChanged
    {
        public Mod(List<Power> availablePowerList)
        {
            AvailablePowerList = availablePowerList;
            SelectedPower = -1;
        }

        public List<Power> AvailablePowerList { get; }
        public int SelectedPower { get; private set; }

        public void SetSelectedPower(int index)
        {
            SelectedPower = index;
            NotifyPropertyChanged(nameof(SelectedPower));
        }

        public void IncrementTier()
        {
            if (SelectedPower >= 0 && SelectedPower < AvailablePowerList.Count)
                AvailablePowerList[SelectedPower].IncrementTier();
        }

        public void DecrementTier()
        {
            if (SelectedPower >= 0 && SelectedPower < AvailablePowerList.Count)
                AvailablePowerList[SelectedPower].DecrementTier();
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
