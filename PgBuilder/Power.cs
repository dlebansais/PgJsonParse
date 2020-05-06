using System.Collections.Generic;

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
            IList<IPgPowerTier> TierEffectList = Source.TierEffectList;
            SelectedTier = TierEffectList.Count / 2;
        }

        public IPgPower Source { get; }
        public int SelectedTier { get; private set; }

        public string Name
        {
            get
            {
                IList<IPgPowerTier> TierEffectList = Source.TierEffectList;
                if (SelectedTier >= 0 && SelectedTier < TierEffectList.Count)
                {
                    string Result = string.Empty;
                    IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];

                    foreach (IPgPowerEffect Effect in TierEffectList[SelectedTier].EffectList)
                    {
                        PgPower.FillCombinedTierEffect(AttributeDefinition.ObjectTable, Effect, out string TierString);

                        if (TierString.Length > 0)
                        {
                            if (Result.Length > 0)
                                Result += ", ";

                            Result += TierString;
                        }
                    }

                    if (Result.Length > 0)
                        Result = $"Tier {SelectedTier}: {Result}";

                    return Result;
                }
                else
                    return "<Unknown>";
            }
        }

        public IPgPowerTier Tier
        {
            get
            {
                IList<IPgPowerTier> TierEffectList = Source.TierEffectList;
                if (SelectedTier >= 0 && SelectedTier < TierEffectList.Count)
                    return TierEffectList[SelectedTier];
                else
                    return null;
            }
        }

        public void DecrementTier()
        {
            SetTier(SelectedTier - 1);
        }

        public void IncrementTier()
        {
            SetTier(SelectedTier + 1);
        }

        public void SetTier(int tier)
        {
            IList<IPgPowerTier> TierEffectList = Source.TierEffectList;
            if (tier < 0)
                tier = 0;
            if (tier >= TierEffectList.Count)
                tier = TierEffectList.Count - 1;

            if (SelectedTier != tier)
            {
                SelectedTier = tier;
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
