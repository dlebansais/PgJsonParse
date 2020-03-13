namespace PgBuilder
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
