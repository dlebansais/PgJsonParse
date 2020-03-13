namespace PgBuilder
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PgJsonObjects;

    public class GearSlot : INotifyPropertyChanged
    {
        public GearSlot(string name, ItemSlot slot)
        {
            Name = name;
            Slot = slot;
            ItemList = new List<IPgItem>();
            SelectedItem = -1;

            IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
            IList<IPgItem> VerifiedObjectList = (IList<IPgItem>)ItemDefinition.VerifiedObjectList;
            foreach (IPgItem Item in VerifiedObjectList)
            {
                if (Item.EquipSlot == slot)
                    ItemList.Add(Item);
            }
        }

        public string Name { get; }
        public ItemSlot Slot { get; }
        public List<IPgItem> ItemList { get; }
        public int SelectedItem { get; private set; }
        public ObservableCollection<Mod> ModList { get; } = new ObservableCollection<Mod>();
        public List<Power> AvailablePowerList { get; } = new List<Power>();

        public void Reset(IPgSkill skill1, IPgSkill skill2)
        {
            ModList.Clear();
            AvailablePowerList.Clear();

            if (skill1 != null)
                UpdateModList(skill1);
            if (skill2 != null)
                UpdateModList(skill2);
        }

        public void UpdateModList(IPgSkill skill)
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;

            UpdateModList(skill, PowerList);
            if (skill.ParentSkill != null)
                UpdateModList(skill.ParentSkill, PowerList);
        }

        public void UpdateModList(IPgSkill skill, IList<IPgPower> powerList)
        {
            foreach (IPgPower PowerItem in powerList)
            {
                if (PowerItem.Skill != skill)
                    continue;

                bool IsSlotCompatible = false;
                foreach (ItemSlot SlotItem in PowerItem.SlotList)
                    if (SlotItem == Slot)
                    {
                        IsSlotCompatible = true;
                        break;
                    }

                if (!IsSlotCompatible)
                    continue;

                AvailablePowerList.Add(new Power(PowerItem));
            }
        }

        public void SetSelectedItem(int index)
        {
            if (index >= 0)
                SelectedItem = index;

            NotifyPropertyChanged(nameof(SelectedItem));
        }

        public void SetSelectedItem(string key)
        {
            foreach (IPgItem Item in ItemList)
                if (Item.Key == key)
                {
                    SelectedItem = ItemList.IndexOf(Item);
                    NotifyPropertyChanged(nameof(SelectedItem));
                }
        }

        public void AddMod()
        {
            ModList.Add(new Mod(AvailablePowerList));
        }

        public void ResetMods()
        {
            ModList.Clear();
        }

        public void AddMod(string key, int tier)
        {
            Mod NewMod = new Mod(AvailablePowerList, key, tier);

            if (NewMod.SelectedPower >= 0)
                ModList.Add(NewMod);
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
