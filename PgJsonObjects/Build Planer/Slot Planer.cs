using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PgJsonObjects
{
    public class SlotPlaner : INotifyPropertyChanged
    {
        public SlotPlaner(ItemSlot Slot, Dictionary<ItemSlot, string> IconFileTable)
        {
            this.Slot = Slot;
            IconFileName = IconFileTable[Slot];

            AvailablePowerList1 = new ObservableCollection<PlanerSlotPower>();
            AvailablePowerList2 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList1 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList2 = new ObservableCollection<PlanerSlotPower>();
            SortedGearList = new ObservableCollection<Item>();
            GearWeightList = new ObservableCollection<PlanerSlotGear>();
            _AvailablePowerIndex1 = -1;
            _AvailablePowerIndex2 = -1;
            _SelectedPowerIndex1 = -1;
            _SelectedPowerIndex2 = -1;
            _SelectedGearIndex = -1;
        }

        public ItemSlot Slot { get; private set; }
        public string SlotName { get { return Slot.ToString(); } }
        public string IconFileName { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList1 { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList2 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList1 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList2 { get; private set; }
        public ObservableCollection<Item> SortedGearList { get; private set; }
        public ObservableCollection<PlanerSlotGear> GearWeightList { get; private set; }

        public int AvailablePowerIndex1
        {
            get { return _AvailablePowerIndex1; }
            set
            {
                if (_AvailablePowerIndex1 != value)
                {
                    _AvailablePowerIndex1 = value;
                    NotifyThisPropertyChanged();
                    _AvailablePowerIndex2 = -1;
                    NotifyPropertyChanged("AvailablePowerIndex2");
                }
            }
        }
        private int _AvailablePowerIndex1;

        public int AvailablePowerIndex2
        {
            get { return _AvailablePowerIndex2; }
            set
            {
                if (_AvailablePowerIndex2 != value)
                {
                    _AvailablePowerIndex2 = value;
                    NotifyThisPropertyChanged();
                    _AvailablePowerIndex1 = -1;
                    NotifyPropertyChanged("AvailablePowerIndex1");
                }
            }
        }
        private int _AvailablePowerIndex2;

        public int SelectedPowerIndex1
        {
            get { return _SelectedPowerIndex1; }
            set
            {
                if (_SelectedPowerIndex1 != value)
                {
                    _SelectedPowerIndex1 = value;
                    NotifyThisPropertyChanged();
                    _SelectedPowerIndex2 = -1;
                    NotifyPropertyChanged("SelectedPowerIndex2");
                }
            }
        }
        private int _SelectedPowerIndex1;

        public int SelectedPowerIndex2
        {
            get { return _SelectedPowerIndex2; }
            set
            {
                if (_SelectedPowerIndex2 != value)
                {
                    _SelectedPowerIndex2 = value;
                    NotifyThisPropertyChanged();
                    _SelectedPowerIndex1 = -1;
                    NotifyPropertyChanged("SelectedPowerIndex1");
                }
            }
        }
        private int _SelectedPowerIndex2;

        public int SelectedGearIndex
        {
            get { return _SelectedGearIndex; }
            set
            {
                if (_SelectedGearIndex != value)
                {
                    _SelectedGearIndex = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged("SelectedGearName");
                    NotifyPropertyChanged("SelectedGearDescription");
                    NotifyPropertyChanged("SelectedGearEffectDescriptionList");
                }
            }
        }
        public string SelectedGearName
        {
            get { return _SelectedGearIndex >= 0 ? SortedGearList[_SelectedGearIndex].Name : SlotName; }
        }
        public string SelectedGearDescription
        {
            get { return _SelectedGearIndex >= 0 ? SortedGearList[_SelectedGearIndex].Description : null; }
        }
        public ObservableCollection<ItemEffect> SelectedGearEffectDescriptionList
        {
            get { return _SelectedGearIndex >= 0 ? SortedGearList[_SelectedGearIndex].EffectDescriptionList : null; }
        }
        private int _SelectedGearIndex;

        public void RefreshCombatSkillList(IList<Power> PowerList, Dictionary<string, Attribute> AttributeTable, PowerSkill FirstSkill, int MaxLevelFirstSkill, PowerSkill SecondSkill, int MaxLevelSecondSkill)
        {
            AvailablePowerList1.Clear();
            AvailablePowerList2.Clear();
            SelectedPowerList1.Clear();
            SelectedPowerList2.Clear();
            AvailablePowerIndex1 = -1;
            AvailablePowerIndex2 = -1;
            SelectedPowerIndex1 = -1;
            SelectedPowerIndex2 = -1;

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(FirstSkill, Slot))
                    AvailablePowerList1.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelFirstSkill));

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(SecondSkill, Slot))
                    AvailablePowerList2.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelSecondSkill));
        }

        public void AddPower1()
        {
            int SourceIndex = AvailablePowerIndex1;
            int DestinationIndex = SelectedPowerIndex1;

            MovePower(AvailablePowerList1, ref SourceIndex, SelectedPowerList1, ref DestinationIndex);

            AvailablePowerIndex1 = SourceIndex;
            SelectedPowerIndex1 = DestinationIndex;
        }

        public void AddPower2()
        {
            int SourceIndex = AvailablePowerIndex2;
            int DestinationIndex = SelectedPowerIndex2;

            MovePower(AvailablePowerList2, ref SourceIndex , SelectedPowerList2, ref DestinationIndex);

            AvailablePowerIndex2 = SourceIndex;
            SelectedPowerIndex2 = DestinationIndex;
        }

        public void RemovePower1()
        {
            int SourceIndex = SelectedPowerIndex1;
            int DestinationIndex = AvailablePowerIndex1;

            MovePower(SelectedPowerList1, ref SourceIndex, AvailablePowerList1, ref DestinationIndex);

            SelectedPowerIndex1 = SourceIndex;
            AvailablePowerIndex1 = DestinationIndex;
        }

        public void RemovePower2()
        {
            int SourceIndex = SelectedPowerIndex2;
            int DestinationIndex = AvailablePowerIndex2;

            MovePower(SelectedPowerList2, ref SourceIndex, AvailablePowerList2, ref DestinationIndex);

            SelectedPowerIndex2 = SourceIndex;
            AvailablePowerIndex2 = DestinationIndex;
        }

        public void MovePower(IList<PlanerSlotPower> Source, ref int SourceIndex, IList<PlanerSlotPower> Destination, ref int DestinationIndex)
        {
            if (SourceIndex >= 0 && SourceIndex < Source.Count)
            {
                PlanerSlotPower Power = Source[SourceIndex];

                Source.RemoveAt(SourceIndex);
                if (SourceIndex >= Source.Count)
                    SourceIndex = Source.Count - 1;

                Destination.Add(Power);
                DestinationIndex = Destination.Count - 1;
            }
        }

        public void SelectPower1(PlanerSlotPower PlanerSlot)
        {
            int Index = AvailablePowerList1.IndexOf(PlanerSlot);
            if (Index >= 0)
            {
                PlanerSlotPower Power = AvailablePowerList1[Index];

                AvailablePowerList1.RemoveAt(Index);
                SelectedPowerList1.Add(Power);
            }
        }

        public void SelectPower2(PlanerSlotPower PlanerSlot)
        {
            int Index = AvailablePowerList2.IndexOf(PlanerSlot);
            if (Index >= 0)
            {
                PlanerSlotPower Power = AvailablePowerList2[Index];

                AvailablePowerList2.RemoveAt(Index);
                SelectedPowerList2.Add(Power);
            }
        }

        public void RefreshGearList(ICollection<Item> ItemList, ICollection<Attribute> AttributeList, WeightProfile WeightProfile, bool IgnoreUnobtainable, bool IgnoreNoAttribute)
        {
            SortedGearList.Clear();
            GearWeightList.Clear();

            foreach (Item Item in ItemList)
            {
                if (Item.EquipSlot != Slot && (Slot != ItemSlot.OffHand || Item.EquipSlot != ItemSlot.OffHandShield))
                    continue;

                if (IgnoreUnobtainable)
                {
                    if (Item.KeywordTable.ContainsKey(ItemKeyword.Lint_NotObtainable))
                        continue;
                }

                float Weight = Item.GetWeight(WeightProfile);

                if (IgnoreNoAttribute && Weight <= 0)
                    continue;

                int i;
                for (i = 0; i < SortedGearList.Count; i++)
                    if (SortedGearList[i].GetWeight(WeightProfile) <= Weight)
                        break;

                SortedGearList.Insert(i, Item);

                foreach (ItemEffect Effect in Item.EffectDescriptionList)
                {
                    bool Found = false;

                    foreach (PlanerSlotGear GearEffect in GearWeightList)
                        if (GearEffect.Effect.Equals(Effect))
                        {
                            Found = true;
                            break;
                        }

                    if (!Found)
                        GearWeightList.Add(new PlanerSlotGear(Effect, 0));
                }
            }
        }

        public void SelectGear(Item SlotItem)
        {
            for (int i = 0; i < SortedGearList.Count; i++)
                if (SortedGearList[i] == SlotItem)
                {
                    SelectedGearIndex = i;
                    break;
                }
        }

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        ///     Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
