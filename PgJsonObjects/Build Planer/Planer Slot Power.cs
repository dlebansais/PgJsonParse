using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PlanerSlotPower
    {
        public PlanerSlotPower(IPgPower Reference, Dictionary<string, IJsonKey> AttributeTable, int maxSkillLevel)
        {
            this.Reference = Reference;
            IList<IPgPowerTier> TierEffectList = Reference.TierEffectList;

            int LastSkillLevelPrereq = 0;
            IPgPowerTier LastPowerTier = null;

            foreach (IPgPowerTier Item in TierEffectList)
                if (LastPowerTier == null || (Item.RawSkillLevelPrereq.HasValue && Item.RawSkillLevelPrereq.Value <= maxSkillLevel && LastSkillLevelPrereq < Item.RawSkillLevelPrereq.Value))
                {
                    LastPowerTier = Item;
                    if (Item.RawSkillLevelPrereq.HasValue)
                        LastSkillLevelPrereq = Item.RawSkillLevelPrereq.Value;
                }

            IPgPowerEffectCollection EffectList = LastPowerTier.EffectList;

            string Name = "";
            List<int> CombinedIdList = new List<int>();
            foreach (IPgPowerEffect EffectItem in EffectList)
            {
                IPgPowerAttributeLink AsPowerAttributeLink;
                IPgPowerSimpleEffect AsPowerSimpleEffect;

                if (Name.Length > 0)
                    Name += ", ";

                if ((AsPowerAttributeLink = EffectItem as IPgPowerAttributeLink) != null)
                {
                    if (AttributeTable.ContainsKey(AsPowerAttributeLink.AttributeName))
                    {
                        IPgAttribute PowerAttribute = AttributeTable[AsPowerAttributeLink.AttributeName] as IPgAttribute;

                        bool IsPercent = PowerAttribute.IsLabelWithPercent;
                        string Label = PowerAttribute.LabelRippedOfPercent;

                        Name += Label;

                        if (AsPowerAttributeLink.AttributeEffect != 0)
                        {
                            float PowerValue = AsPowerAttributeLink.AttributeEffect;

                            if (IsPercent)
                            {
                                string PowerValueString = Tools.FloatToString(PowerValue * 100, AsPowerAttributeLink.AttributeEffectFormat);

                                if (PowerValue > 0)
                                    PowerValueString = "+" + PowerValueString;

                                Name += " " + PowerValueString + "%";
                            }
                            else
                            {
                                string PowerValueString = Tools.FloatToString(PowerValue, AsPowerAttributeLink.AttributeEffectFormat);

                                if (PowerValue > 0)
                                    PowerValueString = "+" + PowerValueString;

                                Name += " " + PowerValueString;
                            }
                        }
                    }

                    if (AsPowerAttributeLink.AttributeLink != null)
                        foreach (int Id in AsPowerAttributeLink.AttributeLink.IconIdList)
                            if (!CombinedIdList.Contains(Id))
                                CombinedIdList.Add(Id);
                }

                else if ((AsPowerSimpleEffect = EffectItem as IPgPowerSimpleEffect) != null)
                {
                    Name += AsPowerSimpleEffect.Description.Trim();

                    foreach (int Id in AsPowerSimpleEffect.IconIdList)
                        if (!CombinedIdList.Contains(Id))
                            CombinedIdList.Add(Id);
                }
            }

            this.Name = Name;

            IconFileNameList = new List<string>();
            foreach (int Id in CombinedIdList)
                IconFileNameList.Add("icon_" + Id);
        }

        public IPgPower Reference { get; private set; }
        public string Name { get; private set; }
        public List<string> IconFileNameList { get; private set; }
    }
}
