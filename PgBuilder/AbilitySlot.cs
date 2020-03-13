﻿namespace PgBuilder
{
    using PgJsonObjects;
    using Presentation;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class AbilitySlot : INotifyPropertyChanged
    {
        #region Init
        static AbilitySlot()
        {
            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
            string[] Names = CurrentAssembly.GetManifestResourceNames();

            using Stream ResourceStream = CurrentAssembly.GetManifestResourceStream("PgBuilder.Resources.default.png");
            DefaultAbilityImageSource = ImageConversion.IconStreamToImageSource(ResourceStream);
        }

        public AbilitySlot()
        {
            AbilityTierList = null;
            Ability = null;
            AbilityName = null;
            Source = DefaultAbilityImageSource;
        }
        #endregion

        #region Properties
        public static ImageSource DefaultAbilityImageSource { get; }
        public AbilityTierList AbilityTierList { get; private set; }
        public IPgAbility Ability { get; private set; }
        public bool IsEmpty { get { return Ability == null; } }
        public string AbilityName { get; private set; }
        public ImageSource Source { get; private set; }
        public string AbilityDescription { get {return Ability?.Description; } }
        public string AbilityMinLevel { get { return Ability != null ? Ability.Level.ToString() : string.Empty; } }
        public bool? AbilityMinLevelModified { get { return null; } }
        public string AbilityPowerCost { get { return Ability != null ? Ability.PvE.PowerCost.ToString() : string.Empty; } }
        public bool? AbilityPowerCostModified { get { return null; } }
        public string AbilityReuseTime { get { return Ability != null ? App.DoubleToString(Ability.ResetTime) : string.Empty; } }
        public bool? AbilityReuseTimeModified { get { return null; } }
        public string AbilityRange { get { return Ability != null ? Ability.PvE.Range.ToString() : string.Empty; } }
        public bool? AbilityRangeModified { get { return null; } }
        public string AbilityDamage { get { return Ability != null ? Ability.PvE.Damage.ToString() : string.Empty; } }
        public bool? AbilityDamageModified { get { return null; } }
        public string AbilityDamageType { get { return Ability != null ? Ability.DamageType.ToString() : string.Empty; } }
        public bool? AbilityDamageTypeModified { get { return null; } }
        public bool HasAbilityDamageVulnerable { get { return Ability != null && Ability.PvE.RawExtraDamageIfTargetVulnerable.HasValue; } }
        public string AbilityDamageVulnerable { get { return Ability != null ? Ability.PvE.ExtraDamageIfTargetVulnerable.ToString() : string.Empty; } }
        public bool? AbilityDamageVulnerableModified { get { return null; } }
        public string AbilityReduceRage { get { return Ability != null ? Ability.PvE.RageBoost.ToString() : string.Empty; } }
        public bool? AbilityReduceRageModified { get { return null; } }
        public string AbilityEnrageTarget { get { return Ability != null ? ((int)(Ability.PvE.RageMultiplier * 100)).ToString() : string.Empty; } }
        public bool? AbilityEnrageTargetModified { get { return null; } }
        public string AbilityAccuracy { get { return Ability != null ? App.DoubleToString(Ability.PvE.Accuracy) : string.Empty; } }
        public bool? AbilityAccuracyModified { get { return null; } }
        public bool IsEpic { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.EpicAttack); } }
        #endregion

        #region Client Interface
        public void SetAbility(AbilityTierList abilityTierList, string iconFolder)
        {
            AbilityTierList = abilityTierList;
            Ability = abilityTierList.Source;

            UpdateName();
            UpdateSource(iconFolder);
        }

        public void SetAbility(AbilityTierList abilityTierList, IPgAbility ability, string iconFolder)
        {
            Debug.Assert(abilityTierList.Contains(ability));

            AbilityTierList = abilityTierList;
            Ability = ability;

            UpdateName();
            UpdateSource(iconFolder);
        }

        public void Reset()
        {
            AbilityTierList = null;
            Ability = null;

            ResetName();
            ResetSource();
        }

        public static string CuteDigitStrippedName(IPgAbility ability)
        {
            string DigitStrippedName = ability.DigitStrippedName;
            int Index;

            Index = 0;
            while (Index < DigitStrippedName.Length)
                if (char.IsDigit(DigitStrippedName[Index]))
                    DigitStrippedName = DigitStrippedName.Substring(0, Index) + DigitStrippedName.Substring(Index + 1);
                else
                    Index++;

            if (IdenticalNameTable.ContainsKey(DigitStrippedName))
                DigitStrippedName = IdenticalNameTable[DigitStrippedName];

            Index = 0;
            while (Index < DigitStrippedName.Length)
            {
                if (char.IsUpper(DigitStrippedName[Index]) && Index > 0)
                {
                    DigitStrippedName = DigitStrippedName.Substring(0, Index) + " " + DigitStrippedName.Substring(Index);
                    Index++;
                }

                Index++;
            }

            return DigitStrippedName;
        }

        private static readonly Dictionary<string, string> IdenticalNameTable = new Dictionary<string, string>()
        {
            { "StabledPetLiving", "StabledPet" },
            { "TameRat", "TameRat" },
            { "TameCat", "TameRat" },
            { "TameBear", "TameRat" },
            { "TameBee", "TameRat" },
            { "BasicShotB", "BasicShot" },
            { "AimedShotB", "AimedShot" },
            { "BlitzShotB", "BlitzShot" },
            { "ToxinBombB", "MycotoxinFormula" },
            { "ToxinBombC", "AcidBomb" },
            { "FireWallB", "FireWall" },
            { "IceVeinsB", "IceVeins" },
            { "SliceB", "DuelistsSlash" },
            { "WerewolfPounceB", "PouncingRend" },
            { "WerewolfPounceBB", "PouncingRend" },
        };
        #endregion

        #region Implementation
        private void UpdateName()
        {
            AbilityName = Ability.Name;
            NotifyPropertyChanged(nameof(AbilityName));
        }

        private void ResetName()
        {
            AbilityName = null;
            NotifyPropertyChanged(nameof(AbilityName));
        }

        private void UpdateSource(string iconFolder)
        {
            string IconFile = Path.Combine(iconFolder, $"icon_{Ability.IconId}.png");
            Source = ImageConversion.IconFileToImageSource(IconFile);
            NotifyPropertyChanged(nameof(Source));
        }

        private void ResetSource()
        {
            Source = DefaultAbilityImageSource;
            NotifyPropertyChanged(nameof(Source));
        }
        #endregion

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
