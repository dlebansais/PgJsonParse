namespace PgBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PgJsonObjects;

    public class GearSlot : INotifyPropertyChanged
    {
        #region Init
        public GearSlot(string name, ItemSlot slot)
        {
            Name = name;
            Slot = slot;
            ItemList = new List<ItemInfo>();
            SelectedItemIndex = -1;

            IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
            IList<IPgItem> VerifiedObjectList = (IList<IPgItem>)ItemDefinition.VerifiedObjectList;
            foreach (IPgItem Item in VerifiedObjectList)
            {
                if (Item.EquipSlot == slot || (slot == ItemSlot.OffHand && Item.EquipSlot == ItemSlot.OffHandShield))
                    ItemList.Add(new ItemInfo(Item));
            }

            ItemList.Sort(SortByName);
        }

        private int SortByName(ItemInfo item1, ItemInfo item2)
        {
            return string.Compare(item1.ItemName, item2.ItemName, StringComparison.InvariantCulture);
        }
        #endregion

        #region Properties
        public string Name { get; }
        public ItemSlot Slot { get; }
        public List<ItemInfo> ItemList { get; }
        public int SelectedItemIndex { get; private set; }
        public ItemInfo SelectedItem { get { return IsItemSelected ? ItemList[SelectedItemIndex] : null; } }
        public bool IsItemSelected { get { return SelectedItemIndex >= 0; } }
        public ObservableCollection<Mod> ModList { get; } = new ObservableCollection<Mod>();
        public List<Power> AvailablePowerList { get; } = new List<Power>();
        #endregion

        #region Client Interface
        public void Reset(IPgSkill skill1, IPgSkill skill2)
        {
            ModList.Clear();
            AvailablePowerList.Clear();

            if (skill1 != null)
                UpdateModList(skill1);
            if (skill2 != null)
                UpdateModList(skill2);

            UpdateModListWithGenericMod();
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

        public void UpdateModListWithGenericMod()
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;

            foreach (IPgPower PowerItem in PowerList)
            {
                if (PowerItem.RawSkill != PowerSkill.AnySkill &&
                    PowerItem.RawSkill != PowerSkill.Endurance &&
                    PowerItem.RawSkill != PowerSkill.ArmorPatching &&
                    PowerItem.RawSkill != PowerSkill.ShamanicInfusion)
                    continue;
                if (PowerItem.IsUnavailable)
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
                SelectedItemIndex = index;

            NotifyPropertiesChanged();
        }

        public void SetSelectedItem(string key)
        {
            SelectedItemIndex = -1;

            foreach (ItemInfo Item in ItemList)
                if (Item.ItemKey == key)
                {
                    SelectedItemIndex = ItemList.IndexOf(Item);
                    break;
                }

            NotifyPropertiesChanged();
        }

        public void ResetItem()
        {
            SelectedItemIndex = -1;
            NotifyPropertiesChanged();
        }

        public void NotifyPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(SelectedItemIndex));
            NotifyPropertyChanged(nameof(SelectedItem));
            NotifyPropertyChanged(nameof(IsItemSelected));
        }

        public void AddMod()
        {
            ModList.Add(new Mod(this, AvailablePowerList));
        }

        public void AddMod(string key, int tier)
        {
            Mod NewMod = new Mod(this, AvailablePowerList, key, tier);

            if (NewMod.SelectedPowerIndex >= 0)
                ModList.Add(NewMod);
        }

        public void RemoveMod(Mod mod)
        {
            ModList.Remove(mod);
        }

        public void MoveModPosition(Mod mod, int offset)
        {
            int CurrentPosition = ModList.IndexOf(mod);
            if (CurrentPosition >= 0 && CurrentPosition + offset >= 0 && CurrentPosition + offset < ModList.Count)
                ModList.Move(CurrentPosition, CurrentPosition + offset);
        }

        public void ResetMods()
        {
            ModList.Clear();
        }
        #endregion

        #region Debugging
        public override string ToString()
        {
            return Name;
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
