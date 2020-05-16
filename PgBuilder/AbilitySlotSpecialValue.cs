namespace PgBuilder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public class AbilitySlotSpecialValue : INotifyPropertyChanged
    {
        public AbilitySlotSpecialValue(string label, string suffix, float baseValue, bool displayAsPercent, bool skipIfZero, List<string> attributesThatDeltaList, List<string> attributesThatModList, List<string> attributesThatModBaseList)
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
        public float BaseValue { get; }
        public bool DisplayAsPercent { get; }
        public bool SkipIfZero { get; }
        public float ActualValue { get { return (BaseValue * ModBaseValue) + ((BaseValue + DeltaValue) * (1.0F + ModValue)); } }
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
        private float DeltaValue = 0;
        private float ModValue = 0;
        private float ModBaseValue = 0;

        public bool? IntModifier(float value)
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
            ModValue = 0;
            ModBaseValue = 0;
            NotifyPropertiesChanged();
        }

        public void AddDelta(float value)
        {
            DeltaValue += value;
            NotifyPropertiesChanged();
        }

        public void AddMod(float value)
        {
            ModValue += value;
            NotifyPropertiesChanged();
        }

        public void AddModBase(float value)
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
