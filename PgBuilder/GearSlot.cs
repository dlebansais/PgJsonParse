namespace PgBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PgJsonObjects;

    public class GearSlot : INotifyPropertyChanged
    {
        #region Init
        public GearSlot(string name, ItemSlot slot, bool canEnhance)
        {
            Name = name;
            Slot = slot;
            CanEnhance = canEnhance;
            ItemList = new List<ItemInfo>();
            SelectedItemIndex = -1;
            ConsumedEnhancementPoints = 0;

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
        public bool CanEnhance { get; }
        public bool HasEnhancement { get { return CanEnhance && IsItemSelected && SelectedItem.Item.KeywordTable.ContainsKey(ItemKeyword.ClothArmor); } }
        public List<ItemInfo> ItemList { get; }
        public int SelectedItemIndex { get; private set; }
        public ItemInfo SelectedItem { get { return IsItemSelected ? ItemList[SelectedItemIndex] : null; } }
        public bool IsItemSelected { get { return SelectedItemIndex >= 0; } }
        public ObservableCollection<Mod> ModList { get; } = new ObservableCollection<Mod>();
        public List<Power> AvailablePowerList { get; } = new List<Power>();
        public int ConsumedEnhancementPoints { get; private set; }
        public List<Enhancement> EnhancementList { get; } = new List<Enhancement>()
        {
            new Enhancement(DamageType.Cold, "MOD_COLD_DIRECT"),
            new Enhancement(DamageType.Darkness, "MOD_DARKNESS_DIRECT"),
            new Enhancement(DamageType.Electricity, "MOD_ELECTRICITY_DIRECT"),
            new Enhancement(DamageType.Fire, "MOD_FIRE_DIRECT"),
            new Enhancement(DamageType.Nature, "MOD_NATURE_DIRECT"),
            new Enhancement(DamageType.Psychic, "MOD_PSYCHIC_DIRECT"),
        };
        public ObservableCollection<IPgItemAttributeLink> EnhancementEffectDescriptionList { get; } = new ObservableCollection<IPgItemAttributeLink>();
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

            UpdateModListWithGenericMods();
        }

        public void UpdateModList(IPgSkill skill)
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;

            if (skill.ParentSkill != null)
                UpdateModListWithSkillMods(skill.ParentSkill, PowerList);
            UpdateModListWithSkillMods(skill, PowerList);
        }

        public void UpdateModListWithSkillMods(IPgSkill skill, IList<IPgPower> powerList)
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

                AvailablePowerList.Add(new Power(PowerItem, AvailablePowerList));
            }
        }

        public void UpdateModListWithGenericMods()
        {
            UpdateModListWithGenericMods(PowerSkill.ShamanicInfusion);
            UpdateModListWithGenericMods(PowerSkill.Endurance);
            UpdateModListWithGenericMods(PowerSkill.ArmorPatching);
            UpdateModListWithGenericMods(PowerSkill.AnySkill);
        }

        public void UpdateModListWithGenericMods(PowerSkill rawSkill)
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;

            foreach (IPgPower PowerItem in PowerList)
            {
                if (PowerItem.RawSkill != rawSkill)
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

                AvailablePowerList.Add(new Power(PowerItem, AvailablePowerList));
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
            NotifyPropertyChanged(nameof(HasEnhancement));
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

        public void UpdateEnhancementList(List<int> values)
        {
            for (int i = 0; i < values.Count && i < EnhancementList.Count; i++)
                EnhancementList[i].SetPointCount(values[i]);

            RecalculateEnhancementPoints();
        }

        public void IncrementEnhancement(int index)
        {
            Debug.Assert(index >= 0 && index < EnhancementList.Count);
            EnhancementList[index].Increment();

            RecalculateEnhancementPoints();
        }

        public void DecrementEnhancement(int index)
        {
            Debug.Assert(index >= 0 && index < EnhancementList.Count);
            EnhancementList[index].Decrement();

            RecalculateEnhancementPoints();
        }

        private void RecalculateEnhancementPoints()
        {
            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];

            ConsumedEnhancementPoints = 0;
            EnhancementEffectDescriptionList.Clear();

            foreach (Enhancement Item in EnhancementList)
            {
                Debug.Assert(AttributeDefinition.ObjectTable.ContainsKey(Item.Key));

                if (Item.PointCount > 0)
                {
                    IPgAttribute Attribute = (IPgAttribute)AttributeDefinition.ObjectTable[Item.Key];
                    ItemAttributeLink Link = new ItemAttributeLink(Attribute.Label, Item.PointCount * 0.02F, FloatFormat.Standard);
                    Link.SetLink(Attribute);
                    EnhancementEffectDescriptionList.Add(Link);

                    ConsumedEnhancementPoints += Item.PointCount;
                }
            }

            ConsumedEnhancementPoints *= 20;
            NotifyPropertyChanged(nameof(ConsumedEnhancementPoints));
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
