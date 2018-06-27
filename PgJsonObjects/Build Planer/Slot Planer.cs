using Presentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PgJsonObjects
{
    public class SlotPlaner : INotifyPropertyChanged
    {
        public delegate int GetSelectedSkillHandler();

        public SlotPlaner(RootControl owner, ItemSlot Slot, Dictionary<ItemSlot, string> IconFileTable, GetSelectedSkillHandler getFirstSkillHandler, GetSelectedSkillHandler getSecondSkillHandler)
        {
            Owner = owner;
            this.Slot = Slot;
            IconFileName = IconFileTable[Slot];
            GetFirstSkillHandler = getFirstSkillHandler;
            GetSecondSkillHandler = getSecondSkillHandler;

            AvailablePowerList1 = new ObservableCollection<PlanerSlotPower>();
            AvailablePowerList2 = new ObservableCollection<PlanerSlotPower>();
            AvailablePowerList3 = new ObservableCollection<PlanerSlotPower>();
            AvailablePowerList4 = new ObservableCollection<PlanerSlotPower>();
            AvailablePowerList5 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList1 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList2 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList3 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList4 = new ObservableCollection<PlanerSlotPower>();
            SelectedPowerList5 = new ObservableCollection<PlanerSlotPower>();
            SortedGearList = new ObservableCollection<Item>();
            GearWeightList = new ObservableCollection<PlanerSlotGear>();
            _AvailablePowerIndex1 = -1;
            _AvailablePowerIndex2 = -1;
            _AvailablePowerIndex3 = -1;
            _AvailablePowerIndex4 = -1;
            _AvailablePowerIndex5 = -1;
            _SelectedPowerIndex1 = -1;
            _SelectedPowerIndex2 = -1;
            _SelectedPowerIndex3 = -1;
            _SelectedPowerIndex4 = -1;
            _SelectedPowerIndex5 = -1;
            _SelectedGearIndex = -1;
        }

        public RootControl Owner { get; private set; }
        public ItemSlot Slot { get; private set; }
        public string SlotName { get { return Slot.ToString(); } }
        public string IconFileName { get; private set; }
        public GetSelectedSkillHandler GetFirstSkillHandler { get; private set; }
        public GetSelectedSkillHandler GetSecondSkillHandler { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList1 { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList2 { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList3 { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList4 { get; private set; }
        public ObservableCollection<PlanerSlotPower> AvailablePowerList5 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList1 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList2 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList3 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList4 { get; private set; }
        public ObservableCollection<PlanerSlotPower> SelectedPowerList5 { get; private set; }
        public ObservableCollection<Item> SortedGearList { get; private set; }
        public ObservableCollection<PlanerSlotGear> GearWeightList { get; private set; }

        public bool IsFirstSkillSelected { get { return GetFirstSkillHandler() >= 0; } }
        public bool IsSecondSkillSelected { get { return GetSecondSkillHandler() >= 0; } }

        public int AvailablePowerIndex1
        {
            get { return _AvailablePowerIndex1; }
            set
            {
                if (_AvailablePowerIndex1 != value)
                {
                    _AvailablePowerIndex1 = value;
                    NotifyThisPropertyChanged();
                    ResetAvailablePowerIndex(1);
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
                    ResetAvailablePowerIndex(2);
                }
            }
        }
        private int _AvailablePowerIndex2;

        public int AvailablePowerIndex3
        {
            get { return _AvailablePowerIndex3; }
            set
            {
                if (_AvailablePowerIndex3 != value)
                {
                    _AvailablePowerIndex3 = value;
                    NotifyThisPropertyChanged();
                    ResetAvailablePowerIndex(3);
                }
            }
        }
        private int _AvailablePowerIndex3;

        public int AvailablePowerIndex4
        {
            get { return _AvailablePowerIndex4; }
            set
            {
                if (_AvailablePowerIndex4 != value)
                {
                    _AvailablePowerIndex4 = value;
                    NotifyThisPropertyChanged();
                    ResetAvailablePowerIndex(4);
                }
            }
        }
        private int _AvailablePowerIndex4;

        public int AvailablePowerIndex5
        {
            get { return _AvailablePowerIndex5; }
            set
            {
                if (_AvailablePowerIndex5 != value)
                {
                    _AvailablePowerIndex5 = value;
                    NotifyThisPropertyChanged();
                    ResetAvailablePowerIndex(5);
                }
            }
        }
        private int _AvailablePowerIndex5;

        public int SelectedPowerIndex1
        {
            get { return _SelectedPowerIndex1; }
            set
            {
                if (_SelectedPowerIndex1 != value)
                {
                    _SelectedPowerIndex1 = value;
                    NotifyThisPropertyChanged();
                    ResetSelectedPowerIndex(1);
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
                    ResetSelectedPowerIndex(2);
                }
            }
        }
        private int _SelectedPowerIndex2;

        public int SelectedPowerIndex3
        {
            get { return _SelectedPowerIndex3; }
            set
            {
                if (_SelectedPowerIndex3 != value)
                {
                    _SelectedPowerIndex3 = value;
                    NotifyThisPropertyChanged();
                    ResetSelectedPowerIndex(3);
                }
            }
        }
        private int _SelectedPowerIndex3;

        public int SelectedPowerIndex4
        {
            get { return _SelectedPowerIndex4; }
            set
            {
                if (_SelectedPowerIndex4 != value)
                {
                    _SelectedPowerIndex4 = value;
                    NotifyThisPropertyChanged();
                    ResetSelectedPowerIndex(4);
                }
            }
        }
        private int _SelectedPowerIndex4;

        public int SelectedPowerIndex5
        {
            get { return _SelectedPowerIndex5; }
            set
            {
                if (_SelectedPowerIndex5 != value)
                {
                    _SelectedPowerIndex5 = value;
                    NotifyThisPropertyChanged();
                    ResetSelectedPowerIndex(5);
                }
            }
        }
        private int _SelectedPowerIndex5;

        public int SelectedGearIndex
        {
            get { return _SelectedGearIndex; }
            set
            {
                if (_SelectedGearIndex != value)
                {
                    _SelectedGearIndex = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedGearName));
                    NotifyPropertyChanged(nameof(SelectedGearIcon));
                    NotifyPropertyChanged(nameof(SelectedGearDescription));
                    NotifyPropertyChanged(nameof(SelectedGearEffectDescriptionList));
                }
            }
        }

        public string SelectedGearName
        {
            get
            {
                if (_SelectedGearIndex < 0)
                    return SlotName;

                string GearName = SortedGearList[_SelectedGearIndex].Name;

                foreach (PlanerSlotPower Item in SelectedPowerList1)
                    if (Item.Reference.Prefix != null || Item.Reference.Suffix != null)
                    {
                        GearName = Item.Reference.Prefix + " " + GearName;
                        break;
                    }

                foreach (PlanerSlotPower Item in SelectedPowerList2)
                    if (Item.Reference.Prefix != null || Item.Reference.Suffix != null)
                    {
                        GearName = GearName + " " + Item.Reference.Suffix;
                        break;
                    }

                return GearName;
            }
        }

        public string SelectedGearIcon
        {
            get { return _SelectedGearIndex >= 0 ? "icon_" + SortedGearList[_SelectedGearIndex].IconId : IconFileName; }
        }
        public string SelectedGearDescription
        {
            get { return _SelectedGearIndex >= 0 ? SortedGearList[_SelectedGearIndex].Description : null; }
        }
        public ItemEffectCollection SelectedGearEffectDescriptionList
        {
            get { return _SelectedGearIndex >= 0 ? SortedGearList[_SelectedGearIndex].EffectDescriptionList : null; }
        }
        private int _SelectedGearIndex;

        public int ColorIndex
        {
            get
            {
                return SelectedPowerList1.Count + SelectedPowerList2.Count + SelectedPowerList3.Count + SelectedPowerList4.Count + SelectedPowerList5.Count;
            }
        }

        public void RefreshCombatSkillList(IList<Power> PowerList, Dictionary<string, IGenericJsonObject> AttributeTable, PowerSkill FirstSkill, int MaxLevelFirstSkill, PowerSkill SecondSkill, int MaxLevelSecondSkill, int MaxLevelGeneric)
        {
            AvailablePowerList1.Clear();
            AvailablePowerList2.Clear();
            AvailablePowerList3.Clear();
            AvailablePowerList4.Clear();
            AvailablePowerList5.Clear();
            SelectedPowerList1.Clear();
            SelectedPowerList2.Clear();
            SelectedPowerList3.Clear();
            SelectedPowerList4.Clear();
            SelectedPowerList5.Clear();
            AvailablePowerIndex1 = -1;
            AvailablePowerIndex2 = -1;
            AvailablePowerIndex3 = -1;
            AvailablePowerIndex4 = -1;
            AvailablePowerIndex5 = -1;
            SelectedPowerIndex1 = -1;
            SelectedPowerIndex2 = -1;
            SelectedPowerIndex3 = -1;
            SelectedPowerIndex4 = -1;
            SelectedPowerIndex5 = -1;

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(FirstSkill, Slot))
                    AvailablePowerList1.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelFirstSkill));

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(SecondSkill, Slot))
                    AvailablePowerList2.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelSecondSkill));

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(PowerSkill.AnySkill, Slot))
                    AvailablePowerList3.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelGeneric));

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(PowerSkill.Endurance, Slot))
                    AvailablePowerList4.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelGeneric));

            foreach (Power PowerItem in PowerList)
                if (PowerItem.IsValidForSlot(PowerSkill.ShamanicInfusion, Slot))
                    AvailablePowerList5.Add(new PlanerSlotPower(PowerItem, AttributeTable, MaxLevelGeneric));

            NotifyPropertyChanged(nameof(ColorIndex));
            NotifyPropertyChanged(nameof(SelectedGearName));
            NotifyPropertyChanged(nameof(IsFirstSkillSelected));
            NotifyPropertyChanged(nameof(IsSecondSkillSelected));
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

        public void AddPower3()
        {
            int SourceIndex = AvailablePowerIndex3;
            int DestinationIndex = SelectedPowerIndex3;

            MovePower(AvailablePowerList3, ref SourceIndex, SelectedPowerList3, ref DestinationIndex);

            AvailablePowerIndex3 = SourceIndex;
            SelectedPowerIndex3 = DestinationIndex;
        }

        public void AddPower4()
        {
            int SourceIndex = AvailablePowerIndex4;
            int DestinationIndex = SelectedPowerIndex4;

            MovePower(AvailablePowerList4, ref SourceIndex, SelectedPowerList4, ref DestinationIndex);

            AvailablePowerIndex4 = SourceIndex;
            SelectedPowerIndex4 = DestinationIndex;
        }

        public void AddPower5()
        {
            int SourceIndex = AvailablePowerIndex5;
            int DestinationIndex = SelectedPowerIndex5;

            MovePower(AvailablePowerList5, ref SourceIndex, SelectedPowerList5, ref DestinationIndex);

            AvailablePowerIndex5 = SourceIndex;
            SelectedPowerIndex5 = DestinationIndex;
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

        public void RemovePower3()
        {
            int SourceIndex = SelectedPowerIndex3;
            int DestinationIndex = AvailablePowerIndex3;

            MovePower(SelectedPowerList3, ref SourceIndex, AvailablePowerList3, ref DestinationIndex);

            SelectedPowerIndex3 = SourceIndex;
            AvailablePowerIndex3 = DestinationIndex;
        }

        public void RemovePower4()
        {
            int SourceIndex = SelectedPowerIndex4;
            int DestinationIndex = AvailablePowerIndex4;

            MovePower(SelectedPowerList4, ref SourceIndex, AvailablePowerList4, ref DestinationIndex);

            SelectedPowerIndex4 = SourceIndex;
            AvailablePowerIndex4 = DestinationIndex;
        }

        public void RemovePower5()
        {
            int SourceIndex = SelectedPowerIndex5;
            int DestinationIndex = AvailablePowerIndex5;

            MovePower(SelectedPowerList5, ref SourceIndex, AvailablePowerList5, ref DestinationIndex);

            SelectedPowerIndex5 = SourceIndex;
            AvailablePowerIndex5 = DestinationIndex;
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

                NotifyPropertyChanged(nameof(ColorIndex));
                NotifyPropertyChanged(nameof(SelectedGearName));
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

                NotifyPropertyChanged(nameof(ColorIndex));
                NotifyPropertyChanged(nameof(SelectedGearName));
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

                NotifyPropertyChanged(nameof(ColorIndex));
                NotifyPropertyChanged(nameof(SelectedGearName));
            }
        }

        public void SelectPower3(PlanerSlotPower PlanerSlot)
        {
            int Index = AvailablePowerList3.IndexOf(PlanerSlot);
            if (Index >= 0)
            {
                PlanerSlotPower Power = AvailablePowerList3[Index];

                AvailablePowerList3.RemoveAt(Index);
                SelectedPowerList3.Add(Power);

                NotifyPropertyChanged(nameof(ColorIndex));
                NotifyPropertyChanged(nameof(SelectedGearName));
            }
        }

        public void SelectPower4(PlanerSlotPower PlanerSlot)
        {
            int Index = AvailablePowerList4.IndexOf(PlanerSlot);
            if (Index >= 0)
            {
                PlanerSlotPower Power = AvailablePowerList4[Index];

                AvailablePowerList4.RemoveAt(Index);
                SelectedPowerList4.Add(Power);

                NotifyPropertyChanged(nameof(ColorIndex));
                NotifyPropertyChanged(nameof(SelectedGearName));
            }
        }

        public void SelectPower5(PlanerSlotPower PlanerSlot)
        {
            int Index = AvailablePowerList5.IndexOf(PlanerSlot);
            if (Index >= 0)
            {
                PlanerSlotPower Power = AvailablePowerList5[Index];

                AvailablePowerList5.RemoveAt(Index);
                SelectedPowerList5.Add(Power);

                NotifyPropertyChanged(nameof(ColorIndex));
                NotifyPropertyChanged(nameof(SelectedGearName));
            }
        }

        public void RefreshGearList(ICollection<Item> ItemList, ICollection<Attribute> AttributeList, WeightProfile WeightProfile, bool IgnoreUnobtainable, bool IgnoreNoAttribute)
        {
            Item OldSelectedGear = SelectedGearIndex >= 0 && SelectedGearIndex < SortedGearList.Count ? SortedGearList[SelectedGearIndex] : null;

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
                if (SortedGearList.Count > ListBox.MaxCount)
                    SortedGearList.RemoveAt(SortedGearList.Count - 1);

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

            SelectedGearIndex = SortedGearList.IndexOf(OldSelectedGear);
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

        private void ResetAvailablePowerIndex(int NotReset)
        {
            if (NotReset != 1)
            {
                _AvailablePowerIndex1 = -1;
                NotifyPropertyChanged(nameof(AvailablePowerIndex1));
            }

            if (NotReset != 2)
            {
                _AvailablePowerIndex2 = -1;
                NotifyPropertyChanged(nameof(AvailablePowerIndex2));
            }

            if (NotReset != 3)
            {
                _AvailablePowerIndex3 = -1;
                NotifyPropertyChanged(nameof(AvailablePowerIndex3));
            }

            if (NotReset != 4)
            {
                _AvailablePowerIndex4 = -1;
                NotifyPropertyChanged(nameof(AvailablePowerIndex4));
            }

            if (NotReset != 5)
            {
                _AvailablePowerIndex5 = -1;
                NotifyPropertyChanged(nameof(AvailablePowerIndex5));
            }
        }

        private void ResetSelectedPowerIndex(int NotReset)
        {
            if (NotReset != 1)
            {
                _SelectedPowerIndex1 = -1;
                NotifyPropertyChanged(nameof(SelectedPowerIndex1));
            }

            if (NotReset != 2)
            {
                _SelectedPowerIndex2 = -1;
                NotifyPropertyChanged(nameof(SelectedPowerIndex2));
            }

            if (NotReset != 3)
            {
                _SelectedPowerIndex3 = -1;
                NotifyPropertyChanged(nameof(SelectedPowerIndex3));
            }

            if (NotReset != 4)
            {
                _SelectedPowerIndex4 = -1;
                NotifyPropertyChanged(nameof(SelectedPowerIndex4));
            }

            if (NotReset != 5)
            {
                _SelectedPowerIndex5 = -1;
                NotifyPropertyChanged(nameof(SelectedPowerIndex5));
            }
        }

        private void SelectPowerWithPrefixOrSuffix(ICollection<PlanerSlotPower> PowerList, ref PlanerSlotPower FirstPower)
        {
            if (FirstPower != null)
                return;

            foreach (PlanerSlotPower Item in PowerList)
                if (Item.Reference.Prefix != null || Item.Reference.Suffix != null)
                {
                    FirstPower = Item;
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
