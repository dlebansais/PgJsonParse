namespace PgBuilder
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public class AbilitySlotSpecialValue : INotifyPropertyChanged
    {
        public AbilitySlotSpecialValue(string label, string suffix, double baseValue, bool displayAsPercent, bool skipIfZero, List<string> attributesThatDeltaList, List<string> attributesThatModList, List<string> attributesThatModBaseList)
        {
            Label = label;
            Suffix = suffix;
            DisplayAsPercent = displayAsPercent;
            BaseValue = DisplayAsPercent ? baseValue * 100 : baseValue;
            SkipIfZero = skipIfZero;
            AttributesThatDeltaList = attributesThatDeltaList;
            AttributesThatModList = attributesThatModList;
            AttributesThatModBaseList = attributesThatModBaseList;
        }

        public string Label { get; }
        public string Suffix { get; }
        public double BaseValue { get; }
        public bool DisplayAsPercent { get; }
        public bool SkipIfZero { get; }
        public double ActualValue { get { return (BaseValue * ModBaseValue) + (DeltaValue * ModValue); } }
        public bool? IsModified { get { return IntModifier(ActualValue - BaseValue); } }
        public bool IsDisplayed { get { return !SkipIfZero || ActualValue != 0; } }
        public string AsString 
        { 
            get 
            {
                int Value = (int)ActualValue;

                if (DisplayAsPercent)
                    return $"{Value}%";
                else
                    return Value.ToString();
            }
        }
        public List<string> AttributesThatDeltaList { get; }
        public List<string> AttributesThatModList { get; }
        public List<string> AttributesThatModBaseList { get; }
        private double DeltaValue = 0;
        private double ModValue = 1.0;
        private double ModBaseValue = 1.0;

        public bool? IntModifier(double value)
        {
            if (value > 0)
                return true;
            else if (value < 0)
                return false;
            else
                return null;
        }

        public void Reset()
        {
            DeltaValue = 0;
            ModValue = 1.0;
            ModBaseValue = 1.0;
            NotifyPropertiesChanged();
        }

        public void AddDelta(double value)
        {
            DeltaValue += value;
            NotifyPropertiesChanged();
        }

        public void AddMod(double value)
        {
            ModValue += value;
            NotifyPropertiesChanged();
        }

        public void AddModBase(double value)
        {
            ModBaseValue += value;
            NotifyPropertiesChanged();
        }

        public void NotifyPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(ActualValue));
            NotifyPropertyChanged(nameof(IsModified));
            NotifyPropertyChanged(nameof(IsDisplayed));
            NotifyPropertyChanged(nameof(AsString));
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
