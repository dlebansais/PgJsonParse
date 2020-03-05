using PgJsonObjects;

namespace PgBuilder
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class GearSlot
    {
        public GearSlot(string name, ItemSlot slot)
        {
            Name = name;
            Slot = slot;
        }

        public string Name { get; }
        public ItemSlot Slot { get; }
        public ObservableCollection<Mod> SelectedModList { get; } = new ObservableCollection<Mod>();

        public List<Mod> AvailableModList { get; } = new List<Mod>();

        public void UpdateModList(IPgSkill skill, IList<IPgPower> powerList)
        {
            SelectedModList.Clear();
            AvailableModList.Clear();

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

                AvailableModList.Add(new Mod(PowerItem));
            }
        }
    }
}
