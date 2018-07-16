using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PlanerSlotPower
    {
        public PlanerSlotPower(IPgPower Reference, Dictionary<string, IGenericJsonObject> AttributeTable, int MaxLevelFirstSkill)
        {
            this.Reference = Reference;

            int LastTier = 0;
            IPgPowerTier LastPowerTier = null;
            int MaxTier;

            if (MaxLevelFirstSkill > 0 && MaxLevelFirstSkill <= 125)
            {
                if (Reference.TierEffectList.Count >= 16)
                    MaxTier = MaxLevelFirstSkill / 5;
                else if (Reference.TierEffectList.Count >= 8)
                    MaxTier = MaxLevelFirstSkill / 10;
                else if (Reference.TierEffectList.Count >= 4)
                    MaxTier = MaxLevelFirstSkill / 20;
                else if (Reference.TierEffectList.Count >= 3)
                    MaxTier = MaxLevelFirstSkill / 30;
                else if (Reference.TierEffectList.Count >= 2)
                    MaxTier = MaxLevelFirstSkill / 50;
                else
                    MaxTier = -1;
            }
            else
                MaxTier = -1;

            for (int i = 0; i < Reference.TierEffectList.Count; i++)
            {
                IPgPowerTier Item = Reference.TierEffectList[i];
                int Tier = Reference.TierOffset + i;

                if ((LastTier < Tier && (MaxTier < 0 || Tier < MaxTier)) || LastPowerTier == null)
                {
                    LastTier = Tier;
                    LastPowerTier = Item;
                }
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
